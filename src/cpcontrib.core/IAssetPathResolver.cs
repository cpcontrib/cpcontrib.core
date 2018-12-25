//!packer:targetFile=lib.cs
using System;
using CrownPeak.CMSAPI;

namespace CPContrib.Core
{
	public interface IAssetPathResolver
	{
		string ResolvePath(Asset current, string assetPath);
	}
}
