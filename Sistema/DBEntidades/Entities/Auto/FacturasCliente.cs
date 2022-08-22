using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class FacturasCliente
    {
		public int Id { get; set; }
		public int ClienteId { get; set; }
		public int TipoComprobanteId { get; set; }
		public int EmpresaId { get; set; }
		public DateTime Fecha { get; set; }
		public decimal Importe { get; set; }
		public int NroFactura { get; set; }
		public int EstadoId { get; set; }
		public DateTime CreateFecha { get; set; }
		public DateTime? UpdateFecha { get; set; }
		public int Delete { get; set; }
		public DateTime? FechaDelete { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ClienteId: " + ClienteId.ToString() + "\r\n " + 
			"TipoComprobanteId: " + TipoComprobanteId.ToString() + "\r\n " + 
			"EmpresaId: " + EmpresaId.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"Importe: " + Importe.ToString() + "\r\n " + 
			"NroFactura: " + NroFactura.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"CreateFecha: " + CreateFecha.ToString() + "\r\n " + 
			"UpdateFecha: " + UpdateFecha.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"FechaDelete: " + FechaDelete.ToString() + "\r\n " ;
		}
        public FacturasCliente()
        {
            Id = -1;

			Delete = 0;
        }

		public ClientesBis GetRelatedClienteId()
		{
			ClientesBis clientesBis = ClientesBisOperator.GetOneByIdentity(ClienteId);
			return clientesBis;
		}

		public TipoComprobantes GetRelatedTipoComprobanteId()
		{
			TipoComprobantes tipoComprobantes = TipoComprobantesOperator.GetOneByIdentity(TipoComprobanteId);
			return tipoComprobantes;
		}

		public Empresas GetRelatedEmpresaId()
		{
			Empresas empresas = EmpresasOperator.GetOneByIdentity(EmpresaId);
			return empresas;
		}

		public Estados GetRelatedEstadoId()
		{
			Estados estados = EstadosOperator.GetOneByIdentity(EstadoId);
			return estados;
		}



		public List<FacturaClienteDetalle> GetRelatedFacturaClienteDetalles()
		{
			return FacturaClienteDetalleOperator.GetAll().Where(x => x.FacturaClienteId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ClienteId": return false;
				case "TipoComprobanteId": return false;
				case "EmpresaId": return false;
				case "Fecha": return false;
				case "Importe": return false;
				case "NroFactura": return false;
				case "EstadoId": return false;
				case "CreateFecha": return false;
				case "UpdateFecha": return true;
				case "Delete": return false;
				case "FechaDelete": return true;
				default: return false;
			}
		}
    }
}

