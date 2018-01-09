using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//# namespaceStatement:fix
namespace CrownPeak.CMSAPI
{
	//# usingStatement:fix
	using CrownPeak.CMSAPI.Custom_Library;

	public static class FilterParamsExtensions
	{
		public static FilterParams FilterBy(this FilterParams filterParams, TemplateRef templateRef)
		{
			filterParams.Add(AssetPropertyNames.TemplateId, Comparison.Equals, templateRef.TemplateId);
			return filterParams;
		}
		public static FilterParams FilterBy(this FilterParams filterParams, Comparison comparison, TemplateRef templateRef)
		{
			filterParams.Add(AssetPropertyNames.TemplateId, comparison, templateRef.TemplateId);
			return filterParams;
		}
	}

}
