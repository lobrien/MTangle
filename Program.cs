using System;

/*
 Port of http://axiom-developer.org/axiom-website/litprog.html to C# / mono
*/
using System.IO;


namespace mtangle
{
	public class MainClass
	{
		static readonly string chunkBegin = "<pre id=\"";
		static readonly int chunkBeginLength = 9;
		static readonly string chunkEnd = "</pre>";           
		static readonly int chunkEndLengthgth = 6;
		static readonly string chunkGet = "<getchunk id=\"";  
		static readonly int chunkGetLength = 14;

		public static void Main (string[] args)
		{
			if(args.Length < 2 || args.Length > 2)
			{
				throw new ArgumentException("Usage: tangle filename chunkname");
			}

			StreamReader streamReader = new StreamReader(args[0]);
			string html = streamReader.ReadToEnd();
			streamReader.Close();
			string htmlFixed = fixHTMLCode(html); 
			string code = getchunk(htmlFixed, args[1]);
			Console.WriteLine (code);
		}

		static string fixHTMLCode(string html)
		{
			return html;
		}

		static string getchunk(string html, string chunkName)
		{
			return html;
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
//		void fixHTMLcode() {
//			int point = 0;
//			int mark = 0;
//			int i=0;
//			for(point = 0; point < bufsize;) {
//				if ((buffer[point] == '&') &&
//				    (strncmp(&buffer[point+1],"lt;",3) == 0)) {
//					buffer[mark++] = 60;
//					point = point + 4;  
//				} else
//					if ((buffer[point] == '&') &&
//					   (strncmp(&buffer[point+1],"gt;",3) == 0)) {
//					buffer[mark++] = 62;
//					point = point + 4;  
//				} else
//					buffer[mark++] = buffer[point++]; 
//			}
//			bufsize = mark;
//		}
//		
//		/* memory map the input file into the global buffer and get the chunk */
//		int main(int argc, char *argv[]) {
//		}
//

	}
}
