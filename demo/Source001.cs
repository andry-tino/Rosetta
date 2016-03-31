///
/// Source001.cs
/// Andrea Tino - 2015
///

namespace Rosetta.Demo.Classes.Simple
{
	using System;
	
	public class PersonalIdentifier
	{
		private const string prefix = "ID";
		
		private string name;
		private int progressiveNumber;
		
		public PersonalIdentifier(string name)
		{
			if (name == null)
			{
				name = "default";
			}
			
			this.name = name;
			this.progressiveNumber = 0;
		}
		
		public string Id
		{
			get { return this.GenerateId(++this.progressiveNumber); }
		}
		
		private string GenerateId(int number)
		{
			string str = "";
			string separator = "-";
			
			str += prefix;
			str += separator + name;
			str += separator + number;
			
			return str + "!";
		}
	}
}
