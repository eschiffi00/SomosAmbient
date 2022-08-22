using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class FacturaClienteDetalle
    {
		public int Id { get; set; }
		public int FacturaClienteId { get; set; }
		public string Descripcion { get; set; }
		public decimal Cantidad { get; set; }
		public decimal Importe { get; set; }
		public int Grabado { get; set; }
		public DateTime CreateFecha { get; set; }
		public DateTime? UpdateFecha { get; set; }
		public int Delete { get; set; }
		public DateTime? FechaDelete { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"FacturaClienteId: " + FacturaClienteId.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Cantidad: " + Cantidad.ToString() + "\r\n " + 
			"Importe: " + Importe.ToString() + "\r\n " + 
			"Grabado: " + Grabado.ToString() + "\r\n " + 
			"CreateFecha: " + CreateFecha.ToString() + "\r\n " + 
			"UpdateFecha: " + UpdateFecha.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"FechaDelete: " + FechaDelete.ToString() + "\r\n " ;
		}
        public FacturaClienteDetalle()
        {
            Id = -1;

			Delete = 0;
        }

		public FacturasCliente GetRelatedFacturaClienteId()
		{
			FacturasCliente facturasCliente = FacturasClienteOperator.GetOneByIdentity(FacturaClienteId);
			return facturasCliente;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "FacturaClienteId": return false;
				case "Descripcion": return false;
				case "Cantidad": return false;
				case "Importe": return false;
				case "Grabado": return false;
				case "CreateFecha": return false;
				case "UpdateFecha": return true;
				case "Delete": return false;
				case "FechaDelete": return true;
				default: return false;
			}
		}
    }
}

