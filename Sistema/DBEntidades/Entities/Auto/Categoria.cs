using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Categoria
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public int CategoriaPadre { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"CategoriaPadre: " + CategoriaPadre.ToString() + "\r\n " ;
		}
        public Categoria()
        {
			ID = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "Descripcion": return false;
				case "CategoriaPadre": return false;
				default: return false;
			}
		}
    }
}

