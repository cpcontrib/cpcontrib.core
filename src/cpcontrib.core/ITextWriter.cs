#region /***cppack_BOF:ITextWriter.cs***/
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

namespace CPContrib.Core
{
	using Out = CrownPeak.CMSAPI.Out;


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
#endregion /***cppack_EOF:ITextWriter.cs***/