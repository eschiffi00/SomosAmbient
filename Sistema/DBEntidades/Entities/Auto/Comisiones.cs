using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Comisiones
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public string Precio { get; set; }
		public int? UnidadNegocioId { get; set; }
		public decimal Porcentaje { get; set; }
		public decimal PorcentajeAdicional { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " + 
			"UnidadNegocioId: " + UnidadNegocioId.ToString() + "\r\n " + 
			"Porcentaje: " + Porcentaje.ToString() + "\r\n " + 
			"PorcentajeAdicional: " + PorcentajeAdicional.ToString() + "\r\n " ;
		}
        public Comisiones()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "Precio": return true;
				case "UnidadNegocioId": return true;
				case "Porcentaje": return false;
				case "PorcentajeAdicional": return true;
				default: return false;
			}
		}
    }
}

