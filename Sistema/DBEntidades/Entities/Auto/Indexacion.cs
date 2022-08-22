using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Indexacion
    {
		public int Id { get; set; }
		public decimal ValorIndexacion { get; set; }
		public string TipoIndexacion { get; set; }
		public DateTime? FechaVencimiento { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ValorIndexacion: " + ValorIndexacion.ToString() + "\r\n " + 
			"TipoIndexacion: " + TipoIndexacion.ToString() + "\r\n " + 
			"FechaVencimiento: " + FechaVencimiento.ToString() + "\r\n " ;
		}
        public Indexacion()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ValorIndexacion": return false;
				case "TipoIndexacion": return false;
				case "FechaVencimiento": return true;
				default: return false;
			}
		}
    }
}

