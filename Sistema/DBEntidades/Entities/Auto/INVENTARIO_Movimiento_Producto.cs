using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class INVENTARIO_Movimiento_Producto
    {
		public int Id { get; set; }
		public int ProductoId { get; set; }
		public DateTime Fecha { get; set; }
		public decimal CantidadAnterior { get; set; }
		public decimal CantidadNueva { get; set; }
		public int DepositoId { get; set; }
		public int EmpleadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ProductoId: " + ProductoId.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"CantidadAnterior: " + CantidadAnterior.ToString() + "\r\n " + 
			"CantidadNueva: " + CantidadNueva.ToString() + "\r\n " + 
			"DepositoId: " + DepositoId.ToString() + "\r\n " + 
			"EmpleadoId: " + EmpleadoId.ToString() + "\r\n " ;
		}
        public INVENTARIO_Movimiento_Producto()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ProductoId": return false;
				case "Fecha": return false;
				case "CantidadAnterior": return false;
				case "CantidadNueva": return false;
				case "DepositoId": return false;
				case "EmpleadoId": return false;
				default: return false;
			}
		}
    }
}

