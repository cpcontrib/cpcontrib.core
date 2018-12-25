//!packer:targetFile=lib.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

namespace CPContrib.Core
{
	using CPContrib.Core.Internal;
	using CPContrib.Core.TemplateComponents;

	public class HtmlOutput : OutputComponentBase
	{
		public HtmlOutput(OutputContext context, Asset asset)
			: base(context, asset)
		{
		}
	}
}
