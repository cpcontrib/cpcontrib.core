using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrownPeak.CMSAPI.Custom_Library
{
	public static class CPUtil
	{


		/// <summary>
		/// Creates a Dictionary<string,string> from the supplied values.  Any duplicated values will cause an exception.</string>
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
		public static Dictionary<string, string> MakeDictionary(params string[] values)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();

			if(values == null || values.Length == 0) return dictionary;

			foreach(string value in values)
			{
				dictionary.Add(value, value);
			}

			return dictionary;
		}

	}
}
