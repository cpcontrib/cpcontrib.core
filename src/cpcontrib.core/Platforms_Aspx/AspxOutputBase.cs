//!packer:targetFile=Platforms.Aspx.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

namespace CPContrib.Core
{
	using CPContrib.Core;
	using CPContrib.Core.TemplateComponents;
	using CPContrib.Core.Internal;

	/// <summary>
	/// Base class for Aspx helpers
	/// </summary>
	public abstract class AspxOutputBase : OutputComponentBase
	{

		#region constructors for DI
		public AspxOutputBase(OutputContext context, Asset asset,
			IAssetRepository assetRepository,
			IAssetPathResolver assetPathResolver,
			string Typename

			) : base(context, asset)
		{
			this.AssetRepository = assetRepository;
			this.AssetPathResolver = assetPathResolver;
			this.Typename = Typename;

			this.PreserveCalls = false;
		}

		protected IAssetRepository AssetRepository;
		protected IAssetPathResolver AssetPathResolver;
		protected string Typename;

		#endregion

		/// <summary>
		/// A flag for preserving code calls in published output. 
		/// Useful for roundtrip of ASPX and MASTER files. 
		/// Setting this true assumes using the runtime uses the correct  Platform runtime.
		/// </summary>
		public bool PreserveCalls { get; set; }

		/// <summary>
		/// When Publishing, writes an ASPX Directive, replacing &lt;$ $&gt; when necessary.
		/// </summary>
		/// <param name="directiveLine"></param>
		/// <param name="args"></param>
		public void WriteDirective(string directiveLine, params object[] args)
		{
			if(context.IsPublishing)
			{
				if(args == null)
					Out.WriteLine(directiveLine);
				else
					Out.WriteLine(directiveLine, args);
			}
		}

		public void PageDirective()
		{

		}

		public string ContentUrl(string assetPath, LinkType linktype = LinkType.Default)
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

			if(PreserveCalls && context.IsPublishing)
			{
				string linktypeStr = GetLinkTypeCall(linktype);
				return string.Format("<$={0}.{1}(\"{2}\")$>{3}", Typename, linktypeStr, assetPath, qs);

			}
			else
			{
				string returnPath = AssetPathResolver.ResolvePath(this.asset, assetPath);
				Asset target = Asset.Load(returnPath);

				return target.GetLink(LinkType.Include) + qs;
			}


		}

		protected string GetLinkTypeCall(LinkType linktype)
		{
			switch(linktype)
			{
				case LinkType.Include:
					return "Include";
				default:
					return "ContentUrl";
			}
		}

		public string Include(string assetPath)
		{
			return ContentUrl(assetPath, LinkType.Include);
		}

		/// <summary>
		/// Wraps a ^lt;script runat="server"&gt; block to keep VisualStudio ASPX parser from trying to execute runtime code blocks
		/// </summary>
		/// <returns></returns>
		//[Obsolete("Recommended to create a page method 'script_runat_server' for closer to original text when comparing original to template.")]
		public Wrapper ScriptRunatServer()
		{
			if(context.IsPublishing)
			{
				Out.WriteLine("<scr" + "ipt runat=\"ser" + "ver\">"); //compiler will optimze this back into one string
				return new Wrapper("\n</script>");
			}
			else
			{
				Out.StartCapture();
				return new Wrapper(() => { Out.StopCapture(); return ""; });
			}

		}

		/// <summary>
		/// Wraps an if block that is only to be rendered at publish.
		/// </summary>
		/// <param name="ifExpression"></param>
		/// <returns></returns>
		public Wrapper If(string ifExpression)//, [System.Runtime.CompilerServices.CallerLineNumber]int lineNumber = -1)
		{
			if(context.IsPublishing)
			{
				//Out.WriteLine("#line {0}", lineNumber);
				Out.WriteLine("<$ if(" + ifExpression + ") { $>");
				return new Wrapper("<$ } $>");
			}
			return new Wrapper();
		}


	}

}
