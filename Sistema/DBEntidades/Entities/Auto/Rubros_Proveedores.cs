using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Rubros_Proveedores
    {
		public int Id { get; set; }
		public int RubroId { get; set; }
		public int ProveedorId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"RubroId: " + RubroId.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " ;
		}
        public Rubros_Proveedores()
        {
            Id = -1;

        }

		public Rubros GetRelatedRubroId()
		{
			Rubros rubros = RubrosOperator.GetOneByIdentity(RubroId);
			return rubros;
		}

		public Proveedores GetRelatedProveedorId()
		{
			Proveedores proveedores = ProveedoresOperator.GetOneByIdentity(ProveedorId);
			return proveedores;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "RubroId": return false;
				case "ProveedorId": return false;
				default: return false;
			}
		}
    }
}

