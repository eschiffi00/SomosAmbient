using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoComprobante_Impuestos
    {
		public int Id { get; set; }
		public int TipoComprobanteId { get; set; }
		public int ImpuestoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"TipoComprobanteId: " + TipoComprobanteId.ToString() + "\r\n " + 
			"ImpuestoId: " + ImpuestoId.ToString() + "\r\n " ;
		}
        public TipoComprobante_Impuestos()
        {
            Id = -1;

        }

		public TipoComprobantes GetRelatedTipoComprobanteId()
		{
			TipoComprobantes tipoComprobantes = TipoComprobantesOperator.GetOneByIdentity(TipoComprobanteId);
			return tipoComprobantes;
		}

		public Impuestos GetRelatedImpuestoId()
		{
			Impuestos impuestos = ImpuestosOperator.GetOneByIdentity(ImpuestoId);
			return impuestos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "TipoComprobanteId": return false;
				case "ImpuestoId": return false;
				default: return false;
			}
		}
    }
}

