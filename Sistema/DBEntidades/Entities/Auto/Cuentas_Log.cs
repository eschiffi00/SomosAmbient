using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Cuentas_Log
    {
		public int Id { get; set; }
		public int CuentaId { get; set; }
		public string Descripcion { get; set; }
		public decimal SaldoAnterior { get; set; }
		public decimal SaldoActual { get; set; }
		public DateTime FechaMovimiento { get; set; }
		public string TipoMovimiento { get; set; }
		public int UsuarioId { get; set; }
		public int? PagoClienteId { get; set; }
		public int? PagoProveedorId { get; set; }
		public int? TipoMovimientoId { get; set; }
		public decimal TipoCambio { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"CuentaId: " + CuentaId.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"SaldoAnterior: " + SaldoAnterior.ToString() + "\r\n " + 
			"SaldoActual: " + SaldoActual.ToString() + "\r\n " + 
			"FechaMovimiento: " + FechaMovimiento.ToString() + "\r\n " + 
			"TipoMovimiento: " + TipoMovimiento.ToString() + "\r\n " + 
			"UsuarioId: " + UsuarioId.ToString() + "\r\n " + 
			"PagoClienteId: " + PagoClienteId.ToString() + "\r\n " + 
			"PagoProveedorId: " + PagoProveedorId.ToString() + "\r\n " + 
			"TipoMovimientoId: " + TipoMovimientoId.ToString() + "\r\n " + 
			"TipoCambio: " + TipoCambio.ToString() + "\r\n " ;
		}
        public Cuentas_Log()
        {
            Id = -1;

        }

		public Cuentas GetRelatedCuentaId()
		{
			Cuentas cuentas = CuentasOperator.GetOneByIdentity(CuentaId);
			return cuentas;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "CuentaId": return false;
				case "Descripcion": return true;
				case "SaldoAnterior": return false;
				case "SaldoActual": return false;
				case "FechaMovimiento": return false;
				case "TipoMovimiento": return false;
				case "UsuarioId": return false;
				case "PagoClienteId": return true;
				case "PagoProveedorId": return true;
				case "TipoMovimientoId": return true;
				case "TipoCambio": return true;
				default: return false;
			}
		}
    }
}

