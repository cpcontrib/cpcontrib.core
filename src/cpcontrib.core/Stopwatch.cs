//!packer:targetFile=lib.cs
using System;


namespace CrownPeak.CMSAPI.CustomLibrary
{

	/// <summary>
	/// A class designed to mimic System.Diagnostics.Stopwatch because System.Diagnostics namespace is not allowed.
	/// </summary>
	public class Stopwatch
	{
		DateTimeOffset orig;
		DateTimeOffset stopat;
		bool stopped = true;

		public Stopwatch()
		{
			orig = DateTimeOffset.Now;
		}

		public DateTimeOffset Started { get { return orig; } }

		public void Start()
		{
			orig = DateTimeOffset.Now;
			stopped = false;
		}
		public void Stop()
		{
			stopat = DateTimeOffset.Now;
			stopped = true;
		}
		public void Reset()
		{
			stopped = true;
			orig = DateTimeOffset.Now;
			stopat = orig; //makes elapsed show zero
		}
		public void Restart()
		{
			orig = DateTimeOffset.Now;
		}

		public TimeSpan Elapsed
		{
			get
			{
				if (stopped)
					return stopat - orig;
				else
					return DateTimeOffset.Now - orig;
			}
		}
		public long ElapsedMilliseconds
		{
			//this might not return exactly same as SystemDiagnosticsStopWatch
			get { return Convert.ToInt64(this.Elapsed.TotalMilliseconds); }
		}
	}

}
