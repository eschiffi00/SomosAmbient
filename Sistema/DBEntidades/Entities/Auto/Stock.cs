using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Stock
    {
		public int ID { get; set; }
		public int? Cantidad { get; set; }
		public decimal Peso { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Cantidad: " + Cantidad.ToString() + "\r\n " + 
			"Peso: " + Peso.ToString() + "\r\n " ;
		}
        public Stock()
        {
            ID = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "Cantidad": return true;
				case "Peso": return true;
				default: return false;
			}
		}
    }
}

