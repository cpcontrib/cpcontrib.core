using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrownPeak.CMSAPI.Custom_Library
{

	public class AssetPathResolver
	{

		public AssetPathStyle AssetsRootPathStyle { get; set; }

		public AssetPathResolver()
		{
			AssetsRootPathStyle = AssetPathStyle.OrganizationLanguage;
		}

		private static AssetPathResolver S_default = new AssetPathResolver();
		public static AssetPathResolver Default
		{
			get { return S_default; }
		}

		private Dictionary<string, Func<AssetPath, string>> _SpecialPaths = new Dictionary<string, Func<AssetPath, string>>();

		public AssetPathResolver AddSpecialPath(string specialPathSpec, string destinationPathSpec)
		{
			_SpecialPaths.Add(specialPathSpec, new Func<AssetPath, string>((ap) => destinationPathSpec));
			return this;
		}
		public AssetPathResolver AddSpecialPath(string specialPathSpec, Func<AssetPath, string> resolverFunction)
		{
			_SpecialPaths.Add(specialPathSpec, resolverFunction);
			return this;
		}

		public string Resolve(Asset current, string assetPath)
		{
			return Resolve(current.AssetPath, assetPath);
		}

		public string Resolve(AssetPath current, string assetPath)
		{
			List<string> assetPath2 = new List<string>(assetPath.Split('/'));

			if(assetPath2[0] == "$")
			{
				switch(AssetsRootPathStyle)
				{
					case AssetPathStyle.OrganizationLanguage:
						assetPath2[0] = current[0];
						assetPath2.Insert(1, current[1]);
						break;
				}
			}

			//check for special paths in the specialPaths structure.
			foreach(string key in _SpecialPaths.Keys)
			{
				if(string.Compare(assetPath2[0], key, true) == 0)
				{
					string specialPathValue = null;

					try
					{
						specialPathValue = _SpecialPaths[key](current);
					}
					catch(Exception ex)
					{
						throw new ApplicationException(string.Format("Failed to evaluate a path for key '{0}'.", key), ex);
					}

					List<string> assetPath3 = new List<string>(specialPathValue.Split('/'));

					//replace first
					assetPath2[0] = GetPathPart(assetPath3[1], current);

					//insert all the rest
					for(int index = 2; index < assetPath3.Count; index++)
					{
						assetPath2.Insert(index - 1, GetPathPart(assetPath3[index], current));
					}
				}
			}

			if(assetPath2[0] == "." || assetPath2[0] == "..")
			{
				//remember and remove the relDir specifier
				string relDir = assetPath2[0];
				assetPath2.RemoveAt(0);

				int index = 0;
				foreach(var part in current.Take(current.Count - relDir.Length))
				{
					assetPath2.Insert(index++, part);
				}
			}

			return "/" + string.Join("/", assetPath2);
		}

		private static string GetPathPart(string part, AssetPath current)
		{
			int indexOfBrace = part.IndexOf("{");
			if(indexOfBrace == -1) return part;

			char[] pathChars = part.ToCharArray();
			int braceIndexerLength = 0;
			for(int i = indexOfBrace + 1; i < pathChars.Length; i++)
			{
				if((pathChars[i] >= '0' && pathChars[i] <= '9') == false) break;
				braceIndexerLength++;
			}
			string pathIndexStr = part.Substring(indexOfBrace + 1, braceIndexerLength);

			int pathIndex = int.Parse(pathIndexStr);
			return current[pathIndex];
		}

	}

	public enum AssetPathStyle
	{
		/// <summary>
		/// a folder structure where the site's content starts in the root of the CMS instance.  Similar to: /Homepage, /Some content, /About Us/Information
		/// </summary>
		SiteRoot = 1
		,
		/// <summary>
		/// a folder structure where the content is loaded into an Org Unit, with no language codes.  Similar to: /AdventGeneral, /Simple Site, /CompanyName, /CompanyName-West
		/// </summary>
		Organization = 2
		,
		/// <summary>
		/// a folder structure where the content is loaded into an Org Unit first and Language after, then any assets.  Similar to: /[Organization]/[Language]/ or /AssetPath[0]/AssetPath[1]
		/// </summary>
		OrganizationLanguage = 3
	}
}
