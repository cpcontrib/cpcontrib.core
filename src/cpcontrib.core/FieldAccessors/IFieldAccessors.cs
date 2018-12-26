//!packer:targetFile=lib.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

//!packer:namespacefix=project
namespace CrownPeak.CMSAPI.CustomLibrary
{
	using CPContrib.Core.Internal;

	/// <summary>
	/// An interface for accessing fields on Asset, PanelEntry or InputForm
	/// </summary>
	public /*interface*/abstract class IFieldAccessor
	{
		public abstract string this[string key] { get; }
		public abstract IFieldAccessor Raw { get; }
		public abstract bool Editable { get; }

		public static implicit operator IFieldAccessor(Asset source)
		{
			return new FieldAccessor(source);
		}
		public static implicit operator IFieldAccessor(PanelEntry source)
		{
			return new FieldAccessor(source);
		}
		public static implicit operator IFieldAccessor(InputForm source)
		{
			return new FieldAccessor(source);
		}
		public static implicit operator Asset(IFieldAccessor source)
		{
			FieldAccessor fieldaccessor = source as FieldAccessor;
			return fieldaccessor.GetAsset();
		}

		public abstract List<IFieldAccessor> GetPanels(string key);
		public abstract UploadedFiles GetUploadedFiles();
		public abstract string GetFieldName(string key);

	}


	public /*interface*/class FieldAccessor : IFieldAccessor /*would be applied to CMSAPI.Asset CMSAPI.PanelEntry and possibly others */
	{

		public FieldAccessor() { }

		public FieldAccessor(Asset source, bool editable = true)
		{
			if(source == null) throw new ArgumentNullException("source");
			_type = 1;
			_asset = source;
			_editable = editable;
			_raw = new RawAccessor(_asset);
		}
		public FieldAccessor(PanelEntry panel, bool editable = true)
		{
			_type = 2;
			_panel = panel;
			_editable = editable;
			_raw = new RawAccessor(_panel);
		}
		public FieldAccessor(PanelEntry panel, Asset parent, bool editable = true)
		{
			//Need a way to reference the parent Asset of a Panel without needing to always pass both to a function.
			//Its annoying because there is no better API functionality when dealing with Assets and their children Panels.
			_type = 2;
			_panel = panel;
			_asset = parent;
			_editable = editable;
			_raw = new RawAccessor(_panel);
		}
		public FieldAccessor(InputForm inputform, bool editable = true)
		{
			_type = 3;
			_inputform = inputform;
			_editable = editable;
			//_raw = new RawAccessor(this);
		}
		public FieldAccessor(InputForm inputform, Asset parent, bool editable = true)
		{
			_type = 3;
			_inputform = inputform;
			_asset = parent;
			_raw = new RawAccessor(_asset);
		}

		public static implicit operator FieldAccessor(Asset source)
		{
			return new FieldAccessor(source);
		}
		public static implicit operator FieldAccessor(PanelEntry source)
		{
			return new FieldAccessor(source);
		}
		public static implicit operator FieldAccessor(InputForm source)
		{
			return new FieldAccessor(source);
		}

		private bool _editable;
		public int _type = 0;
		private Asset _asset;
		private PanelEntry _panel;
		private InputForm _inputform;
		private RawAccessor _raw;
		public override string this[string key]
		{
			get
			{
				switch(_type)
				{
					case 1: if(_editable) return _asset[key]; else return _asset.Raw[key];
					case 2: if(_editable) return _panel[key]; else return _panel.Raw[key];
					case 3: return _inputform[key];
				}
				return null;
			}
		}
		/*internal string RawA(string key)
		{
			switch (_type)
			{
				case 1: return _asset.Raw[key];
				case 2: return _panel.Raw[key];
				case 3: return _inputform[key];
			}
			return null;
		}*/
		public override IFieldAccessor Raw { get { return this._raw; } }
		public override bool Editable
		{
			get { return _editable; }
		}

		public override List<IFieldAccessor> GetPanels(string key)
		{
			List<PanelEntry> panels;
			//Out.DebugWriteLine("GetPanels('{0}')", key);
			switch(_type)
			{
				case 1:
					panels = _asset.GetPanels(key);//Out.Write("id={0},panels.Count={1}",_asset.Id,panels.Count());
					return panels.Select(item => new EditablePanelEntry(item, this._editable)).ToList<IFieldAccessor>();
				case 2:
					panels = _panel.GetPanels(key);//Out.Write("panels.Count={0}",panels.Count());
					return panels.Select(item => new EditablePanelEntry(item, this._editable)).ToList<IFieldAccessor>();
				case 3:
					panels = _inputform.GetPanels(key);//Out.Write("panels.Count={0}",panels.Count());
					return panels.Select(item => new EditablePanelEntry(item, this._editable)).ToList<IFieldAccessor>();
			}

			return new List<IFieldAccessor>();
		}

		private class EditablePanelEntries
		{
			List<EditablePanelEntry> _panelEntries;
			bool _editable;
			public EditablePanelEntries(IEnumerable<PanelEntry> source, bool editable)
			{
				_panelEntries = source.Select(item => new EditablePanelEntry(item, editable)).ToList();
				_editable = editable;
			}
			public static implicit operator List<PanelEntry>(EditablePanelEntries editableEntries)
			{
				return editableEntries._panelEntries.Select(item => item.PanelEntry).ToList();
			}
			//public static implicit operator List<IFieldAccessor>(EditablePanelEntries editableEntries)
			//{
			//    throw new NotImplementedException();
			//    //return new EditablePanelEntries(editableEntries._panelEntries.Select(item=>item.PanelEntry), editableEntries._editable);
			//}
		}
		public class EditablePanelEntry : IFieldAccessor
		{
			public static implicit operator PanelEntry(EditablePanelEntry panelEntry2)
			{
				return panelEntry2.PanelEntry;
			}
			public EditablePanelEntry(PanelEntry panelEntry, bool editable)
			{
				this.PanelEntry = panelEntry;
				this._editable = editable;
				this._raw = new RawAccessor(this.PanelEntry);
			}
			private RawAccessor _raw;
			private bool _editable;
			public override bool Editable
			{
				get { return _editable; }
			}
			public PanelEntry PanelEntry { get; private set; }
			public override List<IFieldAccessor> GetPanels(string key)
			{
				var t = this.PanelEntry.GetPanels(key).Select(item => new EditablePanelEntry(item, this.Editable)).ToList<IFieldAccessor>();
				return t;
			}
			public override UploadedFiles GetUploadedFiles()
			{
				return PanelEntry.UploadedFiles;
			}
			public override string GetFieldName(string key)
			{
				return PanelEntry.GetFieldName(key);
			}
			public override string this[string key]
			{
				get { if(Editable) return PanelEntry[key]; else return PanelEntry.Raw[key]; }
			}
			public override IFieldAccessor Raw
			{
				get { return _raw; }
			}
		}

		public override UploadedFiles GetUploadedFiles()
		{
			switch(_type)
			{
				case 1: return _asset.UploadedFiles;
				case 2: return _panel.UploadedFiles;
				case 3: return _inputform.UploadedFiles;
			}

			return null;
		}

		public override string GetFieldName(string key)
		{
			switch(_type)
			{
				case 2: return _panel.GetFieldName(key);
				default: return key;
			}
		}

		public Asset GetAsset()
		{
			return _asset;
		}

	}


}
