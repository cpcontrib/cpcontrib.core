using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPContrib.Core
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

		/// <summary>
		/// Tests each argument for null or empty, and returns first one that has a string value.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string EmptyFallback(params string[] args)
		{
			if(args == null || args.Length == 0) return null;

			for(int i = 0; i < args.Length; i++)
			{
				string value = args[i];
				if(String.IsNullOrEmpty(value) == false) return value;
			}

			return "";
		}

		///// <summary>
		///// Tests each argument for null or empty, and returns first one that has a string value.
		///// </summary>
		///// <param name="args"></param>
		///// <returns></returns>
		//public static string EmptyFallback(params Func<string>[] funcs)
		//{
		//	if(funcs == null || funcs.Length == 0) return null;

		//	for(int i = 0; i < funcs.Length; i++)
		//	{
		//		string value = funcs[i]();
		//		if(String.IsNullOrEmpty(value) == false) return value;
		//	}

		//	return "";
		//}

	}
}
