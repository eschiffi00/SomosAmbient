using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ComprobantesProveedores
    {
		public int Id { get; set; }
		public int? ProveedorId { get; set; }
		public int? CuentaId { get; set; }
		public int TipoComprobanteId { get; set; }
		public int FormadePagoId { get; set; }
		public int? EmpresaId { get; set; }
		public decimal MontoNeto { get; set; }
		public decimal MontoFactura { get; set; }
		public long PuntoVenta { get; set; }
		public long NroComprobante { get; set; }
		public DateTime Fecha { get; set; }
		public decimal Iva21 { get; set; }
		public decimal Iva27 { get; set; }
		public decimal Iva105 { get; set; }
		public decimal IIBBCABA { get; set; }
		public decimal IIBBARBA { get; set; }
		public decimal PercepcionIVA { get; set; }
		public string GeneraOP { get; set; }
		public int EstadoId { get; set; }
		public DateTime CreateFecha { get; set; }
		public DateTime? UpdateFecha { get; set; }
		public int Delete { get; set; }
		public DateTime? DeleteFecha { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"CuentaId: " + CuentaId.ToString() + "\r\n " + 
			"TipoComprobanteId: " + TipoComprobanteId.ToString() + "\r\n " + 
			"FormadePagoId: " + FormadePagoId.ToString() + "\r\n " + 
			"EmpresaId: " + EmpresaId.ToString() + "\r\n " + 
			"MontoNeto: " + MontoNeto.ToString() + "\r\n " + 
			"MontoFactura: " + MontoFactura.ToString() + "\r\n " + 
			"PuntoVenta: " + PuntoVenta.ToString() + "\r\n " + 
			"NroComprobante: " + NroComprobante.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"Iva21: " + Iva21.ToString() + "\r\n " + 
			"Iva27: " + Iva27.ToString() + "\r\n " + 
			"Iva105: " + Iva105.ToString() + "\r\n " + 
			"IIBBCABA: " + IIBBCABA.ToString() + "\r\n " + 
			"IIBBARBA: " + IIBBARBA.ToString() + "\r\n " + 
			"PercepcionIVA: " + PercepcionIVA.ToString() + "\r\n " + 
			"GeneraOP: " + GeneraOP.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"CreateFecha: " + CreateFecha.ToString() + "\r\n " + 
			"UpdateFecha: " + UpdateFecha.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"DeleteFecha: " + DeleteFecha.ToString() + "\r\n " ;
		}
        public ComprobantesProveedores()
        {
            Id = -1;

			Delete = 0;
        }

		public Proveedores GetRelatedProveedorId()
		{
			if (ProveedorId != null)
			{
				Proveedores proveedores = ProveedoresOperator.GetOneByIdentity(ProveedorId ?? 0);
				return proveedores;
			}
			return null;
		}

		public Cuentas GetRelatedCuentaId()
		{
			if (CuentaId != null)
			{
				Cuentas cuentas = CuentasOperator.GetOneByIdentity(CuentaId ?? 0);
				return cuentas;
			}
			return null;
		}

		public TipoComprobantes GetRelatedTipoComprobanteId()
		{
			TipoComprobantes tipoComprobantes = TipoComprobantesOperator.GetOneByIdentity(TipoComprobanteId);
			return tipoComprobantes;
		}

		public FormasdePago GetRelatedFormadePagoId()
		{
			FormasdePago formasdePago = FormasdePagoOperator.GetOneByIdentity(FormadePagoId);
			return formasdePago;
		}

		public Estados GetRelatedEstadoId()
		{
			Estados estados = EstadosOperator.GetOneByIdentity(EstadoId);
			return estados;
		}



		public List<ComprobantePagoProveedor> GetRelatedComprobantePagoProveedores()
		{
			return ComprobantePagoProveedorOperator.GetAll().Where(x => x.ComprobanteProveedorId == Id).ToList();
		}
		public List<ComprobantesProveedores_Detalles> GetRelatedComprobantesProveedores_Detalleses()
		{
			return ComprobantesProveedores_DetallesOperator.GetAll().Where(x => x.ComprobanteProveedorId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ProveedorId": return true;
				case "CuentaId": return true;
				case "TipoComprobanteId": return false;
				case "FormadePagoId": return false;
				case "EmpresaId": return true;
				case "MontoNeto": return false;
				case "MontoFactura": return true;
				case "PuntoVenta": return false;
				case "NroComprobante": return false;
				case "Fecha": return false;
				case "Iva21": return true;
				case "Iva27": return true;
				case "Iva105": return true;
				case "IIBBCABA": return true;
				case "IIBBARBA": return true;
				case "PercepcionIVA": return true;
				case "GeneraOP": return true;
				case "EstadoId": return false;
				case "CreateFecha": return false;
				case "UpdateFecha": return true;
				case "Delete": return false;
				case "DeleteFecha": return true;
				default: return false;
			}
		}
    }
}

