//!packer:targetFile=Internals.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPContrib.Core.Wrappers
{
	using Out = CrownPeak.CMSAPI.Out;

	/// <summary>
	/// An implementation of ITextWriter that routes to the CrownPeakCMSAPI.Out class.
	/// </summary>
	public class OutWrapper : CrownPeak.CMSAPI.ITextWriter
	{

		public void Write(int value)
		{
			Out.Write(value);
		}

		public void Write(string message)
		{
			Out.Write(message);
		}

		public void Write(object value)
		{
			Out.Write(value);
		}
		public void Write(bool value)
		{
			Out.Write(value);
		}

		public void Write(string format, params object[] args)
		{
			Out.Write(format, args);
		}

		public void WriteLine(string line)
		{
			Out.WriteLine(line);
		}

		public void WriteLine(string formattedString, params object[] args)
		{
			Out.WriteLine(formattedString, args);
		}
	}

}
