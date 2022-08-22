using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Transferencias
    {
		public int Id { get; set; }
		public int? ClienteId { get; set; }
		public int? ProveedorId { get; set; }
		public int BancoId { get; set; }
		public string NroTransferencia { get; set; }
		public decimal Importe { get; set; }
		public string NombreArchivo { get; set; }
		public object Comprobante { get; set; }
		public string ComprobanteExtension { get; set; }
		public DateTime FechaTransferencia { get; set; }
		public DateTime FechaCreate { get; set; }
		public DateTime? FechaUpdate { get; set; }
		public int Delete { get; set; }
		public DateTime? FechaDelete { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ClienteId: " + ClienteId.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"BancoId: " + BancoId.ToString() + "\r\n " + 
			"NroTransferencia: " + NroTransferencia.ToString() + "\r\n " + 
			"Importe: " + Importe.ToString() + "\r\n " + 
			"NombreArchivo: " + NombreArchivo.ToString() + "\r\n " + 
			"Comprobante: " + Comprobante.ToString() + "\r\n " + 
			"ComprobanteExtension: " + ComprobanteExtension.ToString() + "\r\n " + 
			"FechaTransferencia: " + FechaTransferencia.ToString() + "\r\n " + 
			"FechaCreate: " + FechaCreate.ToString() + "\r\n " + 
			"FechaUpdate: " + FechaUpdate.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"FechaDelete: " + FechaDelete.ToString() + "\r\n " ;
		}
        public Transferencias()
        {
            Id = -1;

			Delete = 0;
        }

		public Bancos GetRelatedBancoId()
		{
			Bancos bancos = BancosOperator.GetOneByIdentity(BancoId);
			return bancos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ClienteId": return true;
				case "ProveedorId": return true;
				case "BancoId": return false;
				case "NroTransferencia": return false;
				case "Importe": return false;
				case "NombreArchivo": return true;
				case "Comprobante": return true;
				case "ComprobanteExtension": return true;
				case "FechaTransferencia": return false;
				case "FechaCreate": return false;
				case "FechaUpdate": return true;
				case "Delete": return false;
				case "FechaDelete": return true;
				default: return false;
			}
		}
    }
}

