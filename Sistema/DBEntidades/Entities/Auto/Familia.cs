using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Familia
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " ;
		}
        public Familia()
        {
            ID = -1;

        }



		public List<Fa_categorias> GetRelatedFa_categoriases()
		{
			return Fa_categoriasOperator.GetAll().Where(x => x.FamiliaID == ID).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "Descripcion": return false;
				default: return false;
			}
		}
    }
}

