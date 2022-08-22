using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class INVENTARIO_ProductoDeposito
    {
		public int Id { get; set; }
		public int ProductoId { get; set; }
		public int DepositoId { get; set; }
		public decimal Cantidad { get; set; }
		public int UnidadId { get; set; }
		public DateTime? FechaVencimiento { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ProductoId: " + ProductoId.ToString() + "\r\n " + 
			"DepositoId: " + DepositoId.ToString() + "\r\n " + 
			"Cantidad: " + Cantidad.ToString() + "\r\n " + 
			"UnidadId: " + UnidadId.ToString() + "\r\n " + 
			"FechaVencimiento: " + FechaVencimiento.ToString() + "\r\n " ;
		}
        public INVENTARIO_ProductoDeposito()
        {
            Id = -1;

        }

		public INVENTARIO_Producto GetRelatedProductoId()
		{
			INVENTARIO_Producto iNVENTARIO_Producto = INVENTARIO_ProductoOperator.GetOneByIdentity(ProductoId);
			return iNVENTARIO_Producto;
		}

		public INVENTARIO_Depositos GetRelatedDepositoId()
		{
			INVENTARIO_Depositos iNVENTARIO_Depositos = INVENTARIO_DepositosOperator.GetOneByIdentity(DepositoId);
			return iNVENTARIO_Depositos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ProductoId": return false;
				case "DepositoId": return false;
				case "Cantidad": return false;
				case "UnidadId": return false;
				case "FechaVencimiento": return true;
				default: return false;
			}
		}
    }
}

