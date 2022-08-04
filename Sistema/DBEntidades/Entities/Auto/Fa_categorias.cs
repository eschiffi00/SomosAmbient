using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Fa_categorias
    {
		public int ID { get; set; }
		public int FamiliaID { get; set; }
		public int CategoriaID { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"FamiliaID: " + FamiliaID.ToString() + "\r\n " + 
			"CategoriaID: " + CategoriaID.ToString() + "\r\n " ;
		}
        public Fa_categorias()
        {
            ID = -1;

        }

		public Familia GetRelatedFamiliaID()
		{
			Familia familia = FamiliaOperator.GetOneByIdentity(FamiliaID);
			return familia;
		}

		public Categoria GetRelatedCategoriaID()
		{
			Categoria categoria = CategoriaOperator.GetOneByIdentity(CategoriaID);
			return categoria;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "FamiliaID": return false;
				case "CategoriaID": return false;
				default: return false;
			}
		}
    }
}

