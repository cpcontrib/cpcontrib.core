//!packer:targetFile=Internals.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

namespace CPContrib.Core
{
	
	/// <summary>
	/// Provides Resourced strings for CMS output
	/// </summary>
	public class ResourcesProvider : IResourcesProvider
	{

		/// <summary>
		/// Pass in an already loaded Asset that can supply Resources.  
		/// Recommended to use alternate ctor.
		/// </summary>
		/// <param name="ResourcesAsset"></param>
		public ResourcesProvider(Asset ResourcesAsset)
		{
			//ContractX.ArgNotNull(ResourcesAsset, nameof(ResourcesAsset));
			this._ResourcesAsset = ResourcesAsset;
		}

		/// <summary>
		/// Pass in a func that will provide the Resources Asset when needed.  
		/// Better for more accurate dependency tracking.
		/// </summary>
		/// <param name="GetResourcesAssetFunc"></param>
		public ResourcesProvider(Func<Asset> GetResourcesAssetFunc)
		{
			this._GetResourcesAssetFunc = GetResourcesAssetFunc;
		}

		protected Func<Asset> _GetResourcesAssetFunc;
		protected Asset _ResourcesAsset;


		public string this[string name]
		{
			get
			{
				if(_ResourcesAsset == null)
				{
					if(_GetResourcesAssetFunc == null)
					{
						//no resources available
						return name;
					}
					else
					{
						_ResourcesAsset = _GetResourcesAssetFunc();
					}
				}

				if(_ResourcesAsset.IsLoaded == false)
					return name;

				return _ResourcesAsset[name];
			}
		}

		/// <summary>
		/// Will apply values to the given Resource string. NotImplementedYet
		/// </summary>
		/// <param name="name"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public string Translate(string name, IDictionary<string, object> values)
		{
			//object values may need to get a CultureInfo reference based 
			//on current asset to properly translate.  
			//Might have to move this implementation to higher Interface
			//or require CultureInfo during lazy init of GetResourceAsset
			throw new NotImplementedException();
		}
	}

}
