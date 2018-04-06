using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblio
{
	[Serializable]
	class SaveState
	{
		public List<Article> Results = new List<Article>();
		public string Search;
		public List<int> Topics = new List<int>();
		public List<string> Years = new List<string>();
		public string Match1;
		public string Match2;
	}
}
