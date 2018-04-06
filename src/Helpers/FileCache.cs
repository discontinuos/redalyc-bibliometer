using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ActaAcademica
{
	public class FileCache
	{
		static Dictionary<string, string> files;
		static  string dictPath
		{
			get
			{
				string folder = dictFolder;
				return Path.Combine(folder, "files.dat");
			}
		}

		private static string dictFolder
		{
			get {
			string folder = Path.Combine(Path.GetTempPath(), "ChupaCache");
			if (Directory.Exists(folder) == false)
				Directory.CreateDirectory(folder);
			return folder;
			}
		}

		static void CheckFileList()
		{
			if (files == null)
			{
				if (File.Exists(dictPath) == false)
					files = new Dictionary<string, string>();
				else
				{
					LoadCache();
				}
			}
		}

		private static void LoadCache()
		{
			files = (Dictionary<string, string>)Deserialize(dictPath);
		}

		private static object Deserialize(string file)
		{
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			return bf.Deserialize(new FileStream(file, FileMode.Open));
		}
		public static bool HasFile(string url)
		{
			CheckFileList();
			return (files.ContainsKey(url));
		}

		public static void GetFileTo(string url, string tmp)
		{
			File.Copy(files[url], tmp, true);
		}

		public static void SaveFile(string url, string tmp)
		{
			CheckFileList();
			if (url.ToLower().StartsWith("http://acta-academica") || !File.Exists(tmp))
				return;
			string filename = Path.Combine(dictFolder, Directory.GetFiles(dictFolder).Count().ToString());
			File.Copy(tmp, filename, true);
			files.Add(url, tmp);

			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

			SaveCache(bf);
		}

		private static void SaveCache(System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf)
		{
			using (FileStream fs = new FileStream(dictPath, FileMode.OpenOrCreate))
			{
				bf.Serialize(fs, files);
				fs.Flush();
			}
		}

		public static void Clear()
		{
			files = null;
			File.Delete(dictPath);
		}
	}
}
