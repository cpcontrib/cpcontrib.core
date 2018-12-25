//!packer:targetFile=Platforms.Aspx.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

namespace CPContrib.Core
{

	/// <summary>
	/// A Platform helper primarily focused on needs within an ASPNET Page (aspx)
	/// </summary>
	public class AspxOutput : AspxOutputBase
	{

		#region constructor for DI
		public AspxOutput(OutputContext context, Asset asset,
			IAssetRepository assetRepository,
			IAssetPathResolver assetPathResolver) :
			base(context, asset,
				assetRepository,
				assetPathResolver,
				"Aspx")
		{

		}
		#endregion

	}

}
