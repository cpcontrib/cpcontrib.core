//!packer:targetFile=Internals.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

namespace CPContrib.Core.Internal
{
	using CrownPeak.CMSAPI.CustomLibrary;

	public class RawAccessor : IFieldAccessor
	{
		public RawAccessor(Asset asset)
		{
			this._asset = asset;
			this._type = 1;
		}
		public RawAccessor(PanelEntry panel)
		{
			this._panel = panel;
			this._type = 2;
		}

		private int _type;
		public Asset _asset;
		private PanelEntry _panel;

		public override bool Editable { get { throw new NotImplementedException(); } }

		public override string this[string key]
		{
			get
			{
				//Log.Debug("Type={0}", _type);

				string value = null;
				switch (_type)
				{
					case 1: value = _asset.Raw[key]; break;
					case 2: value = _panel.Raw[key]; break;
					default:
						throw new NotImplementedException();
				}

				//Log.Debug("returning value={0}", value);
				return value;
			}
		}

		public override IFieldAccessor Raw { get { return this; } }

		public override List<IFieldAccessor> GetPanels(string key)
		{
			throw new NotImplementedException();
		}

		public override UploadedFiles GetUploadedFiles()
		{
			throw new NotImplementedException();
		}

		public override string GetFieldName(string key)
		{
			throw new NotImplementedException();
		}
	}
}
