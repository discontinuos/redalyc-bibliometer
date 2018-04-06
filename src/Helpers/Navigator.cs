using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace ActaAcademica
{
	public class Navigator
	{


		public static HtmlNode GetNextLinkRecursiveFrom(HtmlNode node, bool includeSelf = false)
		{
			var next = node.NextSibling;
			if (includeSelf) next = node;
			for (int deep = 1; deep <= 7; deep++)
			{
				foreach (var href in from href in next.Descendants()
														 where href != null && href.Name == "a"
														 select href)
				{
					return href;
				}
				next = next.NextSibling;
				if (next == null) break;
			}
			return null;
		}
		public static HtmlNode GetNextSiblingWithText(HtmlNode node)
		{
			var next = node.NextSibling;
			for (int deep = 1; deep <= 10; deep++)
			{
				if (next.InnerText.Trim() != "")
					return next;
				next = next.NextSibling;
				if (next == null) break;
			}
			return null;
		}

		public static HtmlNode GetFirstAncestorBeing(HtmlNode node, string p)
		{
			var next = node.ParentNode;
			for (int deep = 1; deep <= 20; deep++)
			{
				if (next.Name.Trim() == p)
					return next;
				next = next.ParentNode;
				if (next == null) break;
			}
			return null;
		}
		public static HtmlNode GetNextNodeWithContent(HtmlNode node)
		{
			while (true)
			{
				if (node.NextSibling == null)
				{

					return null;
				}
				node = node.NextSibling;
				if (Http.CleanHtmlTextSpaces(node.InnerText) != "")
					return node;
			}

		}
	}
}
