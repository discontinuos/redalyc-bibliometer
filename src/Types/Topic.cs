using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio
{
	[Serializable]

	class Topic
	{
		public string Name;
		public int Id;
		public Topic(string name, int id)
		{
			Name = name;
			Id = id;
		}
		public override string ToString()
		{
			return Name;
		}
	}
}
