using CrownPeak.CMSAPI.Custom_Library;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

namespace cpcontrib.core.tests
{
	[TestFixture]
	public class CPUtil_Tests
	{


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

	}
}
