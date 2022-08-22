using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Feriados
    {
		public int Id { get; set; }
		public DateTime Fecha { get; set; }
		public string Descripcion { get; set; }
		public int Anio { get; set; }
		public DateTime? SePasaA { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Anio: " + Anio.ToString() + "\r\n " + 
			"SePasaA: " + SePasaA.ToString() + "\r\n " ;
		}
        public Feriados()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Fecha": return false;
				case "Descripcion": return true;
				case "Anio": return false;
				case "SePasaA": return true;
				default: return false;
			}
		}
    }
}

