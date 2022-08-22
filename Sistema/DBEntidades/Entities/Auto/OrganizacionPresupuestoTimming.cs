using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class OrganizacionPresupuestoTimming
    {
		public int Id { get; set; }
		public int PresupuestoId { get; set; }
		public string HoraInicio { get; set; }
		public string Descripcion { get; set; }
		public string Duracion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"PresupuestoId: " + PresupuestoId.ToString() + "\r\n " + 
			"HoraInicio: " + HoraInicio.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Duracion: " + Duracion.ToString() + "\r\n " ;
		}
        public OrganizacionPresupuestoTimming()
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
				case "HoraInicio": return false;
				case "Descripcion": return false;
				case "Duracion": return false;
				default: return false;
			}
		}
    }
}

