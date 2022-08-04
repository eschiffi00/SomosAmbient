using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Personas
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int IsProveedor { get; set; }
		public int IsCliente { get; set; }
		public int IsEmpresa { get; set; }
		public string CuitDni { get; set; }
		public DateTime FechaAlta { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public int IsBaja { get; set; }
		public DateTime? FechaBaja { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"IsProveedor: " + IsProveedor.ToString() + "\r\n " + 
			"IsCliente: " + IsCliente.ToString() + "\r\n " + 
			"IsEmpresa: " + IsEmpresa.ToString() + "\r\n " + 
			"CuitDni: " + CuitDni.ToString() + "\r\n " + 
			"FechaAlta: " + FechaAlta.ToString() + "\r\n " + 
			"FechaModificacion: " + FechaModificacion.ToString() + "\r\n " + 
			"IsBaja: " + IsBaja.ToString() + "\r\n " + 
			"FechaBaja: " + FechaBaja.ToString() + "\r\n " ;
		}
        public Personas()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "IsProveedor": return false;
				case "IsCliente": return false;
				case "IsEmpresa": return false;
				case "CuitDni": return false;
				case "FechaAlta": return false;
				case "FechaModificacion": return true;
				case "IsBaja": return false;
				case "FechaBaja": return true;
				default: return false;
			}
		}
    }
}

