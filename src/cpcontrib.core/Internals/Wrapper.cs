//!packer:targetFile=Internals.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;

namespace CPContrib.Core.Internal
{
	public class Wrapper : IDisposable
	{
		public Wrapper()
		{
		}
		public Wrapper(string output)
		{
			this._output = output;
		}
		public Wrapper(Func<string> outputFunc)
		{
			this._outputFunc = outputFunc;
		}

		//private ICMSOutput Out;
		private string _output;
		private Func<string> _outputFunc;

		public void Dispose()
		{
			if(this._output != null)
			{
				Out.WriteLine(_output);
			}
			if(this._outputFunc != null)
			{
				Out.WriteLine(_outputFunc());
			}
			GC.SuppressFinalize(this);
		}
	}

}
