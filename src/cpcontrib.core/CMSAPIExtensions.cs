//!packer:targetFile=extensions.cs
using System;
using System.Linq;
using CrownPeak.CMSAPI;

namespace CrownPeak.CMSAPI
{
	public static class CMSAPIExtensions
	{

		/// <summary>
		/// Retrieves immediate parent of the given assetpath. Optionally go higher in the chain by specifying the parentCount property.
		/// </summary>
		/// <param name="assetPath"></param>
		/// <param name="parentCount">defaults to 1.  retrieve higher level parents by specifying more than 1.
		/// <para>Ex: assetPath is /Site1/Folder1/Subfolder2/asset1. </para>
		/// <para>parentCount=1 gives you /Site1/Folder1/Subfolder2.  </para>
		/// <para>parentCount=2 gives you /Site1/Folder1.</para>
		/// </param>
		/// <returns></returns>
		public static AssetPath GetParent(this AssetPath assetPath, int parentCount = 1)
		{
			if(parentCount < 1) throw new ArgumentOutOfRangeException("parentCount", "parentCount cant be less than 1");
			if(parentCount > assetPath.Count) throw new ArgumentOutOfRangeException("parentCount", string.Format("parentCount cannot be greater than assetPath.Count of {0}'.", assetPath.Count));

			return new AssetPath("/" + String.Join("/", assetPath.Take(assetPath.Count - parentCount)));
		}



	}
}
