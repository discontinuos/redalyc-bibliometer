using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO.Compression;

namespace ActaAcademica
{
	public class Http
	{
		public static bool forceUTF8 = true;

		public static void ClearCurrentFiles()
		{
			string path = CurrentFilesPath();
			Directory.Delete(path, true);
			Directory.CreateDirectory(path);
		}
		public static string CurrentFilesPath()
		{
			string path = System.IO.Path.GetTempPath() + "\\ChupaFiles";
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			return path;
		}
		public static HtmlAgilityPack.HtmlDocument GetDoc(string url)
		{
			string tmp = GetFileToTemp(url);
			return GetDocFromFile(tmp);
		}
		public static HtmlDocument GetDocFromString(string cad)
		{
			string tmp = Path.GetTempFileName();
			File.WriteAllText(tmp, cad, Encoding.UTF8);
			HtmlDocument ret = new HtmlDocument();
			ret.Load(tmp, Encoding.UTF8);
			File.Delete(tmp);
			return ret;
		}
		public static HtmlDocument GetDocFromFile(string tmp, bool localForce = false)
		{
			HtmlDocument ret = new HtmlDocument();
			if (File.ReadAllText(tmp).IndexOf("charset=iso-8859-1") >= -1 && !forceUTF8 && !localForce)
				ret.Load(tmp);
			else
				ret.Load(tmp, Encoding.UTF8);
			return ret;
		}

		public static string GetFileToTemp(string url)
		{
			string tmp = Path.GetTempFileName();
			GetFile(url, tmp);
			return tmp;
		}

		public static void GetFile(string url, string tmp)
		{
			//if (FileCache.HasFile(url))
			//{
			//	FileCache.GetFileTo(url, tmp);
			//	return;
			//}
			WebClient wc = new WebClient();
			wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
			wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
			wc.Headers.Add("Accept-Encoding", "gzip, deflate");
			wc.Headers.Add("Accept-Language", "es-419,es;q=0.9,en;q=0.8");
			wc.Headers.Add("Cookie", "JSESSIONID=0000fsLzp3EbBQIis5_C2wU6zkp:17dh3et6h; __utma=230685855.1342731984.1505830947.1513134401.1513142015.16; __utmb=230685855.6.10.1513142015; __utmc=230685855; __utmz=230685855.1512053963.12.10.utmcsr=scholar.google.com|utmccn=(referral)|utmcmd=referral|utmcct=/");
			wc.Headers.Add("Host", "www.redalyc.org");
		
			{
				long expectedLength = -1;
				for (int retry = 0; retry < 20; retry++)
				{
					if (retry > 0)
						Thread.Sleep(1200);

					wc.DownloadFile(url, tmp);
					if (wc.ResponseHeaders["Content-Length"] != null)
						expectedLength = long.Parse(wc.ResponseHeaders["Content-Length"]);
					else
						expectedLength = -1;
					if (expectedLength == -1 || new FileInfo(tmp).Length == expectedLength)
						break;
				}
				if (File.Exists(tmp) && new FileInfo(tmp).Length < 4096 &&
							File.ReadAllText(tmp).Contains("404 Not Found"))
					File.Delete(tmp);
				else
				{
					if (expectedLength != -1 && new FileInfo(tmp).Length != expectedLength)
						System.Windows.Forms.MessageBox.Show("No se pudo traer el archivo entero");

					DeflateStream(tmp, wc);
					//					FileCache.SaveFile(url, tmp);
				}
			}
		}

		private static void DeflateStream(string tmp, WebClient wc)
		{
			string encoding = wc.ResponseHeaders["Content-Encoding"];
			if (encoding == null)
				return;
			var cp = tmp + ".dat";
			File.Copy(tmp, cp);
			Stream responseStream = new FileStream(cp, FileMode.Open);
			if (encoding.ToLower().Contains("gzip"))
				responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
			else if (encoding.ToLower().Contains("deflate"))
				responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);
			var outputFile = File.Create(tmp);
			responseStream.CopyTo(outputFile);
			responseStream.Flush();
			responseStream.Close();
			outputFile.Close();
			File.Delete(cp);
		}

		public static System.Windows.Forms.ListBox Errors = new System.Windows.Forms.ListBox();

		private static void ShowError(string cad)
		{
			Errors.Items.Add((Errors.Items.Count + 1 ) + ". " +  cad);
		}

		public static string MergeHost(string absolute, string relative)
		{
			if (relative.StartsWith("http:"))
			{
				return relative;
			}
			else if (relative.StartsWith("/"))
			{
				return hostOnly(absolute) + relative;
			}
			else if (relative.StartsWith("../"))
			{
				return (Path.GetDirectoryName(Path.GetDirectoryName(absolute)) + relative.Substring(2)).Replace("\\", "/").Replace(":/", "://");
			}
			else
				return (Path.Combine(Path.GetDirectoryName(absolute), relative)).Replace("\\", "/").Replace(":/", "://");
		}

		private static string hostOnly(string absolute)
		{
			int n = absolute.IndexOf("/", 8);
			return absolute.Substring(0, n);
		}


        public static string ReplaceOutsideParentesis(string autores, string que, string conque)
        {
            int nStart = 0;
            while (true)
            {
                int nEnding = autores.IndexOf("(", nStart);
                if (nEnding == -1) nEnding = autores.Length - 1;

                while (true)
                {
                    int n = autores.IndexOf(que, nStart);
                    if (n < nEnding && n > -1)
                    {
                        // reemplaza
                        autores = autores.Substring(0, n) + conque + autores.Substring(n + que.Length);
                    }
                    else
                        break;
                }
                if (nEnding == autores.Length - 1) break;
                nStart = autores.IndexOf(")", nEnding);
                if (nStart == -1 || nStart == autores.Length - 1)
                    break;
                else
                    nStart++;
            }
            return autores;
        }

		public static string CleanHtmlTextSpaces(string cad, CleanExtraOptions op = CleanExtraOptions.opNinguna)
		{
			for (int n = 0; n < 2; n++)
			{
				//0x160
				cad = cad.Replace(" ", " ");
        cad = cad.Replace("&nbsp;", " ");
				cad = cad.Replace("\n", " ");
        cad = cad.Replace("&ndash;", "-");
				cad = cad.Replace("\t", " ");
				cad = cad.Replace("\r", " ");
				cad = System.Web.HttpUtility.HtmlDecode(cad);
				cad = RemoverDobleEspacio(cad);

				cad = cad.Trim();
				// opRemoverDosPuntosIniciales
				if ((op & CleanExtraOptions.opRemoverDosPuntosIniciales) != 0)
					if (cad.StartsWith(":"))
						cad = cad.Substring(1);
				// opRemoverDosPuntosFinales
				if ((op & CleanExtraOptions.opRemoverDosPuntosFinales) != 0)
					if (cad.EndsWith(":"))
						cad = cad.Substring(0, cad.Length - 1);
				// opRemoverDosPuntosFinales
				if ((op & CleanExtraOptions.opRemoverComaFinal) != 0)
					if (cad.EndsWith(","))
						cad = cad.Substring(0, cad.Length - 1);

				// opRemoverPuntoInicial
				if ((op & CleanExtraOptions.opRemoverPuntoInicial) != 0)
					if (cad.StartsWith("."))
						cad = cad.Substring(1);
				// opRemoverPuntoFinal

        if ((op & CleanExtraOptions.opRemoverPuntoFinal) != 0)
            if (cad.EndsWith("."))
                cad = cad.Substring(0, cad.Length - 1);

				// opRemoverComillas
				cad = RemoverComillas(cad, op);
				// opRemoverBloqueParentesisFinal
				cad = RemoverParentesis(cad, op);
				// opRemoverComillas
				cad = RemoverComillas(cad, op);
				// opRemoverMesaDosPuntos
				if ((op & CleanExtraOptions.opRemoverMesaDosPuntos) != 0)
				{
					if (cad.StartsWith("Mesa:"))
						cad = cad.Substring(5);
				}
				if ((op & CleanExtraOptions.opKeepBRAsNL) != 0)
				{
					cad = cad.Replace("<br>", "\r\n");
					cad = cad.Replace("<br/>", "\r\n");
					cad = cad.Replace("<BR>", "\r\n");
				}
			}
			return cad;
		}

		private static string RemoverParentesis(string cad, CleanExtraOptions op)
		{
			if ((op & CleanExtraOptions.opRemoverBloqueParentesisFinal) != 0)
			{
				if (cad.EndsWith(")"))
				{
					int n = cad.LastIndexOf("(");
					cad = cad.Substring(0, n).Trim();
				}
			}
			if ((op & CleanExtraOptions.opRemoverParentesisInicioFin) != 0)
			{
				{
					if (cad.EndsWith(")"))
						cad = cad.Substring(0, cad.Length - 1);
					if (cad.StartsWith("("))
						cad = cad.Substring(1);
				}
			}
			return cad;
		}

		private static string RemoverDobleEspacio(string cad)
		{
			while (cad.IndexOf("  ") > -1)
			{
				int n = cad.Length;
				cad = cad.Replace("  ", " ");
				if (n == cad.Length)
					break;
			}
			return cad;
		}

		private static string RemoverComillas(string cad, CleanExtraOptions op)
		{
			if ((op & CleanExtraOptions.opRemoverComillas) != 0)
			{
				if ((cad.EndsWith("\"") || cad.EndsWith("“") || cad.EndsWith("”") || cad.EndsWith("'") || cad.EndsWith("&quot;") || cad.EndsWith("&ldquo;") || cad.EndsWith("&rdquo;") || cad.EndsWith("’"))
					&&
					(cad.StartsWith("\"") || cad.StartsWith("”") || cad.StartsWith("“") || cad.StartsWith("'") || cad.EndsWith("&quot;") || cad.EndsWith("&ldquo;") || cad.EndsWith("&rdquo;") || cad.StartsWith("‘")))
				{
						cad = cad.Substring(0, cad.Length - 1);
						if (cad.Length > 0)
							cad = cad.Substring(1);
				}
			}
			return cad;
		}


		public static string ChequearYRemoverInicio(string cad, string start)
		{
			if (cad.StartsWith(start))
				return cad.Substring(start.Length);
			else
				return cad;
		}

		public static string EatUpTo(string cad, string sep, bool eatSeparator)
		{
			int n = cad.IndexOf(sep);
			if (n == -1) return cad;
			if (eatSeparator) n += sep.Length;
			return cad.Substring(n);
		}

		public static string RemoveFormatTags(string tmp)
		{
			string tmp2 = Path.GetTempFileName();
			string cad = File.ReadAllText(tmp, Encoding.Default);
			cad = Http.StripTag(cad, "p", "<br>");
			cad = Http.StripTag(cad, "font");
			cad = Http.StripTag(cad, "span");
			cad = Http.StripTag(cad, "b");
			cad = Http.StripTag(cad, "i");
			cad = Http.StripTag(cad, "strong");
			cad = Http.StripTag(cad, "center");
			cad = Http.StripTag(cad, "h3", "<br>");
			cad = Http.StripTag(cad, "h2", "<br>");
			cad = Http.StripTag(cad, "h1", "<br>");
			cad = Http.StripTag(cad, "blockquote");

			cad = cad.Replace("<br />", "<br>");
			cad = cad.Replace("<br><br>", "<br>");
			cad = cad.Replace("<br>", "</p><p>");

			File.WriteAllText(tmp2, cad, Encoding.Default);
			return tmp2;
		}

		public static string StripTag(string cad, string tag, string replacement = "")
		{
			cad = Regex.Replace(cad, "<" + tag + " .*?>", replacement);
            cad = Regex.Replace(cad, "<" + tag + ">", replacement);
			cad = Regex.Replace(cad, "</" + tag + ">", replacement);
			return cad;
		}


        public static string CutFrom(string cad, string sep, bool includeSeparator)
        {
            int n = cad.IndexOf(sep);
            if (n == -1) return cad;
            if (includeSeparator) n += sep.Length;
            return cad.Substring(0, n);
        }


				public static string[] SplitOnce(string text, string MesaSeparator)
				{
					int n = text.IndexOf(MesaSeparator);
					if (n == -1)
						return new string[] { text };
					else
						return new string[] { text.Substring(0, n), text.Substring(n + MesaSeparator.Length) };
				}

				public static string Capitalize(string p)
				{
					string ret = p.Substring(0, 1).ToUpper() + p.Substring(1).ToLower();
					return ret;
				}

				public static string[] SplitOutSideParenthesis(string autores, char separator)
				{
					int inPare = 0;
					List<string> ret = new List<string>();
					string buffer = "";
					foreach (char a in autores)
					{
						if (a == '(') inPare++;
						if (a == '[') inPare++;
						if (a == ',' && inPare == 0)
						{
							ret.Add(buffer);
							buffer = "";
						}
						else
							buffer += a;
						if (a == ')') inPare--;
						if (a == ']') inPare--;
					}
					ret.Add(buffer);
					return ret.ToArray();
				}
	}
	public enum CleanExtraOptions
	{
		opNinguna = 0,
		opRemoverDosPuntosFinales = 1,
		opRemoverComillas = 2,
		opRemoverPuntoFinal = 4,
		opRemoverBloqueParentesisFinal = 8,
		opRemoverMesaDosPuntos = 16,
        opRemoverPuntoInicial = 32,
				opRemoverDosPuntosIniciales = 64,
				opKeepBRAsNL = 128,
				opRemoverComaFinal = 256,
				opRemoverParentesisInicioFin = 512

	}
}
