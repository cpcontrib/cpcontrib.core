//!packer:targetFile=lib.cs

//injecting into CMSAPI to establish the necessity of this.
namespace CrownPeak.CMSAPI
{

	public interface ITextWriter
	{
		void WriteLine(string line);
		void WriteLine(string formattedString, params object[] args);

		void Write(bool value);
		void Write(string message);
		void Write(int value);
		void Write(object value);
		void Write(string format, params object[] args);
	}

}
