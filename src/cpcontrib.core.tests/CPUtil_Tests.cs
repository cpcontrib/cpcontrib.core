using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using FluentAssertions;
using CPContrib.Core;

namespace cpcontrib.core.tests
{
	[TestFixture]
	public class CPUtil_Tests
	{

		#region MakeDictionary
		[Test]
		public void MakeDictionary_duplicate_values_throws()
		{
			//arrange
			string[] values = { "There", "There" };

			//act
			Assert.Throws<ArgumentException>(() =>
			{
				CPUtil.MakeDictionary(values);
			});
			//assert
		}

		[Test]
		public void MakeDictionary_no_args_returns_empty()
		{
			//arrange 
			string[] values = null;

			//act
			var actual = CPUtil.MakeDictionary(values);

			//assert
			actual.Should().NotBeNull();
			actual.Should().BeEmpty();
		}

		[Test]
		[TestCase("One,Two,Three")]
		public void MakeDictionary_returns_keyvalue_paired(string valuesSeparated)
		{
			//arrange
			string[] values = valuesSeparated.Split(',');

			//act
			var actual = CPUtil.MakeDictionary(values);

			//assert
			actual.ShouldAllBeEquivalentTo(values.Select(val => new KeyValuePair<string, string>(val, val)));



		}
		#endregion

		#region EmptyFallbacks

		[Test]
		public void EmptyFallbacks_no_params_returns_null()
		{
			//arrange


			//act
			var actual = CPUtil.EmptyFallback((string[])null);

			//assert
			actual.Should().BeNull();
		}

		[Test]
		public void EmptyFallbacks()
		{
			//arrange
			var values = new string[] { null, "", "One" };

			//act
			var actual = CPUtil.EmptyFallback(values);

			//assert
			actual.Should().Be("One");
		}

		//[Test]
		//public void EmptyFallbacks_bad_funcs()
		//{
		//	Assert.Throws<ApplicationException>(() => {
		//		CPUtil.EmptyFallback(() => { throw new ApplicationException(); });
		//	});

		//}

		//[Test]
		//public void EmptyFallbacks_bad_funcs2()
		//{
		//	Assert.Throws<ApplicationException>(() => {
		//		CPUtil.EmptyFallback(
		//			() => "",
		//			() => { throw new ApplicationException(); },
		//			"");
		//	});

		//}


		#endregion

		/*
				[Test]
				public void TestMethod()
				{
					//arrange

					//act

					//assert
				}
		*/

	}
}
