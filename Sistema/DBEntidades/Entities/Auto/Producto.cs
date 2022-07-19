using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Producto
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public int CategoriaID { get; set; }
		public decimal Costo { get; set; }
		public decimal Margen { get; set; }
		public decimal Precio { get; set; }
		public int StockID { get; set; }
		public int EstadoID { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"CategoriaID: " + CategoriaID.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"Margen: " + Margen.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " + 
			"StockID: " + StockID.ToString() + "\r\n " + 
			"EstadoID: " + EstadoID.ToString() + "\r\n " ;
		}
        public Producto()
        {
			ID = -1;
			CategoriaID = -1;
			StockID = -1;
			EstadoID = -1;
        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "Descripcion": return false;
				case "CategoriaID": return false;
				case "Costo": return true;
				case "Margen": return true;
				case "Precio": return true;
				case "StockID": return false;
				case "EstadoID": return false;
				default: return false;
			}
		}
    }
}

