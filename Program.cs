using System;

/*
 Port of http://axiom-developer.org/axiom-website/litprog.html to C# / mono
*/
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace mtangle
{
	/*
		Of course this would be better if it were XML parsed, but this is a straight port of a text strategy
	*/
	public class MainClass
	{
		static readonly string chunkStart = "<pre id=\"{0}\">";
		static readonly string chunkEnd = "</pre>";           

		static readonly string chunkGetForm = "<getchunk id=\"?(.*?)\"/>";


		public static void Main (string[] args)
		{
			if(args.Length < 2 || args.Length > 2)
			{
				throw new ArgumentException("Usage: tangle filename chunkname");
			}

			StreamReader streamReader = new StreamReader(args[0]);
			string html = streamReader.ReadToEnd();
			streamReader.Close();
			string htmlFixed = FixHTMLCode(html); 
			string code = GetChunk(htmlFixed, args[1]);
			Console.WriteLine (code);
		}

		public static string FixHTMLCode(string html)
		{
			var sansLT = html.Replace("&lt;", "<");
			var sansGT = sansLT.Replace("&gt;", ">");
			return sansGT;
		}

		public static string GetChunk(string html, string chunkName)
		{
			//Would be better if I could rely on XML parsing, but I'm just goin to hard-code in strict text
			var chunkTag = String.Format (chunkStart, chunkName);
			var chunkLocation = html.IndexOf(chunkTag);
			if(chunkLocation >= 0)
			{
				//Found it
				var postChunk = html.Substring(chunkLocation + chunkTag.Length);
				var chunkWithPossibleGetChunks = postChunk.Substring(0, postChunk.IndexOf(chunkEnd));
				var chunk = ResolveGetChunks(html, chunkWithPossibleGetChunks);
				return chunk;
			}
			else
			{
				//No chunk. Return empty (Or should it throw?)
				return "";
			}
		}

		public static string ResolveGetChunks(string html, string chunk)
		{
			var matches = Regex.Matches(chunk, chunkGetForm);
			if(matches.Count > 0)
			{
				var replaced = chunk;
				foreach(Match match in matches)
				{
					var innerChunkName = match.Groups[1].Value;
					var innerChunk = GetChunk(html, innerChunkName);
					replaced = replaced.Replace(match.Groups[0].Value, innerChunk);
				}
				return replaced;
			}
			else
			{
				return chunk;
			}
		}
	}
}
