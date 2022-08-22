using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class GruposItems
    {
		public int Id { get; set; }
		public string Codigo { get; set; }
		public string Tipo { get; set; }
		public string Descripcion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Codigo: " + Codigo.ToString() + "\r\n " + 
			"Tipo: " + Tipo.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " ;
		}
        public GruposItems()
        {
            Id = -1;

        }



		public List<Familias> GetRelatedFamiliases()
		{
			return FamiliasOperator.GetAll().Where(x => x.GrupoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Codigo": return false;
				case "Tipo": return false;
				case "Descripcion": return true;
				default: return false;
			}
		}
    }
}

