using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Retenciones
    {
		public int Id { get; set; }
		public int? PagoProveedorId { get; set; }
		public int? PagoClienteId { get; set; }
		public string NroCertificado { get; set; }
		public int TipoMovimimientoId { get; set; }
		public DateTime Fecha { get; set; }
		public decimal Importe { get; set; }
		public DateTime CreateFecha { get; set; }
		public DateTime? UpdateFecha { get; set; }
		public int Delete { get; set; }
		public DateTime? DeleteFecha { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"PagoProveedorId: " + PagoProveedorId.ToString() + "\r\n " + 
			"PagoClienteId: " + PagoClienteId.ToString() + "\r\n " + 
			"NroCertificado: " + NroCertificado.ToString() + "\r\n " + 
			"TipoMovimimientoId: " + TipoMovimimientoId.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"Importe: " + Importe.ToString() + "\r\n " + 
			"CreateFecha: " + CreateFecha.ToString() + "\r\n " + 
			"UpdateFecha: " + UpdateFecha.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"DeleteFecha: " + DeleteFecha.ToString() + "\r\n " ;
		}
        public Retenciones()
        {
            Id = -1;

			Delete = 0;
        }

		public PagosProveedores GetRelatedPagoProveedorId()
		{
			if (PagoProveedorId != null)
			{
				PagosProveedores pagosProveedores = PagosProveedoresOperator.GetOneByIdentity(PagoProveedorId ?? 0);
				return pagosProveedores;
			}
			return null;
		}

		public PagosClientes GetRelatedPagoClienteId()
		{
			if (PagoClienteId != null)
			{
				PagosClientes pagosClientes = PagosClientesOperator.GetOneByIdentity(PagoClienteId ?? 0);
				return pagosClientes;
			}
			return null;
		}

		public TipoMovimientos GetRelatedTipoMovimimientoId()
		{
			TipoMovimientos tipoMovimientos = TipoMovimientosOperator.GetOneByIdentity(TipoMovimimientoId);
			return tipoMovimientos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "PagoProveedorId": return true;
				case "PagoClienteId": return true;
				case "NroCertificado": return false;
				case "TipoMovimimientoId": return false;
				case "Fecha": return false;
				case "Importe": return false;
				case "CreateFecha": return false;
				case "UpdateFecha": return true;
				case "Delete": return false;
				case "DeleteFecha": return true;
				default: return false;
			}
		}
    }
}

