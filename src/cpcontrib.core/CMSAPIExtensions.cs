using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrownPeak.CMSAPI
{
	public static class CMSAPIExtensions
	{

		public static AssetPath GetParent(this AssetPath assetPath)
		{
			var segments = assetPath.Take(assetPath.Count - 1);
			return new AssetPath("/"+String.Join("/", segments));
		}

	}
}
