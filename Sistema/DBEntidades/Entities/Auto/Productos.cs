using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Productos
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int TipoMovimientoId { get; set; }
		public int CentroCostoId { get; set; }
		public int TipoImpuestoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"TipoMovimientoId: " + TipoMovimientoId.ToString() + "\r\n " + 
			"CentroCostoId: " + CentroCostoId.ToString() + "\r\n " + 
			"TipoImpuestoId: " + TipoImpuestoId.ToString() + "\r\n " ;
		}
        public Productos()
        {
            Id = -1;

        }

		public TipoMovimientos GetRelatedTipoMovimientoId()
		{
			TipoMovimientos tipoMovimientos = TipoMovimientosOperator.GetOneByIdentity(TipoMovimientoId);
			return tipoMovimientos;
		}

		public CentroCostos GetRelatedCentroCostoId()
		{
			CentroCostos centroCostos = CentroCostosOperator.GetOneByIdentity(CentroCostoId);
			return centroCostos;
		}

		public TipoImpuestos GetRelatedTipoImpuestoId()
		{
			TipoImpuestos tipoImpuestos = TipoImpuestosOperator.GetOneByIdentity(TipoImpuestoId);
			return tipoImpuestos;
		}



		public List<DocumentosDetalle> GetRelatedDocumentosDetalles()
		{
			return DocumentosDetalleOperator.GetAll().Where(x => x.ProductoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "TipoMovimientoId": return false;
				case "CentroCostoId": return false;
				case "TipoImpuestoId": return false;
				default: return false;
			}
		}
    }
}

