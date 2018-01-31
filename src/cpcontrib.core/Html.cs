using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;
using CrownPeak.CMSAPI.Services;
/* Some Namespaces are not allowed. */

namespace CPContrib.Core
{
	using CPContrib.Core;

	public class Html
	{
		public Html(OutputContext context, Asset asset)
		{
			this.context = context;
			this.asset = asset;
		}

		private OutputContext context;
		private Asset asset;

		/// <summary>
		/// Loads an asset by assetPath, adjusted for the current CMS site root
		/// </summary>
		/// <param name="assetPath"></param>
		/// <param name="additionalQuerystring"></param>
		/// <returns></returns>
		public string LinkHref(string linkHref, string additionalQuerystring = null)
		{
			if(linkHref == null) throw new ArgumentNullException("linkHref");

			string linkHrefPath = linkHref;
			string addlQuery = "";

			int indexOfQ = linkHref.IndexOf('?');
			if(indexOfQ > -1)
			{
				linkHrefPath = linkHref.Substring(0, indexOfQ);
				addlQuery = linkHref.Substring(indexOfQ + 1);
			}

			string[] segments = linkHrefPath.Split('/');
			if(segments[0] == "~" && string.Compare(segments[1], "assets", true) == 0)
				segments[1] = "_Web/assets";

			string adjustedAssetPath = String.Join("/", segments);

			Asset linkAsset = Asset.Load(adjustedAssetPath);

			if(linkAsset.IsLoaded == false) linkAsset = Asset.LoadDirect(adjustedAssetPath);

			string href = linkAsset.GetLink(LinkType.Include);

			if(string.IsNullOrEmpty(href) == false)
			{
				additionalQuerystring = additionalQuerystring ?? "";

				if(string.IsNullOrEmpty(additionalQuerystring))
				{
					return href + (additionalQuerystring.StartsWith("?") ? "" : "?") + additionalQuerystring;
				}
			}

			return "";
		}



	}
}
