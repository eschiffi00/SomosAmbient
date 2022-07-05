using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Pais
    {
		public int PaisId { get; set; }
		public string Nombre { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"PaisId: " + PaisId.ToString() + "\r\n " + 
			"Nombre: " + Nombre.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public Pais()
        {
            PaisId = -1;

			EstadoId = 1;
        }



		public List<Provincia> GetRelatedProvincias()
		{
			return ProvinciaOperator.GetAll().Where(x => x.PaisId == PaisId).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "PaisId": return false;
				case "Nombre": return false;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

