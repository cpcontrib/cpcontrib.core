using CrownPeak.CMSAPI.Custom_Library;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;
using CrownPeak.CMSAPI;

namespace cpcontrib.core.tests
{
	[TestFixture]
	public class AssetPathResolver_Tests
	{

		[SetUp]
		public void SetUp()
		{
			AssetPathResolver = new AssetPathResolver();

			AssetPathResolver.AddSpecialPath("$/Widgets", "~/_Config/Widgets");
			AssetPathResolver.AddSpecialPath("$/Assets", (ap) => "/" + ap[0] + "/_Assets");
			AssetPathResolver.AddSpecialPath("$/FailurePathTest", (ap) => { throw new InvalidOperationException(); });

			_PathOfAsset1 = new AssetPath("/Site1/SubFolder1/Asset1");
			_PathOfAsset1a = new AssetPath("/Site1/SubFolder1/SubFolderA/Asset1A");

			_PathOfAsset2 = new AssetPath("/Site1/SubFolder1/Asset2");
			_PathOfAsset2a = new AssetPath("/Site1/SubFolder1/SubFolder2/Asset2A");

			_PathOfAsset3 = new AssetPath("/Site1/SubFolder1/SubFolder2/SubFolder3/Asset3");

		}

		private AssetPathResolver AssetPathResolver;
		private AssetPath _PathOfAsset1;
		private AssetPath _PathOfAsset1a;
		private AssetPath _PathOfAsset2;
		private AssetPath _PathOfAsset2a;
		private AssetPath _PathOfAsset3;


		[Test]
		public void Resolve_SpecialPath_Dollar()
		{
			//arrange

			//act
			var actual = AssetPathResolver.Resolve(_PathOfAsset1, "$/Widgets/widget1");

			//assert
			actual.Should().Be("/Site1/_Config/Widgets/widget1");
		}
		[Test]
		public void Resolve_SpecialPath_ThrowsException()
		{
			//arrange

			//act
			Assert.Throws<InvalidOperationException>(() =>
			{
				var actual = AssetPathResolver.Resolve(_PathOfAsset1, "$/FailurePathTest/this_should_fail");
			});

			//assert
		}


		[Test]
		public void Resolve_Dot()
		{
			//arrange
			//var resolver = AssetPathResolver;

			//act
			var actual = AssetPathResolver.Resolve(new AssetPath("/Site1/SubFolder1/Asset1"), ".");


			//assert
			actual.Should().Be("/Site1/SubFolder1");
		}

		[Test]
		public void Resolve_DotDot()
		{
			//arrange
			//var resolver = AssetPathResolver;

			//act
			var actual = AssetPathResolver.Resolve(new AssetPath("/Site1/SubFolder1/Asset1"), "..");


			//assert
			actual.Should().Be("/Site1");
		}

		[Test]
		public void Resolve_SpecialFolder()
		{
			//arrange
			//var resolver = AssetPathResolver;

			//act
			var actual = AssetPathResolver.Resolve(new AssetPath("/Site1/SubFolder1/Asset1"), "$/Assets");

			//assert
			actual.Should().Be("/Site1/_Assets");
		}

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
