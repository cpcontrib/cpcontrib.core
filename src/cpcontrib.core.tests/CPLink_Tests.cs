using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;

using CPContrib.Core;
using CPContrib.Core.Internals;

namespace cpcontrib.core.tests
{
	[TestFixture]
	public class CPLink_Tests
	{

		[Test]
		public void ExternalToString_Returns_ExpectedToString()
		{
			//arrange
			var asset = new MockFieldAccessor();
			asset["basename_link_type"] = "External";
			asset["basename_link_external"] = "http://localhost/";

			var cplink = new CPLink(asset, "basename");

			//act
			var actual = cplink.ToString();

			//assert
			actual.Should().Be("CPLink { Type=External Href=\"http://localhost/\" }");
		}

		[Test]
		public void ExternalGetExternalHref_Returns_Value()
		{
			//arrange
			var asset = new MockFieldAccessor();
			asset["basename_link_type"] = "External";
			asset["basename_link_external"] = "http://localhost/";

			var cplink = new CPLink(asset, "basename");

			//act
			var actual = cplink.GetExternalHref();

			//assert
			actual.Should().Be("http://localhost/");
		}

		public class MockFieldAccessor : CrownPeak.CMSAPI.CustomLibrary.IFieldAccessor
		{
			public class MockRawWrapper : RawWrapper
			{
				public MockRawWrapper(MockFieldAccessor parent)
				{
					this._Parent = parent;
				}
				private MockFieldAccessor _Parent;

				public override string this[string key]
				{
					get { return _Parent[key]; }
				}

			}

			public MockFieldAccessor()
			{
				_MockRawWrapper = new MockRawWrapper(this);
			}
			private MockRawWrapper _MockRawWrapper;
			private Dictionary<string, string> _Dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

			public override string this[string key]
			{
				get
				{
					string value;
					if(_Dictionary.TryGetValue(key, out value) == true)
						return value;
					else
						return "";
				}
				set
				{
					this._Dictionary[key] = value;
				}
			}
			public MockFieldAccessor Set(string key, string value)
			{
				this._Dictionary[key] = value;
				return this;
			}





			public override RawWrapper Raw
			{
				get
				{
					return this._MockRawWrapper;
				}
			}
		}

	}
}
