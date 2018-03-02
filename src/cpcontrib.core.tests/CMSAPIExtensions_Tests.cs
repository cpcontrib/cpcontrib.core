using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using CrownPeak.CMSAPI;
using FluentAssertions;

namespace cpcontrib.core.tests
{
	[TestFixture]
	public class CMSAPIExtensions_Tests
	{

		[Test]
		public void AssetPath_GetParent()
		{
			//arrange
			AssetPath assetPath = new AssetPath("/Site1/Folder1/Folder2/Asset1");
			var expected = "/Site1/Folder1/Folder2";

			//act
			var actual_assetpath = assetPath.GetParent();
			var actual = actual_assetpath.ToString();

			//assert
			actual.Should().Be(expected);
		}

	}

}
