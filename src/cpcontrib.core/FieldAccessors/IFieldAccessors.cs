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

}
