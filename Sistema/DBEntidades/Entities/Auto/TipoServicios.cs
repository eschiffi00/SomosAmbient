using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoServicios
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int RubroId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"RubroId: " + RubroId.ToString() + "\r\n " ;
		}
        public TipoServicios()
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
			return ConfiguracionCateringTecnicaOperator.GetAll().Where(x => x.TipoServicioId == Id).ToList();
		}
		public List<TipoServicioAdicional> GetRelatedTipoServicioAdicionales()
		{
			return TipoServicioAdicionalOperator.GetAll().Where(x => x.TipoServicioId == Id).ToList();
		}
		public List<CostoTecnica> GetRelatedCostoTecnicas()
		{
			return CostoTecnicaOperator.GetAll().Where(x => x.TipoServicioId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "RubroId": return false;
				default: return false;
			}
		}
    }
}

