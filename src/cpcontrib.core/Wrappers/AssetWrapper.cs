//!packer:targetFile=Internals.cs
using System;
using System.Collections.Generic;
using CrownPeak.CMSAPI;

namespace CPContrib.Core.Wrappers
{
	using CPContrib.Core;

	/// <summary>
	/// An implementation of <see cref="IAssetRepository" /> for CrownPeak.CMSAPI.Asset 
	/// </summary>
	public class AssetWrapper : IAssetRepository
	{
		private static readonly IAssetRepository _Default = new AssetWrapper();

		public static IAssetRepository Default { get { return _Default; } }
		private AssetWrapper() { }

		public Asset Load(int id)
		{
			return Asset.Load(id);
		}

		public Asset Load(string assetpath)
		{
			return Asset.Load(assetpath);
		}

		public Asset Load(AssetPath assetpath)
		{
			return Asset.Load(assetpath);
		}

		public Asset CreateNewAsset(string label, Asset saveLocation, Asset model, Dictionary<string, string> contentFields, bool createModelChildren = false)
		{
			return Asset.CreateNewAsset(label, saveLocation, model, contentFields, createModelChildren);
		}
	}

}
