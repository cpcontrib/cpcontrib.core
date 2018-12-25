//!packer:targetFile=Internals.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPContrib.Core
{
	public interface IResourcesProvider
	{
		string this[string name] { get; }
	}
}
