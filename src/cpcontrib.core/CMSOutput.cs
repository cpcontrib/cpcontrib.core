//!packer:targetFile=lib.cs
using System;
using CrownPeak.CMSAPI;

namespace CPContrib.Core
{
	using CPContrib.Core.Internal;
	using CPContrib.Core.TemplateComponents;
	
	/// <summary>
	/// A general use for CMS output needs
	/// </summary>
	public class CMSOutput : OutputComponentBase
	{

		#region constructors for DI
		public CMSOutput(OutputContext context, Asset asset,
			IAssetRepository AssetRepository,
			IAssetPathResolver AssetPathResolver,
			IResourcesProvider ResourcesProvider)
			: base(context, asset)
		{
			this.AssetPathResolver = AssetPathResolver;
			this.AssetRepository = AssetRepository;
			this.ResourcesProvider = ResourcesProvider;
		}
		protected IAssetPathResolver AssetPathResolver;
		protected IAssetRepository AssetRepository;
		protected IResourcesProvider ResourcesProvider;
		#endregion

		/// <summary>
		/// Gets the configured ResourcesProvider.
		/// </summary>
		public virtual IResourcesProvider Resources
		{
			get { return this.ResourcesProvider; }
		}

		public string ContentUrl(string assetPath)
		{
			//mistakenly run through here...
			if(assetPath.StartsWith("//") || assetPath.StartsWith("http"))
				return assetPath;

			string qs = null;

			int qsIndex;
			if((qsIndex = assetPath.IndexOf("?")) > -1)
			{
				qs = assetPath.Substring(qsIndex);
				assetPath = assetPath.Substring(0, qsIndex);
			}
			if((qsIndex = assetPath.IndexOf("#")) > -1)
			{
				qs = assetPath.Substring(qsIndex);
				assetPath = assetPath.Substring(0, qsIndex);
			}

			string returnPath = AssetPathResolver.ResolvePath(this.asset, assetPath);

			Asset target = Asset.Load(returnPath);

			if(qs != null)
			{
				return target.GetLink(LinkType.Include) + qs;
			}
			else
			{
				return target.GetLink(LinkType.Include);
			}
		}

		public string Include(string assetPath)
		{
			return ContentUrl(assetPath);
		}

		/// <summary>
		/// Hides the block from preview.  
		/// IMPORTANT: Must be used in a using block
		/// </summary>
		/// <param name="beginBlock"></param>
		/// <param name="endBlock"></param>
		/// <returns></returns>
		public IDisposable NoPreview(string beginBlock, string endBlock)
		{
			if(context.IsPublishing)
			{
				Out.WriteLine(beginBlock); //compiler will optimze this back into one string
				return new Wrapper("\n" + endBlock);
			}
			else
			{
				Out.StartCapture();
				return new Wrapper(() => { Out.StopCapture(); return ""; });
			}
		}

		public IDisposable PreviewAlt(string raw)
		{
			if(context.IsPublishing)
			{
				//basically do nothing when in publishing mode
				return new Wrapper();
			}
			else
			{
				//collect anything within {block}
				Out.StartCapture();

				//...and output just the raw string
				return new Wrapper(() => {
					Out.StopCapture();
					return raw;
				});
			}
		}

	}
}
