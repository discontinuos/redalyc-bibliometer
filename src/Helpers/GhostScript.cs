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
	public class GhostScript
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
			if (File.Exists(GhostScriptExe) == false)
			{
				throw new Exception("No se encontró GhostScript aquí: " + GhostScriptExe);
			}
			initialized = true;
		}
		public enum PdfMode
		{
			Raw,
			Layout
		}
		protected static string GhostScriptExe
		{
			get
			{
				string cad = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToString();
				if (cad == "x86")
					return @"..\..\..\..\..\src\cgi-bin\gswin32c.exe";
				else
					return @"..\..\..\..\..\src\cgi-bin\gswin64c.exe";
			}
		}
		protected static string GhostScriptParamsLayout = "-q -dNOPAUSE -dBATCH -sDEVICE=pdfwrite -dFirstPage=2 -sOutputFile=\"{1}\" \"{0}\" ";

		public static string RemoveFirstPage(string pdf)
		{
			string tmp = Path.GetTempFileName() + ".pdf";
			GhostScript.RunGhostScript(pdf, tmp);
			return tmp;
		}


		public static int RunGhostScript(string pdf, string txt)
		{
			checkInitialized();
			ProcessStartInfo pr = new ProcessStartInfo();
			string args;
			args = GhostScriptParamsLayout;
			pr.Arguments = string.Format(args, pdf, txt);
			pr.FileName = GhostScriptExe;
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
