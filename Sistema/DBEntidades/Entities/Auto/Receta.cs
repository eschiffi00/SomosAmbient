using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Receta
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public int StockID { get; set; }
		public int EstadoID { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"StockID: " + StockID.ToString() + "\r\n " +
			"EstadoID: " + StockID.ToString() + "\r\n " ;
		}
        public Receta()
        {
			ID = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "Descripcion": return false;
				case "StockID": return false;
				case "EstadoID": return false;
				case "RecPasosID": return false;
				default: return false;
			}
		}
    }
}

