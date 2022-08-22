using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class DuracionEvento
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int? CantidadHoras { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"CantidadHoras: " + CantidadHoras.ToString() + "\r\n " ;
		}
        public DuracionEvento()
        {
            Id = -1;

        }



		public List<Presupuestos> GetRelatedPresupuestoses()
		{
			return PresupuestosOperator.GetAll().Where(x => x.DuracionId == Id).ToList();
		}
		public List<ConfiguracionCateringTecnica> GetRelatedConfiguracionCateringTecnicas()
		{
			return ConfiguracionCateringTecnicaOperator.GetAll().Where(x => x.DuracionId == Id).ToList();
		}
		public List<TiposBarras> GetRelatedTiposBarrases()
		{
			return TiposBarrasOperator.GetAll().Where(x => x.DuracionId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "CantidadHoras": return true;
				default: return false;
			}
		}
    }
}

