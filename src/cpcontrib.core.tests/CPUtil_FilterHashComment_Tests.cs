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
	public class CPUtil_FilterHashComment_Tests
	{

		[Test]
		[TestCase("no hash test", "no hash test")]
		public void FilterHashComments_NoHash_allstring(string expected, string actual)
		{
			var processed = Invoke_FilterHashComments(expected);

			processed.First().Should().Be(actual);
		}

		[Test]
		[TestCase("#begin hash")]
		public void FilterHashComments_BeginsHash_stringempty(string expected)
		{
			var processed = Invoke_FilterHashComments(expected);

			processed.Should().BeEmpty();
		}

		[Test]
		[TestCase("hash '\\#' test", "hash '\\#' test")]
		public void FilterHashComments_EscapedHash_allstring(string expected, string actual)
		{
			var processed = Invoke_FilterHashComments(expected);

			processed.First().Should().Be(actual);
		}

		[Test]
		[TestCase("a#comment", "a")]
		//[TestCase("hash test #comment", "hash test ")]
		public void FilterHashComments_Common(string expected, string actual)
		{
			var processed = Invoke_FilterHashComments(expected);

			processed.First().Should().Be(actual);
		}

		IEnumerable<string> Invoke_FilterHashComments(string expected)
		{
			return CPUtil.FilterHashComments(new string[] { expected });
		}

	}
}
