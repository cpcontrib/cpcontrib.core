//!packer:targetFile=lib.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

namespace CPContrib.Core
{
	public interface IAssetRepository
	{
		Asset Load(int id);
		Asset Load(string assetPath);
		Asset Load(CrownPeak.CMSAPI.AssetPath assetPath);

		/// <summary>
		/// Creates an asset in the given folder using a given model.
		/// </summary>
		/// <param name="label">The name of the asset.</param>
		/// <param name="saveLocation">The folder to save this asset.</param>
		/// <param name="model">The model for this asset.</param>
		/// <param name="contentFields">The content fields for this asset.</param>
		/// <param name="createModelChildren">if set to true, will also create the model's children if the model is of type Folder.</param>
		/// <returns></returns>
		Asset CreateNewAsset(string label, Asset saveLocation, Asset model, Dictionary<string, string> contentFields, bool createModelChildren = false);
	}
}
