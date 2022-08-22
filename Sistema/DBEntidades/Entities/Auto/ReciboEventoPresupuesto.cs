using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ReciboEventoPresupuesto
    {
		public int Id { get; set; }
		public int ReciboId { get; set; }
		public int EventoId { get; set; }
		public int? PresupuestoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ReciboId: " + ReciboId.ToString() + "\r\n " + 
			"EventoId: " + EventoId.ToString() + "\r\n " + 
			"PresupuestoId: " + PresupuestoId.ToString() + "\r\n " ;
		}
        public ReciboEventoPresupuesto()
        {
            Id = -1;

        }

		public Recibos GetRelatedReciboId()
		{
			Recibos recibos = RecibosOperator.GetOneByIdentity(ReciboId);
			return recibos;
		}

		public Eventos GetRelatedEventoId()
		{
			Eventos eventos = EventosOperator.GetOneByIdentity(EventoId);
			return eventos;
		}

		public Presupuestos GetRelatedPresupuestoId()
		{
			if (PresupuestoId != null)
			{
				Presupuestos presupuestos = PresupuestosOperator.GetOneByIdentity(PresupuestoId ?? 0);
				return presupuestos;
			}
			return null;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ReciboId": return false;
				case "EventoId": return false;
				case "PresupuestoId": return true;
				default: return false;
			}
		}
    }
}

