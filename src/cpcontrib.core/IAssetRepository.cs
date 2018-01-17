using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrownPeak.CMSAPI
{
	public interface IAssetRepository
	{

		Asset Load(int assetId);
		Asset Load(string assetPath);
		Asset Load(AssetPath assetPath);

		Asset CreateNewAsset(string label, Asset saveLocation, Asset model, Dictionary<string, string> contentFields, bool createModelChildren = false);
	}

	public class AssetProxyRepository : IAssetRepository
	{

		public AssetProxyRepository()
		{
		}
		public static IAssetRepository Default { get { return S_AssetRepo; } }
		private static IAssetRepository S_AssetRepo = new AssetProxyRepository();

		public Asset Load(int assetId)
		{
			return CrownPeak.CMSAPI.Asset.Load(assetId);
		}

		public Asset Load(string assetPath)
		{
			return CrownPeak.CMSAPI.Asset.Load(assetPath);
		}

		public Asset Load(AssetPath assetPath)
		{
			return CrownPeak.CMSAPI.Asset.Load(assetPath);
		}

		/// <summary>
		/// Creates an asset in the given folder with the given model
		/// </summary>
		/// <param name="label"></param>
		/// <param name="saveLocation"></param>
		/// <param name="model"></param>
		/// <param name="contentFields"></param>
		/// <param name="createModelChildren"></param>
		/// <returns></returns>
		public Asset CreateNewAsset(string label, Asset saveLocation, Asset model, Dictionary<string, string> contentFields, bool createModelChildren = false)
		{
			return CrownPeak.CMSAPI.Asset.CreateNewAsset(label, saveLocation, model, contentFields, createModelChildren);
		}


	}
}
