using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Item
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public int CuentaID { get; set; }
		public int StockID { get; set; }
		public int? ProItemID { get; set; }
		public int EstadoID { get; set; }


		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"CuentaID: " + CuentaID.ToString() + "\r\n " + 
			"StockID: " + StockID.ToString() + "\r\n " + 
			"ProItemID: " + ProItemID.ToString() + "\r\n " ;
		}
        public Item()
        {
			ID = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "Descripcion": return false;
				case "CuentaID": return false;
				case "StockID": return false;
				case "ProItemID": return true;
				default: return false;
			}
		}
    }
}

