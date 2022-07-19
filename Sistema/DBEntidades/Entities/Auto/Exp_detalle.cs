using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Exp_detalle
    {
		public int ID { get; set; }
		public int ExpID { get; set; }
		public string TipoRelacion { get; set; }
		public int CodigoRelacion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"ExpID: " + ExpID.ToString() + "\r\n " + 
			"TipoRelacion: " + TipoRelacion.ToString() + "\r\n " + 
			"CodigoRelacion: " + CodigoRelacion.ToString() + "\r\n " ;
		}
        public Exp_detalle()
        {
			ID = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "ExpID": return false;
				case "TipoRelacion": return false;
				case "CodigoRelacion": return false;
				default: return false;
			}
		}
    }
}

