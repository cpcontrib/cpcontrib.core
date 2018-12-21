//!packer:targetFile=lib.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

//!packer:namespacefix=project
namespace CrownPeak.CMSAPI.CustomLibrary
{
	using CPContrib.Core.Internals;

	public abstract class IFieldAccessor
	{
		//must be abstract class because of the implicit operators

		abstract public string this[string key] { get; set;  }

		abstract public RawWrapper Raw { get; }

		public static implicit operator IFieldAccessor(Asset asset)
		{
			return new FieldAccessorAssetWrapper(asset);
		}
		public static implicit operator IFieldAccessor(PanelEntry panel)
		{
			return new PanelEntryWrapper(panel);
		}
	}

}

namespace CPContrib.Core.Internals
{
	using CrownPeak.CMSAPI.CustomLibrary;

	public class RawWrapper
	{
		private Asset _Asset;
		private PanelEntry _Panel;

		protected RawWrapper() { }
		public RawWrapper(Asset asset) { this._Asset = asset; }
		public RawWrapper(PanelEntry panel) { this._Panel = panel; }

		public virtual string this[string key]
		{
			get
			{
				if(_Asset != null) return _Asset.Raw[key];
				if(_Panel != null) return _Panel.Raw[key];
				return "";
			}
		}

	}

	public class FieldAccessorAssetWrapper : IFieldAccessor
	{
		private Asset _Asset;
		private RawWrapper _RawWrapper;

		public FieldAccessorAssetWrapper(Asset asset)
		{
			_Asset = asset;
			_RawWrapper = new RawWrapper(asset);
		}

		public Asset Asset { get { return _Asset; } }

		public override string this[string key]
		{
			get { return _Asset[key]; }
			set { _Asset[key] = value; }
		}

		public override RawWrapper Raw { get { return _RawWrapper; } }
	}
	public class PanelEntryWrapper : IFieldAccessor
	{
		private PanelEntry _Panel;
		private RawWrapper _RawWrapper;
		public PanelEntryWrapper(PanelEntry panel)
		{
			this._Panel = panel;
			this._RawWrapper = new RawWrapper(panel);
		}

		public override string this[string key]
		{
			get { return this._Panel[key]; }
			set { this._Panel[key] = value; }
		}

		public override RawWrapper Raw { get { return _RawWrapper; } }

	}

}