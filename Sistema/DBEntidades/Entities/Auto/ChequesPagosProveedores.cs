using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ChequesPagosProveedores
    {
		public int Id { get; set; }
		public int PagoProveedorId { get; set; }
		public int ChequeId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"PagoProveedorId: " + PagoProveedorId.ToString() + "\r\n " + 
			"ChequeId: " + ChequeId.ToString() + "\r\n " ;
		}
        public ChequesPagosProveedores()
        {
            Id = -1;

        }

		public PagosProveedores GetRelatedPagoProveedorId()
		{
			PagosProveedores pagosProveedores = PagosProveedoresOperator.GetOneByIdentity(PagoProveedorId);
			return pagosProveedores;
		}

		public Cheques GetRelatedChequeId()
		{
			Cheques cheques = ChequesOperator.GetOneByIdentity(ChequeId);
			return cheques;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "PagoProveedorId": return false;
				case "ChequeId": return false;
				default: return false;
			}
		}
    }
}

