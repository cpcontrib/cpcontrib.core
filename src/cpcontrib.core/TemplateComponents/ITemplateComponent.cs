//!packer:targetFile=Internals.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

namespace CPContrib.Core.TemplateComponents
{
	public interface ITemplateComponent
	{
		Context context { get; }
		Asset asset { get; set; }
	}
	public interface ITemplateComponent<Tcontext> : ITemplateComponent
	{
		new Tcontext context { get; }
	}
}
