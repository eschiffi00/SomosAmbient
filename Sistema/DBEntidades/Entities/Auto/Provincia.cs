using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Provincia
    {
		public int ProvinciaId { get; set; }
		public string Nombre { get; set; }
		public int PaisId { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ProvinciaId: " + ProvinciaId.ToString() + "\r\n " + 
			"Nombre: " + Nombre.ToString() + "\r\n " + 
			"PaisId: " + PaisId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public Provincia()
        {
            ProvinciaId = -1;

			EstadoId = 1;
        }

		public Pais GetRelatedPaisId()
		{
			Pais pais = PaisOperator.GetOneByIdentity(PaisId);
			return pais;
		}



		public List<Entidades> GetRelatedEntidadeses()
		{
			return EntidadesOperator.GetAll().Where(x => x.ProvinciaId == ProvinciaId).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ProvinciaId": return false;
				case "Nombre": return false;
				case "PaisId": return false;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

