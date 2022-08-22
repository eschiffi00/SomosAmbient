using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ObjetivosEmpleados
    {
		public int Id { get; set; }
		public int EmpleadoId { get; set; }
		public int Mes { get; set; }
		public int Anio { get; set; }
		public decimal Facturacion { get; set; }
		public int CantidadAperturas { get; set; }
		public int Trimestre { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"EmpleadoId: " + EmpleadoId.ToString() + "\r\n " + 
			"Mes: " + Mes.ToString() + "\r\n " + 
			"Anio: " + Anio.ToString() + "\r\n " + 
			"Facturacion: " + Facturacion.ToString() + "\r\n " + 
			"CantidadAperturas: " + CantidadAperturas.ToString() + "\r\n " + 
			"Trimestre: " + Trimestre.ToString() + "\r\n " ;
		}
        public ObjetivosEmpleados()
        {
            Id = -1;

        }

		public Empleados GetRelatedEmpleadoId()
		{
			Empleados empleados = EmpleadosOperator.GetOneByIdentity(EmpleadoId);
			return empleados;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "EmpleadoId": return false;
				case "Mes": return false;
				case "Anio": return false;
				case "Facturacion": return false;
				case "CantidadAperturas": return false;
				case "Trimestre": return false;
				default: return false;
			}
		}
    }
}

