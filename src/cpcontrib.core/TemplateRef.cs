using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

//#namespace {generated}
namespace cpcontrib.core
{

	/// <summary>
	/// Provides a reference to a CrownPeak Template.
	/// </summary>
	public class TemplateRef
	{

		public TemplateRef(string assetPath, int templateId)
		{
			this._AssetPath = assetPath;
			this._TemplateId = templateId;
		}

		string _AssetPath;
		int _TemplateId;

		public string AssetPath
		{
			get { return this._AssetPath; }
		}

		/// <summary>
		/// The TemplateId or AssetId of the Template.
		/// </summary>
		public int TemplateId
		{
			get { return this._TemplateId; }
		}

	}

}
