using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Segmentos
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " ;
		}
        public Segmentos()
        {
            Id = -1;

        }



		public List<CostosPaquetesCIAmbientacion> GetRelatedCostosPaquetesCIAmbientaciones()
		{
			return CostosPaquetesCIAmbientacionOperator.GetAll().Where(x => x.SegmentoId == Id).ToList();
		}
		public List<DegustacionDetalle> GetRelatedDegustacionDetalles()
		{
			return DegustacionDetalleOperator.GetAll().Where(x => x.SegmentoId == Id).ToList();
		}
		public List<Categorias> GetRelatedCategoriases()
		{
			return CategoriasOperator.GetAll().Where(x => x.SegmentoId == Id).ToList();
		}
		public List<Presupuestos> GetRelatedPresupuestoses()
		{
			return PresupuestosOperator.GetAll().Where(x => x.SegmentoId == Id).ToList();
		}
		public List<ConfiguracionCateringTecnica> GetRelatedConfiguracionCateringTecnicas()
		{
			return ConfiguracionCateringTecnicaOperator.GetAll().Where(x => x.SegmentoId == Id).ToList();
		}
		public List<TiposBarras> GetRelatedTiposBarrases()
		{
			return TiposBarrasOperator.GetAll().Where(x => x.SegmentoId == Id).ToList();
		}
		public List<CostoCanon> GetRelatedCostoCanones()
		{
			return CostoCanonOperator.GetAll().Where(x => x.SegmentoId == Id).ToList();
		}
		public List<CostoTecnica> GetRelatedCostoTecnicas()
		{
			return CostoTecnicaOperator.GetAll().Where(x => x.SegmentoId == Id).ToList();
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

