using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoCatering
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public string EsAdicional { get; set; }
		public int RubroId { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"EsAdicional: " + EsAdicional.ToString() + "\r\n " + 
			"RubroId: " + RubroId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public TipoCatering()
        {
			Id = -1;

        }

		public UnidadesNegocios GetRelatedRubroId()
		{
			UnidadesNegocios unidadesNegocios = UnidadesNegociosOperator.GetOneByIdentity(RubroId);
			return unidadesNegocios;
		}



		public List<ConfiguracionCateringTecnica> GetRelatedConfiguracionCateringTecnicas()
		{
			return ConfiguracionCateringTecnicaOperator.GetAll().Where(x => x.TipoCateringId == Id).ToList();
		}
		public List<TipoCateringAdicional> GetRelatedTipoCateringAdicionales()
		{
			return TipoCateringAdicionalOperator.GetAll().Where(x => x.TipoCateringId == Id).ToList();
		}
		public List<CostoCatering> GetRelatedCostoCateringes()
		{
			return CostoCateringOperator.GetAll().Where(x => x.TipoCateringId == Id).ToList();
		}
		public List<CostoCanon> GetRelatedCostoCanones()
		{
			return CostoCanonOperator.GetAll().Where(x => x.TipoCateringId == Id).ToList();
		}
		public List<TipoCateringTiempoProductoItem> GetRelatedTipoCateringTiempoProductoItemes()
		{
			return TipoCateringTiempoProductoItemOperator.GetAll().Where(x => x.TipoCateringId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "EsAdicional": return true;
				case "RubroId": return false;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

