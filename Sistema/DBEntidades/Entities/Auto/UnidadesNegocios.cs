using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class UnidadesNegocios
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " ;
		}
        public UnidadesNegocios()
        {
            Id = -1;

        }



		public List<UnidadesNegocios_TipoMovimientos> GetRelatedUnidadesNegocios_TipoMovimientoses()
		{
			return UnidadesNegocios_TipoMovimientosOperator.GetAll().Where(x => x.UnidadNegocioId == Id).ToList();
		}
		public List<TipoCatering> GetRelatedTipoCateringes()
		{
			return TipoCateringOperator.GetAll().Where(x => x.RubroId == Id).ToList();
		}
		public List<UnidadesNegocios_Proveedores> GetRelatedUnidadesNegocios_Proveedoreses()
		{
			return UnidadesNegocios_ProveedoresOperator.GetAll().Where(x => x.UnidadNegocioId == Id).ToList();
		}
		public List<Productos> GetRelatedProductoses()
		{
			return ProductosOperator.GetAll().Where(x => x.UnidadNegocioId == Id).ToList();
		}
		public List<Intermediarios> GetRelatedIntermediarioses()
		{
			return IntermediariosOperator.GetAll().Where(x => x.UnidadNegocioId == Id).ToList();
		}
		public List<ComprobantesProveedores_Detalles> GetRelatedComprobantesProveedores_Detalleses()
		{
			return ComprobantesProveedores_DetallesOperator.GetAll().Where(x => x.UnidadNegocioId == Id).ToList();
		}
		public List<TipoServicios> GetRelatedTipoServicioses()
		{
			return TipoServiciosOperator.GetAll().Where(x => x.RubroId == Id).ToList();
		}
		public List<Adicionales> GetRelatedAdicionaleses()
		{
			return AdicionalesOperator.GetAll().Where(x => x.RubroId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				default: return false;
			}
		}
    }
}

