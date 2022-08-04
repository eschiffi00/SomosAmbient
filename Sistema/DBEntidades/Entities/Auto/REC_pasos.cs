using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class REC_pasos
    {
		public int ID { get; set; }
		public int RecetaID { get; set; }
		public string Paso { get; set; }
		public string Orden { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"RecetaID: " + RecetaID.ToString() + "\r\n " + 
			"Paso: " + Paso.ToString() + "\r\n " + 
			"Orden: " + Orden.ToString() + "\r\n " ;
		}
        public REC_pasos()
        {
            ID = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "RecetaID": return false;
				case "Paso": return false;
				case "Orden": return false;
				default: return false;
			}
		}
    }
}

