//!packer:targetFile=Platforms.Aspx.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

namespace CPContrib.Core.Platforms.Aspx
{
	/// <summary>
	/// A Platform helper primarily focused on needs within an ASPNET Masterpage
	/// </summary>
	public class MasterOutput : AspxOutputBase
	{

		#region constructor for DI
		public MasterOutput(OutputContext context, Asset asset,
			IAssetRepository assetRepository,
			IAssetPathResolver assetPathResolver) :
			base(context, asset,
				assetRepository,
				assetPathResolver)
		{

		}
		#endregion

		/// <summary>
		/// Writes the standard ASP.Net Masterpage directive.
		/// </summary>
		/// <param name="language">specify a language that the page is written in, defaults to C#</param>
		/// <param name="masterPageType">specify what type this masterpage inherits from.</param>
		public void WriteMasterDirective(string language = "C#", string masterPageType = null)
		{
			if(context.IsPublishing)
			{
				string masterDirective = string.Format("<$@ Master Language=\"{0}\" $>", language);

				if(!String.IsNullOrWhiteSpace(masterPageType))
				{
					masterDirective = masterDirective.Replace(" $>", string.Format(" Inherits=\"{0}\" $>", masterPageType));
				}

				Out.WriteLine(masterDirective);
			}
		}
	}

}