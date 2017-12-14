using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrownPeak.CMSAPI.Custom_Library
{
	public static class TextUtil
	{

		private static readonly HashSet<char> DefaultNonWordCharacters = new HashSet<char> { ',', '.', ':', ';' };
		private static bool IsWhitespace(char character)
		{
			return character == ' ' || character == '\n' || character == '\t';
		}

		public static int GetWordCount(string text)
		{
			int wordCount = 0, index = 0;

			while(index < text.Length)
			{
				// check if current char is part of a word
				while(index < text.Length && IsWhitespace(text[index]) == false)
					index++;

				wordCount++;

				// skip whitespace until next word
				while(index < text.Length && IsWhitespace(text[index]))
					index++;
			}

			return wordCount;
		}

		[Obsolete()]
		public static string CropToWordCount(string text, int wordCount)
		{
			if(string.IsNullOrEmpty(text)) return "";

			StringBuilder sb = new StringBuilder();
			int foundWords = 0, index = 0;

			bool whitespaceAdded = false;
			while(index < text.Length && foundWords < wordCount)
			{
				// check if current char is part of a word
				while(index < text.Length && !char.IsWhiteSpace(text[index]))
				{
					sb.Append(text[index]);
					whitespaceAdded = false;
					index++;
				}

				foundWords++;

				// skip whitespace until next word
				while(index < text.Length && char.IsWhiteSpace(text[index]))
				{
					if(whitespaceAdded == false) { sb.Append(text[index]); whitespaceAdded = true; }
					index++;
				}
			}

			return sb.ToString();
		}


		public static string WholeWordCrop(
			string value, int length, bool appendEllipsis = true)
		{
			if(value == null)
			{
				throw new ArgumentNullException("value");
			}

			if(length < 0)
			{
				throw new ArgumentOutOfRangeException("Negative values not allowed.", "length");
			}

			var nonWordCharacters = DefaultNonWordCharacters;

			if(length >= value.Length)
			{
				return value;
			}
			int end = length;

			for(int i = end; i > 0; i--)
			{
				if(IsWhitespace(value[i]))
				{
					break;
				}

				if(nonWordCharacters.Contains(value[i])
					&& (value.Length == i + 1 || value[i + 1] == ' '))
				{
					//Removing a character that isn't whitespace but not part 
					//of the word either (ie ".") given that the character is 
					//followed by whitespace or the end of the string makes it
					//possible to include the word, so we do that.
					break;
				}
				end--;
			}

			if(end == 0)
			{
				//If the first word is longer than the length we favor 
				//returning it as cropped over returning nothing at all.
				end = length;
			}

			if(appendEllipsis == false)
				return value.Substring(0, end);
			else
				return value.Substring(0, end) + "...";

		}
	}
}
