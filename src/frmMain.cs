using ActaAcademica;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Biblio
{
	public partial class frmMain : Form
	{
		string _filename = "";
		string filename { get { return _filename; } set { _filename = value; updateTitle(); } }

		private void updateTitle()
		{
			this.Text = "Cuenta artículos - " + Path.GetFileNameWithoutExtension(filename);
		}

		public frmMain()
		{
			InitializeComponent();
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Cursor.Current = Cursors.Default;
			MessageBox.Show(this, e.ExceptionObject.ToString(), "Error");
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			for (int n = 2000; n < DateTime.Now.Year; n++)
			{
				lstYears.Items.Add(n.ToString());
			}
			lstTopics.Items.Add(new Topic("Administración y Contabilidad", 1), true);
			lstTopics.Items.Add(new Topic("Agrociencias", 28));
			lstTopics.Items.Add(new Topic("Antropología", 2), true);
			lstTopics.Items.Add(new Topic("Biología", 31));
			lstTopics.Items.Add(new Topic("Ciencias de la Tierra", 48));
			lstTopics.Items.Add(new Topic("Comunicación", 3), true);
			lstTopics.Items.Add(new Topic("Derecho", 6), true);
			lstTopics.Items.Add(new Topic("Economía y Finanzas", 8), true);
			lstTopics.Items.Add(new Topic("Educación", 9), true);
			lstTopics.Items.Add(new Topic("Filosofía", 11), true);
			lstTopics.Items.Add(new Topic("Historia", 13), true);
			lstTopics.Items.Add(new Topic("Ingeniería", 38));
			lstTopics.Items.Add(new Topic("Lengua y Literatura", 24), true);
			lstTopics.Items.Add(new Topic("Medicina", 40));
			lstTopics.Items.Add(new Topic("Multidisciplinarias (Ciencias Sociales))", 17), true);
			lstTopics.Items.Add(new Topic("Política", 14), true);
			lstTopics.Items.Add(new Topic("Psicología", 15), true);
			lstTopics.Items.Add(new Topic("Salud", 20));
			lstTopics.Items.Add(new Topic("Sociología", 16), true);
			lstTopics.Items.Add(new Topic("Veterinaria", 43));

		}
		private void lstTopics_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			UpdateTopics(e);

		}

		private void UpdateTopics(ItemCheckEventArgs e)
		{
			txtTopics.Text = "";
			var clicked = lstTopics.Items[e.Index] as Topic;
			foreach (Topic t in lstTopics.CheckedItems)
			{
				if (e.NewValue == CheckState.Checked || t != clicked)
					txtTopics.Text += t.Id.ToString() + " ";
			}
			if (e.NewValue == CheckState.Checked)
				txtTopics.Text += clicked.Id.ToString() + " ";
		}
		private string GetYears()
		{
			var ret = "";
			foreach (string y in lstYears.CheckedItems)
			{
				ret += y + " ";
			}
			return ret;
		}

		private void cmdGet_Click(object sender, EventArgs e)
		{
			string q = txtSearch.Text;
			if (q.Length == 0)
			{
				MessageBox.Show("Debe indicar una búsqueda");
				return;
			}
			if (lstTopics.CheckedItems.Count == 0)
			{
				MessageBox.Show("Debe indicar disciplinas");
				return;
			}
			try
			{
				begin();
				List<Topic> topics = new List<Topic>();
				foreach (Topic topic in lstTopics.CheckedItems)
					topics.Add(topic);

				foreach (Topic topic in topics)
				{
					lstTopics.SelectedItem = topic;
					ProcessTopicYear(topic);
				}
				//cmdGetPdfs_Click(null, null);
				MessageBox.Show(this, "Listo", "Listo");
			}
			finally
			{
				end();
			}
		}

		private void begin()
		{
			this.Enabled = false;
			Cursor.Current = Cursors.WaitCursor;
		}
		private void end()
		{
			lblStatus.Text = "";
			this.Enabled = true;
			Cursor.Current = Cursors.Default;
		}
		private void ProcessTopicYear(Topic topic)
		{
			int page = 1;
			while (true)
			{
				string fullQuery = txtQuery.Text;
				fullQuery = set(fullQuery, "búsqueda", txtSearch.Text);
				fullQuery = set(fullQuery, "disciplinas", topic.Id.ToString());
				fullQuery = set(fullQuery, "años", GetYears());
				fullQuery = set(fullQuery, "página", page.ToString());
				string tmp = Path.GetTempFileName() + ".html";
				Http.GetFile(fullQuery, tmp);
				if (!ParseResults(tmp, topic))
				{
					File.Delete(tmp);
					break;
				}
				updateCount();
				File.Delete(tmp);
				page++;
			}
		}

		private string set(string fullQuery, string tag, string text)
		{
			return fullQuery.Replace("{{ " + tag + " }}", System.Web.HttpUtility.UrlEncode(text));
		}

		private void updateCount()
		{
			lblCount.Text = lstResults.Items.Count.ToString() + " filas.";
			//this.Refresh();
			Application.DoEvents();
		}

		private bool ParseResults(string tmp, Topic topic)
		{
			var doc = Http.GetDocFromFile(tmp);
			var tags = doc.DocumentNode.Descendants();
			HtmlNode table2 = (from table in tags
												 where table.Id.EndsWith(":tablaTextoCompleto")
									 && table.Name == "table"
												 select table).FirstOrDefault();
			if (table2 == null)
				return false;
			foreach (HtmlNode row in from tr in table2.Descendants()
															 where tr.Name == "tr" && tr.Attributes.Contains("class") && tr.Attributes["class"].Value.Contains("dr-table-row rich-table-row")
															 select tr)
			{
				if (row.SelectNodes("td") != null)
				{
					List<string> items = new List<string>();
					string abstracts = null;
					string pdflink = null;
					var cells = row.SelectNodes("td[@class='dr-table-cell rich-table-cell ']");
					if (cells != null)
					{
						foreach (HtmlNode col in cells)
						{
							items.Add(col.InnerText);
							if (items.Count == 5)
							{
								HtmlNode resDiv = (from table in col.Descendants()
																	 where table.Attributes.Contains("class")
																	 && table.Attributes["class"].Value == "texto-resumen"
																	 select table).FirstOrDefault();
								if (resDiv != null)
									abstracts = resDiv.InnerText;
							}
							else if (items.Count == 6)
							{
								HtmlNode href = (from table in col.Descendants()
																 where table.Name == "a"
																 && table.InnerText.Contains("PDF [es]")
																 select table).FirstOrDefault();
								if (href != null)
									pdflink = href.Attributes["href"].Value;
							}
						}
						if (items[3].StartsWith("-1") == false)
						{
							Article a = new Article();
							a.Title = System.Web.HttpUtility.HtmlDecode(items[0]);
							a.Authors = System.Web.HttpUtility.HtmlDecode(items[1]);
							a.Journal = System.Web.HttpUtility.HtmlDecode(items[2]);
							a.Number = System.Web.HttpUtility.HtmlDecode(items[3].Substring(5));
							a.Year = items[3].Substring(0, 4);
							a.Abstract = System.Web.HttpUtility.HtmlDecode(abstracts);
							if (pdflink != null)
								a.Pdf = makeAbsolute(pdflink);
							a.Topic = topic.Name;
							// se fija que no esté duplicado...
							if (notExists(a))
								lstResults.Items.Add(a.GetListViewItem());
						}
					}
				}
			}
			return true;
		}

		private string makeAbsolute(string pdflink)
		{
			var ret = "http://www.redalyc.org";
			if (pdflink.StartsWith("/") == false)
				ret+= "/";
			return ret + pdflink;
		}

		private bool notExists(Article a)
		{
			foreach (ListViewItem item in lstResults.Items)
			{
				Article b = item.Tag as Article;
				if (b.Pdf != null && a.Pdf == b.Pdf)
					return false;
				if (a.Title == b.Title)
					return false;
			}
			return true;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void cmdClear_Click(object sender, EventArgs e)
		{
			lstResults.Items.Clear();
			updateCount();
		}

		private void cmdSaveAs_Click(object sender, EventArgs e)
		{
				SaveFileDialog savefile = new SaveFileDialog();
				// set a default file name
				if(filename != "")
					savefile.FileName = Path.GetFileName(filename);
				else
					savefile.FileName = "nuevo.bib";
				// set filters - this can be done in properties as well
				savefile.Filter = "Biblio files (*.bib)|*.bib|All files (*.*)|*.*";
				if (savefile.ShowDialog() == DialogResult.OK)
				{
					SaveState(savefile.FileName);
				}

		}

		private void SaveState(string fileName)
		{
			this.filename = fileName;
			SaveState s = new SaveState();
			foreach (ListViewItem i in lstResults.Items)
				s.Results.Add(i.Tag as Article);
			foreach (string year in lstYears.CheckedItems)
				s.Years.Add(year);
			foreach (Topic t in lstTopics.CheckedItems)
				s.Topics.Add(t.Id);
			s.Search = txtSearch.Text;
			s.Match1 = txtMatch1.Text;
			s.Match2 = txtMatch2.Text;

			FileStream stream = File.Create(filename);
			var formatter = new BinaryFormatter();
			formatter.Serialize(stream, s);
			stream.Close();
		}

		private void cmbOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog openfile = new OpenFileDialog();
			// set filters - this can be done in properties as well
			openfile.Filter = "Biblio files (*.bib)|*.bib|All files (*.*)|*.*";
			if (openfile.ShowDialog() == DialogResult.OK)
			{
				OpenState(openfile.FileName);
			}

		}

		private void OpenState(string fileName)
		{
			this.filename = fileName;
			var formatter = new BinaryFormatter();
			FileStream stream = File.OpenRead(filename);
			SaveState s = (SaveState)formatter.Deserialize(stream);
			stream.Close();

			lstResults.BeginUpdate();
			lstResults.Items.Clear();
			foreach (Article i in s.Results)
			{
				string fileTxt = i.CalculateFilenameTxt(this.filename);
				i.HasFile = File.Exists(fileTxt);
				lstResults.Items.Add(i.GetListViewItem());
			}
			lstResults.EndUpdate();

			updateCount();
			while (lstYears.CheckedItems.Count > 0)
				lstYears.SetItemChecked(lstYears.CheckedIndices[0], false);
			while (lstTopics.CheckedItems.Count > 0)
				lstTopics.SetItemChecked(lstTopics.CheckedIndices[0], false);
			foreach (string year in s.Years)
				lstYears.SetItemChecked(getYearPos(year), true);
			foreach (int topicId in s.Topics)
				lstTopics.SetItemChecked(getTopicPos(topicId), true);

			txtSearch.Text = s.Search;
			txtMatch1.Text = s.Match1;
			txtMatch2.Text = s.Match2;


		}

		private int getTopicPos(int topicId)
		{
			int i = 0;
			foreach (Topic t in lstTopics.Items)
			{
				if (t.Id == topicId)
					return i;
				i++;
			}
			return -1;
		}

		private int getYearPos(string year)
		{
			int i = 0;
			foreach (string lyear in lstYears.Items)
			{
				if (year == lyear)
					return i;
				i++;
			}
			return -1;
		}

		private void cmdGetPdfs_Click(object sender, EventArgs e)
		{
			if (this.filename == "")
			{
				MessageBox.Show(this, "Debe primero grabar los resultados.", "Atención");
				return;
			}
			try
			{
				begin();
				
				DownloadPdfs();
				MessageBox.Show(this, "Listo", "Listo");
			}
			finally
			{
				end();
			}
		}

		private void DownloadPdfs()
		{
			int total = lstResults.Items.Count;
			int current = 0;
			for (int n = 0; n < lstResults.Items.Count; n++)
			{
				ListViewItem item = lstResults.Items[n];
				UpdateProgressLabel(total, current++);
				var article = item.Tag as Article;
				DownloadArticle(article);
				RegenItem(n, article);
			}
		}

		private void UpdateProgressLabel(int total, int current)
		{
			lblStatus.Text = (current).ToString() + " de " + total + " (" + ((int)(current * 100 / total)) + "%)";
			//this.Refresh();
			Application.DoEvents();
		}

		private void DownloadArticle(Article article)
		{
			string filename = article.CalculateFilename(this.filename, true);
			if (filename == null || File.Exists(filename))
				return;

			HtmlNode href = CalculateURL(article);
			if (href != null)
			{
				var pdflink = href.Attributes["href"].Value;
				try
				{
					Http.GetFile(makeAbsolute(pdflink), filename);

					ConvertToTxt(filename);

					article.HasFile = File.Exists(filename);

				}
				catch { }
			}
			
		}

		private static void ConvertToTxt(string filename)
		{
			var txtFilename = filename + ".txt";
			if (File.Exists(filename) && !File.Exists(txtFilename))
			{
				string text = PdfToText.GetPdfText(filename);
				if (text != null && text.Length > 0)
					File.WriteAllText(txtFilename, text);
			}
		}

		private static HtmlNode CalculateURL(Article article)
		{
			string tmp = Path.GetTempFileName();
			Http.GetFile(article.Pdf, tmp);
			var doc = Http.GetDocFromFile(tmp);
			var tags = doc.DocumentNode.Descendants();
			HtmlNode href = (from table in tags
											 where table.Name == "a"
											 && table.InnerText.Contains("Descargar PDF")
											 select table).FirstOrDefault();
			File.Delete(tmp);
			return href;
		}

		private void cmdMatch1_Click(object sender, EventArgs e)
		{
			Match(typeof(Article).GetField("Match1"), txtMatch1.Text);
		}

		private void cmdMatch2_Click(object sender, EventArgs e)
		{
			Match(typeof(Article).GetField("Match2"), txtMatch2.Text);
		}
		private void Match(FieldInfo property, string text)
		{
			if (this.filename == "")
			{
				MessageBox.Show(this, "Debe primero grabar los resultados y obtener los PDFs.", "Atención");
				return;
			}
			try
			{
				begin();
				lstResults.BeginUpdate();
				int total = lstResults.Items.Count;
				int current = 0;
				for(int n = 0; n < lstResults.Items.Count; n++)
				{
					ListViewItem item = lstResults.Items[n];
					UpdateProgressLabel(total, current++);
					Article b = item.Tag as Article;
					property.SetValue(b, CalculateMatch(b, text));
					RegenItem(n, b);
				}
				MessageBox.Show(this, "Listo", "Listo");
			}
			finally
			{
				lstResults.EndUpdate();
				end();
			}
		}

		private void RegenItem(int n, Article b)
		{
			lstResults.Items.RemoveAt(n);
			lstResults.Items.Insert(n, b.GetListViewItem());
			lstResults.SelectedIndices.Clear();
			lstResults.SelectedIndices.Add(n);
		}

		private Match CalculateMatch(Article b, string text)
		{
			Match m = new Match();
			m.Title = matchCount(b.Title, text);
			if (b.Abstract != null)
				m.Abstract = matchCount(b.Abstract, text);
			string fileTxt = b.CalculateFilenameTxt(this.filename);
			if (File.Exists(fileTxt))
			{
				m.Pdf = matchCount(File.ReadAllText(fileTxt), text);
			}
			return m;
		}

		private int matchCount(string text, string searchPart)
		{
			return text.ToLower().Split(new string[] { searchPart.ToLower() }, StringSplitOptions.None).Length - 1;
		}

		private void cmdSave_Click(object sender, EventArgs e)
		{
			if (filename == "")
			{
				cmdSaveAs.PerformClick();
				return;
			}
			SaveState(filename);
		}

		private void cmdSaveCSV_Click(object sender, EventArgs e)
		{
			SaveFileDialog savefile = new SaveFileDialog();
			// set a default file name
			if (filename != "")
				savefile.FileName = Path.GetFileNameWithoutExtension(filename) + ".csv";
			else
				savefile.FileName = "nuevo.csv";
			// set filters - this can be done in properties as well
			savefile.Filter = "Archivos separados por coma (*.csv)|*.csv|All files (*.*)|*.*";
			if (savefile.ShowDialog() == DialogResult.OK)
			{
				SaveCSV(savefile.FileName);
			}
		}

		private void SaveCSV(string fileName)
		{
			ListViewToCSV(lstResults, fileName, true);
		}
		public static void ListViewToCSV(ListView listView, string filePath, bool includeHidden)
		{
			//make header string
			StringBuilder result = new StringBuilder();
			WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listView.Columns[i].Text);

			//export data rows
			foreach (ListViewItem listItem in listView.Items)
				WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listItem.SubItems[i].Text);

			File.WriteAllText(filePath, result.ToString(), Encoding.UTF8);
		}

		private static void WriteCSVRowArray(StringBuilder result, string[] vals)
		{
			WriteCSVRow(result, vals.Length, i => true, i => vals[i]);
		}
		private static void WriteCSVRow(StringBuilder result, int itemsCount, Func<int, bool> isColumnNeeded, Func<int, string> columnValue)
		{
			bool isFirstTime = true;
			for (int i = 0; i < itemsCount; i++)
			{
				if (!isColumnNeeded(i))
					continue;

				if (!isFirstTime)
					result.Append(",");
				isFirstTime = false;
				string clean = sanitize(columnValue(i));
				result.Append(String.Format("\"{0}\"", clean));
			}
			result.AppendLine();
		}

		private static string sanitize(string v)
		{
			string ret = v.Replace("\"", "\"\"");
			ret = ret.Replace("\n", " ");
			ret = ret.Replace("\r", " ");
			ret = ret.Replace("  ", " ");
			ret = ret.Replace("  ", " ");
			return ret;
		}

		private void cmdGetTotals_Click(object sender, EventArgs e)
		{
			string q = txtSearch.Text;
			if (lstTopics.CheckedItems.Count == 0)
			{
				MessageBox.Show("Debe indicar disciplinas");
				return;
			}
			try
			{
				begin();
				List<Topic> topics = new List<Topic>();
				foreach (Topic topic in lstTopics.CheckedItems)
					topics.Add(topic);

				List<Tuple<Topic, int, int>> totals = new List<Tuple<Topic, int, int>>();
				foreach (Topic topic in topics)
				{
					lstTopics.SelectedItem = topic;
					totals.AddRange(GetTotalsTopicYear(topic));
				}
				string filename = getFilename();
				if (filename == null)
					return;
				SaveTotalsCsv(filename, totals);
				MessageBox.Show(this, "Listo", "Listo");
			}
			finally
			{
				end();
			}
		}

		private void SaveTotalsCsv(string filename, List<Tuple<Topic, int, int>> totals)
		{
			//make header string
			StringBuilder result = new StringBuilder();
			WriteCSVRowArray(result, new string[] { "Disciplina", "Año", "Total" });

			//export data rows
			foreach (var total in totals)
				WriteCSVRowArray(result, new string[] { total.Item1.Name, total.Item2.ToString(), total.Item3.ToString() });

			File.WriteAllText(filename, result.ToString(), Encoding.UTF8);
		}

		private string getFilename()
		{
			SaveFileDialog savefile = new SaveFileDialog();
			// set a default file name
			if (filename != "")
				savefile.FileName = Path.GetFileNameWithoutExtension(filename) + "Totales.csv";
			else
				savefile.FileName = "totales.csv";
			// set filters - this can be done in properties as well
			savefile.Filter = "Archivos separados por coma (*.csv)|*.csv|All files (*.*)|*.*";
			if (savefile.ShowDialog() == DialogResult.OK)
			{
				return savefile.FileName;
			}
			else
				return null;
		}

		private List<Tuple<Topic, int, int>> GetTotalsTopicYear(Topic topic)
		{
			List<Tuple<Topic, int, int>> ret = new List<Tuple<Topic, int, int>>();
			string url = "http://www.redalyc.org/busquedaArticuloFiltros.oa?q=a&idp=1&a=&i=&d={{ topico }}&cvePais=#panel";
			string fullQuery = set(url, "topico", topic.Id.ToString());
			string tmp = Path.GetTempFileName() + ".html";
			Http.GetFile(fullQuery, tmp);

					var doc = Http.GetDocFromFile(tmp);
			var tags = doc.DocumentNode.Descendants();
			var links = (from table in tags
											 where table.Name == "a"
									 select table);
			foreach(var link in links)
			{
				if (link.Attributes.Contains("title"))
				{
					int val;
					if (int.TryParse(link.Attributes["title"].Value, out val))
					{
						if (val > 1900 && val < 2200)
						{
							var text = Http.CleanHtmlTextSpaces(link.InnerHtml, CleanExtraOptions.opRemoverParentesisInicioFin);
							int count = int.Parse(text);
							ret.Add(new Tuple<Topic, int, int>(topic, val, count));
						}
					}
				}
			}
			File.Delete(tmp);
	
			return ret;
		}

		private void txtFind_TextChanged(object sender, EventArgs e)
		{
			string text = txtFind.Text.Trim();
			if (text == "")
			{
				lstResults.SelectedIndices.Clear();
				cmdNext.Text = "Siguiente";				
				return;
			}
			var item = findItem(text, null);
			SelectItem(item);
			
			var n = countItems(text);
			cmdNext.Text = "Siguiente (" + n.ToString() + ")";
		}

		private void cmdNext_Click(object sender, EventArgs e)
		{
			string text = txtFind.Text.Trim();
			if (text == "")
			{
				lstResults.SelectedIndices.Clear();
				return;
			}
			ListViewItem first = GetSelectedItem();
			var item = findItem(text, first);
			SelectItem(item);
		}

		private void SelectItem(ListViewItem item)
		{
			lstResults.SelectedIndices.Clear();
			if (item != null)
			{
				item.Selected = true;
				lstResults.TopItem = item;
			}
		}

		private ListViewItem GetSelectedItem()
		{
			ListViewItem first = null;
			if (lstResults.SelectedItems.Count > 0)
				first = lstResults.SelectedItems[0];
			return first;
		}

		
		private int countItems(string text)
		{
			text = text.ToLower();
			int n = 0;
			foreach(ListViewItem item in lstResults.Items)
			{
				if (MatchSearch(item, text))
					n++;
			}
			return n;
		}
		private ListViewItem findItem(string text, ListViewItem first)
		{
			text = text.ToLower();
			bool passedFirst = first == null;
			foreach(ListViewItem item in lstResults.Items)
			{
				if (passedFirst)
				{ 
					if (MatchSearch(item, text))
					{
						return item;
					}
				}
				else
				{
					passedFirst = first == item;
				}
			}
			return null;
		}

		private bool MatchSearch(ListViewItem item, string text)
		{
			var article = item.Tag as Article;
			return (article.Title.ToLower().Contains(text) ||
						article.Authors.ToLower().Contains(text));
		}

		private void lstResults_DoubleClick(object sender, EventArgs e)
		{
			Openselected();
		}

		private void Openselected()
		{
			ListViewItem first = GetSelectedItem();
			if (first != null)
			{
				var article = first.Tag as Article;
				if (article.HasFile)
				{
					Process.Start(article.CalculateFilename(this.filename, true));
				}
				else
				{
					MessageBox.Show(this, "El artículo seleccionado no tiene un pdf asociado.");
				}
			}
		}

		private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
				Openselected();
		}

		private void lstResults_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void lstResults_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
				Openselected();
		}
	}
}