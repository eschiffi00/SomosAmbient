using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class sysdiagrams
    {
		public string name { get; set; }
		public int principal_id { get; set; }
		public int diagram_id { get; set; }
		public int? version { get; set; }
		public object definition { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"name: " + name.ToString() + "\r\n " + 
			"principal_id: " + principal_id.ToString() + "\r\n " + 
			"diagram_id: " + diagram_id.ToString() + "\r\n " + 
			"version: " + version.ToString() + "\r\n " + 
			"definition: " + definition.ToString() + "\r\n " ;
		}
        public sysdiagrams()
        {
            diagram_id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "name": return false;
				case "principal_id": return false;
				case "diagram_id": return false;
				case "version": return true;
				case "definition": return true;
				default: return false;
			}
		}
    }
}

