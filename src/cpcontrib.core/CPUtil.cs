//!packer:combine
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;
using CrownPeak.CMSAPI.Services;

namespace CPContrib.Core
{
	using CrownPeak.CMSAPI;

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



	}
}
