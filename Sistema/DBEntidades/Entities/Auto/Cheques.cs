using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Cheques
    {
		public int Id { get; set; }
		public string NroCheque { get; set; }
		public decimal Importe { get; set; }
		public string EmitidoA { get; set; }
		public DateTime FechaEmision { get; set; }
		public DateTime FechaVencimiento { get; set; }
		public int? ClienteId { get; set; }
		public int? ProveedorId { get; set; }
		public int BancoId { get; set; }
		public string Observaciones { get; set; }
		public string TipoCheque { get; set; }
		public int EstadoId { get; set; }
		public int? CuentaId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"NroCheque: " + NroCheque.ToString() + "\r\n " + 
			"Importe: " + Importe.ToString() + "\r\n " + 
			"EmitidoA: " + EmitidoA.ToString() + "\r\n " + 
			"FechaEmision: " + FechaEmision.ToString() + "\r\n " + 
			"FechaVencimiento: " + FechaVencimiento.ToString() + "\r\n " + 
			"ClienteId: " + ClienteId.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"BancoId: " + BancoId.ToString() + "\r\n " + 
			"Observaciones: " + Observaciones.ToString() + "\r\n " + 
			"TipoCheque: " + TipoCheque.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"CuentaId: " + CuentaId.ToString() + "\r\n " ;
		}
        public Cheques()
        {
            Id = -1;

        }

		public Bancos GetRelatedBancoId()
		{
			Bancos bancos = BancosOperator.GetOneByIdentity(BancoId);
			return bancos;
		}

		public Estados GetRelatedEstadoId()
		{
			Estados estados = EstadosOperator.GetOneByIdentity(EstadoId);
			return estados;
		}



		public List<ChequesPagosProveedores> GetRelatedChequesPagosProveedoreses()
		{
			return ChequesPagosProveedoresOperator.GetAll().Where(x => x.ChequeId == Id).ToList();
		}
		public List<Eventos> GetRelatedEventoses()
		{
			return EventosOperator.GetAll().Where(x => x.ChequeSenaId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "NroCheque": return false;
				case "Importe": return false;
				case "EmitidoA": return true;
				case "FechaEmision": return false;
				case "FechaVencimiento": return false;
				case "ClienteId": return true;
				case "ProveedorId": return true;
				case "BancoId": return false;
				case "Observaciones": return true;
				case "TipoCheque": return true;
				case "EstadoId": return false;
				case "CuentaId": return true;
				default: return false;
			}
		}
    }
}

