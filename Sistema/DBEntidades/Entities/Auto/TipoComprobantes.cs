using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoComprobantes
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int? CondicionIvaId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"CondicionIvaId: " + CondicionIvaId.ToString() + "\r\n " ;
		}
        public TipoComprobantes()
        {
            Id = -1;

        }

		public CondicionIva GetRelatedCondicionIvaId()
		{
			if (CondicionIvaId != null)
			{
				CondicionIva condicionIva = CondicionIvaOperator.GetOneByIdentity(CondicionIvaId ?? 0);
				return condicionIva;
			}
			return null;
		}



		public List<FacturasCliente> GetRelatedFacturasClientes()
		{
			return FacturasClienteOperator.GetAll().Where(x => x.TipoComprobanteId == Id).ToList();
		}
		public List<TipoComprobante_Impuestos> GetRelatedTipoComprobante_Impuestoses()
		{
			return TipoComprobante_ImpuestosOperator.GetAll().Where(x => x.TipoComprobanteId == Id).ToList();
		}
		public List<ComprobantesProveedores> GetRelatedComprobantesProveedoreses()
		{
			return ComprobantesProveedoresOperator.GetAll().Where(x => x.TipoComprobanteId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "CondicionIvaId": return true;
				default: return false;
			}
		}
    }
}

