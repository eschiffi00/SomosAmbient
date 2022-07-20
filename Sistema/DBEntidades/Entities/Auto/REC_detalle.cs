using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class REC_detalle
    {
		public int ID { get; set; }
		public int RecetaID { get; set; }
		public string TipoRelacion { get; set; }
		public int CodigoRelacion { get; set; }
		public int? Cantidad { get; set; }
		public decimal Peso { get; set; }

		public override string ToString() 
		{
			return "\r\n " +
			"ID: " + ID.ToString() + "\r\n " +
			"RecetaID: " + RecetaID.ToString() + "\r\n " +
			"TipoRelacion: " + TipoRelacion.ToString() + "\r\n " +
			"CodigoRelacion: " + CodigoRelacion.ToString() + "\r\n " +
			"Cantidad: " + Cantidad.ToString() + "\r\n " +
			"Peso: " + Peso.ToString() + "\r\n ";
		}
        public REC_detalle()
        {
			ID = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "RecetaID": return false;
				case "TipoRelacion": return false;
				case "CodigoRelacion": return false;
				case "Cantidad": return true;
				case "Peso": return true;
				case "RecPasosID": return false;
				default: return false;
			}
		}
    }
}

