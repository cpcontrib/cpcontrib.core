//!packer:targetFile=lib.cs
using System;
using System.Text;

namespace CrownPeak.CMSAPI.CustomLibrary
{

	/// <summary>
	/// Type for writing out well formed XML
	/// </summary>
	public class XmlTextWriter
	{
		static string fqn;
		static Type xmltextwriterType;

		static string GetFqn(string name, string assembly = "mscorlib")
		{
			string fqnval = name + "," + assembly + fqn;
			return fqnval.Replace("[Sys]", "Sys" + "tem.");
		}

		static XmlTextWriter()
		{
			fqn = typeof(string).AssemblyQualifiedName.Replace("System.String, mscorlib", "");

			xmltextwriterType = Type.GetType(GetFqn("[Sys]Xml.XmlTextWriter", "[Sys]Xml"));

			//_methods = new Dictionary<string, System./*asdf*/Reflection.MethodInfo>();
			//string[] methodNames = new string[] { "WriteStartElement", "WriteCData", "WriteString", "WriteRaw" };
			//foreach(string methodName in methodNames)
			//{
			//	System./*asdf*/Reflection.MethodInfo methodInfo = xmltextwriterType.GetMethod(methodName, new Type[] { typeof(string) });
			//	_methods.Add(methodName, methodInfo);
			//}
			//_methods.Add("WriteEndElement", xmltextwriterType.GetMethod("WriteEndElement"));
			//_methods.Add("WriteAttributeString", xmltextwriterType.GetMethod("WriteAttributeString", new Type[] { typeof(string), typeof(string) }));

			//_methods.Add("Formatting.set", xmltextwriterType.GetProperty("Formatting").GetSetMethod());

			//System./*asdf*/Reflection.Emit.DynamicMethod = new System./*asdf*/Reflection.Emit.DynamicMethod("DM$CORE_FACTORY_" + )
			//System./*asdf*/Reflection.Emit.ILGenerator ilGen = 
		}

		private System.Xml.XmlWriter _writer;
		private StringBuilder _sb;

		public XmlTextWriter(System.Xml.XmlWriterSettings settings)
		{
			if(settings == null) throw new ArgumentNullException("settings");
			_Initialize(settings);
		}
		public XmlTextWriter() : this(false) { }
		public XmlTextWriter(bool indented = true)
		{
			System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings()
			{
				ConformanceLevel = System.Xml.ConformanceLevel.Fragment
			};
			if(indented)
			{
				settings.Indent = true;
				settings.IndentChars = "\t";
			}
			_Initialize(settings);
		}
		private void _Initialize(System.Xml.XmlWriterSettings settings)
		{
			_sb = new StringBuilder();
			_writer = System.Xml.XmlWriter.Create(_sb, settings);
		}

		public override string ToString()
		{
			_writer.Flush();
			return _sb.ToString();
		}

		public bool Indented
		{
			set
			{
				if(value == true)
				{
					//_writer.Formatting = System.Xml.Formatting.Indented;
					//_writer.Indentation = 1;
					//_writer.IndentChar = '\t';
				}
				else
				{
					//_writer.Formatting = System.Xml.Formatting.None;
					//_writer.Indentation = 0;
				}
			}
		}

		public void WriteStartDocument()
		{
			_writer.WriteStartDocument();
		}
		public void WriteStartDocument(bool standalone)
		{
			_writer.WriteStartDocument(standalone);
		}
		public void WriteProcessingInstruction(string name, string text)
		{
			_writer.WriteProcessingInstruction(name, text);
		}
		public void WriteStartElement(string name)
		{
			_writer.WriteStartElement(name);
		}
		public void WriteStartElement(string localName, string ns)
		{
			_writer.WriteStartElement(localName, ns);
		}
		public void WriteStartElement(string prefix, string localName, string ns)
		{
			_writer.WriteStartElement(prefix, localName, ns);
		}

		public void WriteEndElement()
		{
			_writer.WriteEndElement();
		}
		public void WriteAttributeString(string name, string value)
		{
			_writer.WriteAttributeString(name, value);
		}
		public void WriteCData(string value)
		{
			_writer.WriteCData(value);
		}
		public void WriteString(string value)
		{
			_writer.WriteString(value);
		}
		public void WriteRaw(string value)
		{
			_writer.WriteRaw(value);
		}
		public void WriteBase64(byte[] buffer, int index, int count)
		{
			_writer.WriteBase64(buffer, index, count);
		}

	}

}
