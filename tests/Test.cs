using System;
using NUnit.Framework;
using mtangle;

namespace tests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void BadArgsThrow ()
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
	}
}

