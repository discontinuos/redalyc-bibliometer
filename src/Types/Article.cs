using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblio
{
	[Serializable]

	class Article
	{
		public int Id;
		public string Title;
		public string Authors;
		public string Journal;
		public string Year;
		public string Topic;
		public string Number;
		public string Pdf;
		public string Abstract;
		public Match Match1 = new Match();
		public Match Match2 = new Match();
		public bool HasFile = false;

		public Article()
		{
		}
		public override string ToString()
		{
			return Title;
		}

		internal ListViewItem GetListViewItem()
		{
			ListViewItem i = new ListViewItem();
			i.Text = Title;
			i.SubItems.Add(Authors);
			i.SubItems.Add(Journal);
			i.SubItems.Add(Number);
			i.SubItems.Add(Abstract);
			i.SubItems.Add(Pdf);
			i.SubItems.Add(Year);
			i.SubItems.Add(Topic);
			i.SubItems.Add(Match1.Title.ToString());
			i.SubItems.Add(Match1.Abstract.ToString());
			i.SubItems.Add(Match1.Pdf.ToString());
			i.SubItems.Add(Match2.Title.ToString());
			i.SubItems.Add(Match2.Abstract.ToString());
			i.SubItems.Add(Match2.Pdf.ToString());
			if (HasFile)
				i.SubItems.Add("OK");
			else
				i.SubItems.Add("");
			i.Tag = this;
			return i;
		}

		internal string CalculateFilename(string basePath, bool create = false)
		{
			if (this.Pdf == null || this.Pdf.Contains("=") == false)
				return null;

			var folder = Path.GetDirectoryName(basePath);
			var file = Path.GetFileNameWithoutExtension(basePath);
			var f2 = Path.Combine(folder, file + "_files");
			if (Directory.Exists(f2) == false)
			{
				if (create)
					Directory.CreateDirectory(f2);
				else
					return null;
			}
			string id = Pdf.Split('=')[1];
			return Path.Combine(f2, id + ".pdf");

			}

		internal string CalculateFilenameTxt(string filename)
		{
			string file = CalculateFilename(filename);
			if (file == null)
				return null;
			return file + ".txt";
		}
	}
}
