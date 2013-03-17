using System;
using NUnit.Framework;
using mtangle;

namespace tests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void BadArgCountsThrow ()
		{
			Assert.Throws<ArgumentException>(
				delegate 
				{ 
					MainClass.Main(new String[] {});
				});

			Assert.Throws<ArgumentException>(
				delegate 
				{ 
				MainClass.Main(new String[] {"justOne"});
			});

			Assert.Throws<ArgumentException>(
				delegate 
				{ 
				MainClass.Main(new String[] {"too", "many", "args"});
			});

		}

		[Test()]
		public void FixesHTMLEscapes()
		{
			string bad = "Some text &lt;element&gt;";
			string good = MainClass.FixHTMLCode(bad);
			Assert.AreEqual("Some text <element>", good);
		}

		[Test()]
		public void FindsChunk()
		{
			string enchunked = "<p>OK, here's my first chunk, which is the \"chunk\" section</p>" +
				"<pre id=\"chunk\">" +
				"chunk" + 
				"</pre>";

			string chunk = MainClass.GetChunk(enchunked, "chunk");
			Assert.AreEqual("chunk", chunk);
		}

		[Test()]
		public void ReturnsEmptyIfNoChunk()
		{
			string noChunk = "<p>adfadfadfadfasdfasdf chunk asdfasdfadsfasdfasdf</p>";
			string empty = MainClass.GetChunk(noChunk, "chunk");
			Assert.IsEmpty(empty);
		}

		[Test()]
		public void ResolvesGetChunkAsChunkWhenNoGetChunk()
		{
			string enchunked = "<p>OK, here's my first chunk, which is the \"chunk\" section</p>" +
				"<pre id=\"chunk\">" +
					"chunk" + 
					"</pre>";
			string same = MainClass.ResolveGetChunks(enchunked, MainClass.GetChunk(enchunked, "chunk"));
			Assert.AreEqual("chunk", same);
		}

		[Test()]
		public void ResolvesGetChunks()
		{

			string enchunked = "<p>OK, here's my first chunk, which is the \"chunk\" section</p>" +
				"<pre id=\"chunk\">" +
					"chunk" + 
					"</pre> " + 
			"<p>And here's my second chunk, which has a getchunk of the first chunk</p>\n" + 
					"<pre id=\"second\">" +
					"pre" + 
					"<getchunk id=\"chunk\"/>" +
					"post" +
				"\n</pre>";
			string got = MainClass.ResolveGetChunks(enchunked, MainClass.GetChunk(enchunked, "second"));
			Assert.AreEqual("prechunkpost\n", got);
		}
	}
}

