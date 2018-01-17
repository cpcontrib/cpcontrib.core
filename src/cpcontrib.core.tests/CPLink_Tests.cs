using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPContrib.Core.FieldAccessors;
using FluentAssertions;
using CrownPeak.CMSAPI.Custom_Library;

using CPContrib.Core;

namespace cpcontrib.core.tests
{
	[TestFixture]
	public class CPLink_Tests
	{

		[Test]
		public void External_ToString()
		{
			//arrange
			var fa = new MockFieldAccessor();
			fa.Set("basename_link_type", "External");
			fa.Set("basename_link_external", "http://localhost/");

			var cplink = new CPLink(fa, "basename");

			//act
			var actual = cplink.ToString();

			//assert
			actual.Should().Be("CPLink { Type=External Href=\"http://localhost/\" }");
		}

		[Test]
		public void External_GetExternalHref()
		{
			//arrange
			var fa = new MockFieldAccessor();
			fa.Set("basename_link_type", "External");
			fa.Set("basename_link_external", "http://localhost/");

			var cplink = new CPLink(fa, "basename");

			//act
			var actual = cplink.GetExternalHref();

			//assert
			actual.Should().Be("http://localhost/");
		}

		public class MockFieldAccessor : CPContrib.Core.FieldAccessors.IFieldAccessor
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
