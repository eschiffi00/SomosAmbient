using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ComprobantePagoProveedor
    {
		public int Id { get; set; }
		public int ComprobanteProveedorId { get; set; }
		public int PagoProveedorId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ComprobanteProveedorId: " + ComprobanteProveedorId.ToString() + "\r\n " + 
			"PagoProveedorId: " + PagoProveedorId.ToString() + "\r\n " ;
		}
        public ComprobantePagoProveedor()
        {
            Id = -1;

        }

		public ComprobantesProveedores GetRelatedComprobanteProveedorId()
		{
			ComprobantesProveedores comprobantesProveedores = ComprobantesProveedoresOperator.GetOneByIdentity(ComprobanteProveedorId);
			return comprobantesProveedores;
		}

		public PagosProveedores GetRelatedPagoProveedorId()
		{
			PagosProveedores pagosProveedores = PagosProveedoresOperator.GetOneByIdentity(PagoProveedorId);
			return pagosProveedores;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ComprobanteProveedorId": return false;
				case "PagoProveedorId": return false;
				default: return false;
			}
		}
    }
}

