using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class OrganizacionPresupuestosArchivos
    {
		public int Id { get; set; }
		public int PresupuestoId { get; set; }
		public string Desripcion { get; set; }
		public string NombreArchivo { get; set; }
		public string Extension { get; set; }
		public object Archivo { get; set; }
		public DateTime CreateFecha { get; set; }
		public int EmpleadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"PresupuestoId: " + PresupuestoId.ToString() + "\r\n " + 
			"Desripcion: " + Desripcion.ToString() + "\r\n " + 
			"NombreArchivo: " + NombreArchivo.ToString() + "\r\n " + 
			"Extension: " + Extension.ToString() + "\r\n " + 
			"Archivo: " + Archivo.ToString() + "\r\n " + 
			"CreateFecha: " + CreateFecha.ToString() + "\r\n " + 
			"EmpleadoId: " + EmpleadoId.ToString() + "\r\n " ;
		}
        public OrganizacionPresupuestosArchivos()
        {
            Id = -1;

        }

		public Presupuestos GetRelatedPresupuestoId()
		{
			Presupuestos presupuestos = PresupuestosOperator.GetOneByIdentity(PresupuestoId);
			return presupuestos;
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
				case "PresupuestoId": return false;
				case "Desripcion": return true;
				case "NombreArchivo": return true;
				case "Extension": return true;
				case "Archivo": return true;
				case "CreateFecha": return false;
				case "EmpleadoId": return false;
				default: return false;
			}
		}
    }
}

