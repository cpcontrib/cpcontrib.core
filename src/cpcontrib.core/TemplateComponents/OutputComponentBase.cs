//!packer:targetFile=Internals.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

namespace CPContrib.Core.TemplateComponents
{
	public abstract class OutputComponentBase : ITemplateComponent<OutputContext>
	{
		public OutputComponentBase(OutputContext context, Asset asset)
		{
			this._context = context;
			this._asset = asset;
		}

		Context ITemplateComponent.context { get { return this.context; } }
		public OutputContext context { get { return this._context; } }
		OutputContext _context;

		private Asset _asset;
		public Asset asset { get; set; }
	}
}