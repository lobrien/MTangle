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

//		static int bufsize = 0;
//
//		
//		/* return the length of the next line */
//		int nextline(int i) {
//			int j;
//			if (i >= bufsize) return(-1);
//			for (j=0; ((i+j < bufsize) && (buffer[i+j] != '\n')); j++);
//			return(j);
//		}
//		
//		/* output the line we need */
//		int printline(int i, int length) {
//			int j; 
//			for (j=0; j<length; j++) { putchar(buffer[i+j]); }
//			Console.WriteLine("\n");
//		}
//		
//		/* handle <pre id="chunkname">            */
//		/* is this chunk name we are looking for? */
//		int foundchunk(int i, char *chunkname) {
//			if ((strncmp(&buffer[i],chunkBegin,chunkBeginLength) == 0) &&
//			    (strncmp(&buffer[i+9],chunkname,strlen(chunkname)) == 0) &&
//			    (buffer[i+chunkBeginLength+strlen(chunkname)] == '"') &&
//			    (buffer[i+chunkBeginLength+strlen(chunkname)+1] == '>')) return(1);
//			return(0);
//		}
//		
//		/* handle </pre>   */
//		/* is it really an end? */
//		int foundEnd(int i) {
//			if (strncmp(&buffer[i],chunkEnd,chunkEndlen) == 0) {
//				return(1); 
//			}
//			return(0);
//		}
//		
//		/* handle <getchunk id="chunkname"/> */
//		/* is this line a getchunk?          */
//		int foundGetchunk(int i, int linelen) {
//			int len;
//			if (strncmp(&buffer[i],chunkget,chunkgetlen) == 0) {
//				for(len=1; ((len < linelen) && (buffer[i+chunkgetlen+len] != '\"')); len++);
//				return(len);
//			}
//			return(0);
//		}
//		
//		/* Somebody did a getchunk and we need a copy of the name */
//		/* malloc string storage for a copy of the getchunk name  */
//		char *getChunkname(int k, int getlen) {
//			char *result = (char *)malloc(getlen+1);
//			strncpy(result,&buffer[k+chunkgetlen],getlen);
//			result[getlen]='\0';
//			return(result);
//		}
//		
//		/* print lines in this chunk, possibly recursing into getchunk */
//		int printchunk(int i, int chunklinelen, char *chunkname) {
//			int j;
//			int k;
//			int linelen;
//			char *getname;
//			int getlen = 0;
//			for (k=i+chunklinelen+1; ((linelen=nextline(k)) != -1); ) {
//				if ((getlen=foundGetchunk(k,linelen)) > 0) {
//					getname = getChunkname(k,getlen);
//					getchunk(getname);
//					free(getname);
//					k=k+getlen+17;
//				} else {
//					if ((linelen >= chunkEndlen) && (foundEnd(k) == 1)) {
//						return(k+chunkBeginLength);
//					} else {
//						printline(k,linelen);
//						k=k+linelen+1;
//					}
//				}}
//			return(k);
//		}
//		
//		/* find the named chunk and call printchunk on it */
//		int getchunk(char *chunkname) {
//			int i;
//			int j;
//			int linelen;
//			int chunklen = strlen(chunkname);
//			for (i=0; ((linelen=nextline(i)) != -1); ) {
//				if ((linelen >= chunklen+11) && (foundchunk(i,chunkname) == 1)) {
//					i=printchunk(i,linelen,chunkname);
//				} else {
//					i=i+linelen+1;
//				}
//			}
//			return(i);
//		}
//		
	
//		
//		/* memory map the input file into the global buffer and get the chunk */
//		int main(int argc, char *argv[]) {
//		}
//

	}
}
