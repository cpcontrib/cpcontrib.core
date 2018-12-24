//!packer:targetFile=lib.cs
using System;
using System.Collections.Generic;
using System.Linq;
using CrownPeak.CMSAPI;

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

		public static Asset EnsureFoldersExist(AssetPath assetPath, Asset model = null)
		{
			string[] folderPath = assetPath.ToArray();

			int index = 0;
			Asset saveLocation = Asset.Load(0); //load root asset first
			Asset testFolder = null;

			do
			{
				testFolder = Asset.Load(folderPath[index]);

				if(testFolder.IsLoaded == false)
				{
					testFolder = Asset.CreateFolder(folderPath[index], saveLocation, FolderType.Folder);
				}

				saveLocation = testFolder;

			} while(index++ < folderPath.Length);

			//dont return a null
			if(testFolder == null) testFolder = Asset.Load(-1);

			return testFolder;
		}

		public static Asset EnsureFoldersExist(string assetPath, Asset model = null)
		{
			return EnsureFoldersExist(new AssetPath(assetPath), model);
		}

		/// <summary>
		/// Removes any comments that use hash (#) 
		/// If the string starts with #, it is filtered out.
		/// </summary>
		/// <param name="lines"></param>
		/// <returns></returns>
		public static IEnumerable<string> FilterHashComments(IEnumerable<string> lines)
		{
			//ignore any lines that startwith #
			//ignore any part of line after a #
			//escape this with \#
			var stringlist = lines.Where(_ => _.StartsWith("#") == false).Select(_ => {

				int indexOfHash = -1;

				char current, last = (char)0x00;
				int index = 0;
				int length = _.Length;
				do
				{
					current = _[index];

					if(current == '#')
					{
						if(last != '\\')
						{
							indexOfHash = index;
							break;
						}
					}
					last = current;

				} while((++index) < length);

				if(indexOfHash > 0)
				{
					return _.Substring(0, indexOfHash);
				}
				else
				{
					return _;
				}

			});

			return stringlist;
		}

	}
}
