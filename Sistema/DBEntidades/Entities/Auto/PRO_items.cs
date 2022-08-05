using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class PRO_items
    {
		public int ID { get; set; }
		public int ProveedorID { get; set; }
		public decimal Precio { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"ProveedorID: " + ProveedorID.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " ;
		}
        public PRO_items()
        {
            ID = -1;

        }

		public Proveedor GetRelatedProveedorID()
		{
			Proveedor proveedor = ProveedorOperator.GetOneByIdentity(ProveedorID);
			return proveedor;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "ProveedorID": return false;
				case "Precio": return true;
				default: return false;
			}
		}
    }
}

