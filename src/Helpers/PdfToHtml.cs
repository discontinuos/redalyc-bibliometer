using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ActaAcademica
{
	public class PdfToHtml
	{
		private static bool initialized = false;

		protected const string Qpdf = @"..\..\lib\qpdf\qpdf.exe";

		private static void checkInitialized()
		{
			if (initialized == false)
			{
				Initialize();
			}
		}

		private static void Initialize()
		{
			if (File.Exists(PdfToHtmlExe) == false)
			{
				throw new Exception("No se encontró Pdftohtml aquí: " + PdfToHtmlExe);
			}
			initialized = true;
		}
		protected static string PdfToHtmlExe
		{
			get
			{
				return @"..\..\..\..\..\src\cgi-bin\pdftohtml32.exe";
			}
		}
		// -i ignore images
		protected static string PdfToHtmlFirstPageParams = "-f 1 -l 1 -noframes -i \"{0}\" \"{1}\"";
		protected static string PdfToHtmlParams = "-noframes -i \"{0}\" \"{1}\"";

		public static string GetPdfHtml(string pdf, bool firstPageOnly = false)
		{
			string tmp = Path.GetTempFileName() + ".html";
			PdfToHtml.RunPdfToHtml(pdf, tmp, firstPageOnly);
			return File.ReadAllText(tmp, Encoding.Default);
		}
		public static string[] GetPdfLines(string pdf)
		{
			string text = GetPdfHtml(pdf);
			return splitLines(text);
		}

		private static string[] splitLines(string text)
		{
			return text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
		}
		public static List<string[]> GetPdfPages(string pdf, bool firstPageOnly = false)
		{
			string text = GetPdfHtml(pdf, firstPageOnly);
			string[] pages = text.Split((char)0x0C);
			List<string[]> ret = new List<string[]>();
			foreach (string page in pages)
				ret.Add(splitLines(page));
			return ret;
		}
		public static string[] GetPdfFirstPageLines(string pdf)
		{
			return GetPdfPages(pdf, true)[0];
		}

		public static int RunPdfToHtml(string pdf, string txt, bool firstPageOnly = false)
		{
			checkInitialized();
			ProcessStartInfo pr = new ProcessStartInfo();
			string args;
			if (firstPageOnly)
				args = PdfToHtmlFirstPageParams;
			else
				args = PdfToHtmlParams;
			pr.Arguments = string.Format(args, pdf, txt);
			pr.FileName = PdfToHtmlExe;
			pr.WindowStyle = ProcessWindowStyle.Hidden;
			pr.CreateNoWindow = true;
			using (Process proc = Process.Start(pr))
			{
				proc.WaitForExit();
				return proc.ExitCode;
			}
			throw new Exception();
		}
	}
}
