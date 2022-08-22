using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Recibos
    {
		public int Id { get; set; }
		public string NroRecibo { get; set; }
		public DateTime FechaRecibo { get; set; }
		public string Concepto { get; set; }
		public DateTime FechaCreate { get; set; }
		public decimal Importe { get; set; }
		public int FormadePagoId { get; set; }
		public DateTime? FechaUpdate { get; set; }
		public int Delete { get; set; }
		public DateTime? FechaDelete { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"NroRecibo: " + NroRecibo.ToString() + "\r\n " + 
			"FechaRecibo: " + FechaRecibo.ToString() + "\r\n " + 
			"Concepto: " + Concepto.ToString() + "\r\n " + 
			"FechaCreate: " + FechaCreate.ToString() + "\r\n " + 
			"Importe: " + Importe.ToString() + "\r\n " + 
			"FormadePagoId: " + FormadePagoId.ToString() + "\r\n " + 
			"FechaUpdate: " + FechaUpdate.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"FechaDelete: " + FechaDelete.ToString() + "\r\n " ;
		}
        public Recibos()
        {
            Id = -1;

			Delete = 0;
        }

		public FormasdePago GetRelatedFormadePagoId()
		{
			FormasdePago formasdePago = FormasdePagoOperator.GetOneByIdentity(FormadePagoId);
			return formasdePago;
		}



		public List<ReciboEventoPresupuesto> GetRelatedReciboEventoPresupuestos()
		{
			return ReciboEventoPresupuestoOperator.GetAll().Where(x => x.ReciboId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "NroRecibo": return false;
				case "FechaRecibo": return false;
				case "Concepto": return false;
				case "FechaCreate": return false;
				case "Importe": return false;
				case "FormadePagoId": return false;
				case "FechaUpdate": return true;
				case "Delete": return false;
				case "FechaDelete": return true;
				default: return false;
			}
		}
    }
}

