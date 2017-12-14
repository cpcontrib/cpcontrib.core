using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;
using datagen;

using CrownPeak.CMSAPI.Custom_Library;

namespace cpcontrib.core.tests
{
	[TestFixture]
	public class TextUtil_Tests
	{

		const string SAMPLE_PARAGRAPH = "The quick brown fox jumps over the brown cow with zeal. Once you realize the reality is that there is no spoon.";
		string[] _WordsList = SAMPLE_PARAGRAPH.Split(' ');

		[Test]
		public void GetWordCount_returns_correct()
		{
			//arrange
			int expected = 22; //22 words in the _WordList
			string wordstring = String.Join(" ", _WordsList);


			//act
			int wordCount = TextUtil.GetWordCount(wordstring);

			//assert
			wordCount.Should().Be(expected);
		}

		[Test]
		[TestCase(SAMPLE_PARAGRAPH, 22)]
		[TestCase("The",1)]
		[TestCase("The quick",2)]
		[TestCase("The quick.", 2)]
		[TestCase("The quick. ", 2)]
		[TestCase("The quick brown", 3)]
		[TestCase("The quick brown.", 3)]
		[TestCase("The quick brown. ", 3)]
		public void GetWordCount_returns_correct(string wordstring, int expected)
		{
			//arrange
			//int expected = 22; //22 words in the _WordList


			//act
			int wordCount = TextUtil.GetWordCount(wordstring);

			//assert
			wordCount.Should().Be(expected);
		}

		[Test]
		[TestCase(10)]
		[TestCase(11)]
		[TestCase(12)]
		[TestCase(13)]
		[TestCase(14)]

		public void WholeWordCrop(int length)
		{
			//arrange
			var expected = "The quick...";
			string wordstring = SAMPLE_PARAGRAPH;


			//act
			string actual = TextUtil.WholeWordCrop(wordstring, length, appendEllipsis: true);

			//assert
			actual.Should().Be(expected);

		}

		[Test]
		[TestCase(10)]
		public void WholeWordCrop_with_ellipsis(int length)
		{
			//arrange
			var expected = "The quick...";
			string wordstring = SAMPLE_PARAGRAPH;


			//act
			string actual = TextUtil.WholeWordCrop(wordstring, length, appendEllipsis: true);

			//assert
			actual.Should().Be(expected);
		}

		[Test]
		[TestCase(10)]
		public void WholeWordCrop_without_ellipsis(int length)
		{
			//arrange
			var expected = "The quick";
			string wordstring = SAMPLE_PARAGRAPH;


			//act
			string actual = TextUtil.WholeWordCrop(wordstring, length, appendEllipsis: false);

			//assert
			actual.Should().Be(expected);

		}



	}
}
