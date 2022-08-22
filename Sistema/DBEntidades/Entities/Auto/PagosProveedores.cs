using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class PagosProveedores
    {
		public int Id { get; set; }
		public string NroOrdenPago { get; set; }
		public decimal Importe { get; set; }
		public DateTime Fecha { get; set; }
		public int CuentaId { get; set; }
		public int FormadePagoId { get; set; }
		public string NroComprobanteTransferencia { get; set; }
		public DateTime? FechaTransferencia { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"NroOrdenPago: " + NroOrdenPago.ToString() + "\r\n " + 
			"Importe: " + Importe.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"CuentaId: " + CuentaId.ToString() + "\r\n " + 
			"FormadePagoId: " + FormadePagoId.ToString() + "\r\n " + 
			"NroComprobanteTransferencia: " + NroComprobanteTransferencia.ToString() + "\r\n " + 
			"FechaTransferencia: " + FechaTransferencia.ToString() + "\r\n " ;
		}
        public PagosProveedores()
        {
            Id = -1;

        }

		public Cuentas GetRelatedCuentaId()
		{
			Cuentas cuentas = CuentasOperator.GetOneByIdentity(CuentaId);
			return cuentas;
		}



		public List<ComprobantePagoProveedor> GetRelatedComprobantePagoProveedores()
		{
			return ComprobantePagoProveedorOperator.GetAll().Where(x => x.PagoProveedorId == Id).ToList();
		}
		public List<ChequesPagosProveedores> GetRelatedChequesPagosProveedoreses()
		{
			return ChequesPagosProveedoresOperator.GetAll().Where(x => x.PagoProveedorId == Id).ToList();
		}
		public List<Retenciones> GetRelatedRetencioneses()
		{
			return RetencionesOperator.GetAll().Where(x => x.PagoProveedorId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "NroOrdenPago": return false;
				case "Importe": return false;
				case "Fecha": return false;
				case "CuentaId": return false;
				case "FormadePagoId": return false;
				case "NroComprobanteTransferencia": return true;
				case "FechaTransferencia": return true;
				default: return false;
			}
		}
    }
}

