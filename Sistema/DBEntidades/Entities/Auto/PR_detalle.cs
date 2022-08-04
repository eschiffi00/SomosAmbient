using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class PR_detalle
    {
		public int ID { get; set; }
		public int ProductoID { get; set; }
		public string TipoRelacion { get; set; }
		public int CodigoRelacion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"ProductoID: " + ProductoID.ToString() + "\r\n " + 
			"TipoRelacion: " + TipoRelacion.ToString() + "\r\n " + 
			"CodigoRelacion: " + CodigoRelacion.ToString() + "\r\n " ;
		}
        public PR_detalle()
        {
            ID = -1;

        }

		public Producto GetRelatedProductoID()
		{
			Producto producto = ProductoOperator.GetOneByIdentity(ProductoID);
			return producto;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "ProductoID": return false;
				case "TipoRelacion": return false;
				case "CodigoRelacion": return false;
				default: return false;
			}
		}
    }
}

