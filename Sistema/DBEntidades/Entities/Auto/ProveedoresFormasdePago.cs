using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ProveedoresFormasdePago
    {
		public int Id { get; set; }
		public int ProveedorId { get; set; }
		public int FormadePagoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"FormadePagoId: " + FormadePagoId.ToString() + "\r\n " ;
		}
        public ProveedoresFormasdePago()
        {
            Id = -1;

        }

		public Proveedores GetRelatedProveedorId()
		{
			Proveedores proveedores = ProveedoresOperator.GetOneByIdentity(ProveedorId);
			return proveedores;
		}

		public FormasdePago GetRelatedFormadePagoId()
		{
			FormasdePago formasdePago = FormasdePagoOperator.GetOneByIdentity(FormadePagoId);
			return formasdePago;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ProveedorId": return false;
				case "FormadePagoId": return false;
				default: return false;
			}
		}
    }
}

