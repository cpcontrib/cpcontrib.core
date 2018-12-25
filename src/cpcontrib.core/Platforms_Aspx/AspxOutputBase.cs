//!packer:targetFile=Platforms.Aspx.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

namespace CPContrib.Core.Platforms.Aspx
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
			IAssetPathResolver assetPathResolver

			) : base(context, asset)
		{
			this._AssetRepository = assetRepository;
			this._AssetPathResolver = assetPathResolver;
		}

		protected IAssetRepository _AssetRepository;
		protected IAssetPathResolver _AssetPathResolver;
		#endregion


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
