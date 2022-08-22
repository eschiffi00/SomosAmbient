using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TecnicaSalon
    {
		public int Id { get; set; }
		public int ProveedorId { get; set; }
		public int LocacionId { get; set; }
		public int SectorId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"LocacionId: " + LocacionId.ToString() + "\r\n " + 
			"SectorId: " + SectorId.ToString() + "\r\n " ;
		}
        public TecnicaSalon()
        {
            Id = -1;

        }

		public Proveedores GetRelatedProveedorId()
		{
			Proveedores proveedores = ProveedoresOperator.GetOneByIdentity(ProveedorId);
			return proveedores;
		}

		public Locaciones GetRelatedLocacionId()
		{
			Locaciones locaciones = LocacionesOperator.GetOneByIdentity(LocacionId);
			return locaciones;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ProveedorId": return false;
				case "LocacionId": return false;
				case "SectorId": return false;
				default: return false;
			}
		}
    }
}

