using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;

namespace ActaAcademica
{
	public class PdfToText
	{
		private static bool initialized = false;

		private static void checkInitialized()
		{
			if (initialized == false)
			{
				Initialize();
			}
		}

		private static void Initialize()
		{
			if (File.Exists(PdfToTextExe) == false)
			{
				throw new Exception("No se encontró Pdftotext aquí: " + PdfToTextExe);
			}
			initialized = true;
		}
		public enum PdfMode
		{
			Raw,
			Layout
		}
		protected static string PdfToTextExe
		{
			get
			{
				string path = Assembly.GetExecutingAssembly().Location;
				string folder = Path.GetDirectoryName(path);
				string path1 = Path.Combine(folder, "pdftotext32.exe");
				string path2 = Path.Combine(folder, @"..\..\lib\pdftotext\pdftotext32.exe");
				if (File.Exists(path2))
					return path2;
				if (File.Exists(path1))
					return path1;
				return path1;
			}
		}
		//qpdf --empty --linearize --pages {in.pdf} {desde}-{hasta} -- {out.pdf}
		protected static string QpdfParams = "--empty --linearize --pages \"{0}\" {1}-{2} -- \"{3}\"";

		//protected string PdfToTextParamsRaw = "-enc UTF-8 \"{0}\" \"{1}\"";
		protected static string PdfToTextParamsRaw = "-raw -enc UTF-8 \"{0}\" \"{1}\"";
		protected static string PdfToTextParamsLayout = "-layout -enc UTF-8 \"{0}\" \"{1}\"";

		public static bool LastWasCached = false;
		public static string GetPdfText(string pdf, PdfMode mode = PdfMode.Raw)
		{
			LastWasCached = false;
			string existingPdf = pdf.Substring(pdf.Length - 3) + "txt";
			if (File.Exists(existingPdf))
			{
				LastWasCached = true;
				return File.ReadAllText(existingPdf);
			}

			string tmp = Path.GetTempFileName() + ".txt";
			PdfToText.RunPdfToText(pdf, tmp, mode);
			string text = File.ReadAllText(tmp);
			File.Delete(tmp);
			return text;
		}
		public static string[] GetPdfLines(string pdf, PdfMode mode = PdfMode.Raw)
		{
			string text = GetPdfText(pdf, mode);
			return splitLines(text);
		}

		private static string[] splitLines(string text)
		{
			return text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
		}
		public static List<string[]> GetPdfPages(string pdf, PdfMode mode = PdfMode.Raw)
		{
			string text = GetPdfText(pdf, mode);
			string[] pages = text.Split((char)0x0C);
			List<string[]> ret = new List<string[]>();
			foreach (string page in pages)
				ret.Add(splitLines(page));
			return ret;
		}
		public static string[] GetPdfFirstPageLines(string pdf, PdfMode mode = PdfMode.Raw)
		{
			return GetPdfPages(pdf, mode)[0];
		}

		public static int RunPdfToText(string pdf, string txt, PdfMode mode = PdfMode.Raw)
		{
			checkInitialized();
			ProcessStartInfo pr = new ProcessStartInfo();
			string args;
			if (mode == PdfMode.Raw)
				args = PdfToTextParamsRaw;
			else
				args = PdfToTextParamsLayout;
			pr.Arguments = string.Format(args, pdf, txt);
			pr.FileName = PdfToTextExe;
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
