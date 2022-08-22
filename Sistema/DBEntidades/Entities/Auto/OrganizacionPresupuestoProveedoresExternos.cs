using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class OrganizacionPresupuestoProveedoresExternos
    {
		public int Id { get; set; }
		public int PresupuestoId { get; set; }
		public string ProveedorExterno { get; set; }
		public string Rubro { get; set; }
		public string Contacto { get; set; }
		public string Telefono { get; set; }
		public string Correo { get; set; }
		public string Observaciones { get; set; }
		public int SegurosOk { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"PresupuestoId: " + PresupuestoId.ToString() + "\r\n " + 
			"ProveedorExterno: " + ProveedorExterno.ToString() + "\r\n " + 
			"Rubro: " + Rubro.ToString() + "\r\n " + 
			"Contacto: " + Contacto.ToString() + "\r\n " + 
			"Telefono: " + Telefono.ToString() + "\r\n " + 
			"Correo: " + Correo.ToString() + "\r\n " + 
			"Observaciones: " + Observaciones.ToString() + "\r\n " + 
			"SegurosOk: " + SegurosOk.ToString() + "\r\n " ;
		}
        public OrganizacionPresupuestoProveedoresExternos()
        {
            Id = -1;

        }

		public Presupuestos GetRelatedPresupuestoId()
		{
			Presupuestos presupuestos = PresupuestosOperator.GetOneByIdentity(PresupuestoId);
			return presupuestos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "PresupuestoId": return false;
				case "ProveedorExterno": return true;
				case "Rubro": return true;
				case "Contacto": return true;
				case "Telefono": return true;
				case "Correo": return true;
				case "Observaciones": return true;
				case "SegurosOk": return false;
				default: return false;
			}
		}
    }
}

