using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class EmpleadosPresupuestosAprobados
    {
		public int Id { get; set; }
		public int PresupuestoId { get; set; }
		public int? CoordinadorId { get; set; }
		public int? CoordinadorBisId { get; set; }
		public string HoraIngresoCoordinador1 { get; set; }
		public string HoraIngresoCoordinador2 { get; set; }
		public int? JefeSalonId { get; set; }
		public int? JefeCocinaId { get; set; }
		public int? JefeBarraId { get; set; }
		public int? JefeOperacionId { get; set; }
		public int? JefeLogisticaId { get; set; }
		public int? OrganizadorId { get; set; }
		public int? ResponsableLogisticaArmadoId { get; set; }
		public int? ResponsableLogisticaDesarmadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"PresupuestoId: " + PresupuestoId.ToString() + "\r\n " + 
			"CoordinadorId: " + CoordinadorId.ToString() + "\r\n " + 
			"CoordinadorBisId: " + CoordinadorBisId.ToString() + "\r\n " + 
			"HoraIngresoCoordinador1: " + HoraIngresoCoordinador1.ToString() + "\r\n " + 
			"HoraIngresoCoordinador2: " + HoraIngresoCoordinador2.ToString() + "\r\n " + 
			"JefeSalonId: " + JefeSalonId.ToString() + "\r\n " + 
			"JefeCocinaId: " + JefeCocinaId.ToString() + "\r\n " + 
			"JefeBarraId: " + JefeBarraId.ToString() + "\r\n " + 
			"JefeOperacionId: " + JefeOperacionId.ToString() + "\r\n " + 
			"JefeLogisticaId: " + JefeLogisticaId.ToString() + "\r\n " + 
			"OrganizadorId: " + OrganizadorId.ToString() + "\r\n " + 
			"ResponsableLogisticaArmadoId: " + ResponsableLogisticaArmadoId.ToString() + "\r\n " + 
			"ResponsableLogisticaDesarmadoId: " + ResponsableLogisticaDesarmadoId.ToString() + "\r\n " ;
		}
        public EmpleadosPresupuestosAprobados()
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
				case "CoordinadorId": return true;
				case "CoordinadorBisId": return true;
				case "HoraIngresoCoordinador1": return true;
				case "HoraIngresoCoordinador2": return true;
				case "JefeSalonId": return true;
				case "JefeCocinaId": return true;
				case "JefeBarraId": return true;
				case "JefeOperacionId": return true;
				case "JefeLogisticaId": return true;
				case "OrganizadorId": return true;
				case "ResponsableLogisticaArmadoId": return true;
				case "ResponsableLogisticaDesarmadoId": return true;
				default: return false;
			}
		}
    }
}

