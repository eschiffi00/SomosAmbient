using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Parametro
    {
		public int ParametroId { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ParametroId: " + ParametroId.ToString() + "\r\n " + 
			"Name: " + Name.ToString() + "\r\n " + 
			"Value: " + Value.ToString() + "\r\n " ;
		}
        public Parametro()
        {
            ParametroId = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ParametroId": return false;
				case "Name": return false;
				case "Value": return false;
				default: return false;
			}
		}
    }
}

