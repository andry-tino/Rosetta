///
/// Source001.cs
/// Andrea Tino - 2015
///

namespace Rosetta.Demo.Source001 
{
	using System;
	
	public interface IPrinter
	{
		string Print(string message);
	}
	
	public class MessagePrinter : IPrinter
	{
		private const string standardRadix = "RX";
		private const string standardPrefix = "n";
		
		private string messageBase;
		
		private string name;
		private int progressiveNumber;
		
		public MessagePrinter(string name, string messageBase)
		{
			this.name = name;
			this.messageBase = messageBase;
			
			this.progressiveNumber = 0;
		}
		
		public string Print(string message)
		{
			if (message == null)
			{
				return;
			}
			
			string messageBase = this.messageBase;
			return messageBase + " " + message;
		}
		
		public string Id
		{
			get { return this.GenerateId("id", "N", this.name, ++this.progressiveNumber); }
		}
		
		private string GenerateId(string radix, string prefix, string number, int progNumber)
		{
			string str = "";
			string separator = "-";
			
			str += radix;
			str += separator + prefix;
			str += separator + prefix;
			str += separator + number;
			str += separator + progNumber;
			
			return str + "!";
		}
	}
}
