using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Cat_Productos
    {
		public int ID { get; set; }
		public int CategoriaID { get; set; }
		public int ProductoID { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"CategoriaID: " + CategoriaID.ToString() + "\r\n " + 
			"ProductoID: " + ProductoID.ToString() + "\r\n " ;
		}
        public Cat_Productos()
        {
			ID = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "CategoriaID": return false;
				case "ProductoID": return false;
				default: return false;
			}
		}
    }
}

