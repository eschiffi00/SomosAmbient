using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Items
    {
		public int Id { get; set; }
		public string Detalle { get; set; }
		public int CategoriaItemId { get; set; }
		public int ItemDetalleId { get; set; }
		public int CuentaId { get; set; }
		public decimal Costo { get; set; }
		public decimal Margen { get; set; }
		public decimal Precio { get; set; }
		public int DepositoId { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Detalle: " + Detalle.ToString() + "\r\n " + 
			"Categoria: " + CategoriaItemId.ToString() + "\r\n " +
			"Costo: " + Costo.ToString() + "\r\n " + 
			"Margen: " + Margen.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " + 
			"DepositoId: " + DepositoId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public Items()
        {
           Id  = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Detalle": return false;
				case "CategoriaItemId": return false;
				case "Costo": return false;
				case "Margen": return false;
				case "Precio": return false;
				case "DepositoId": return false;
				case "EstadoId": return true;
				default: return false;
			}
		}
    }
}

