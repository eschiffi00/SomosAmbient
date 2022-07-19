using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ItemDetalle
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public int CuentaID { get; set; }
		public string CuentaDescripcion { get; set; }
		public int StockID { get; set; }
		public int? ProItemID { get; set; }
		public int EstadoID { get; set; }
		public int? Cantidad { get; set; }
		public decimal? Peso { get; set; }


		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"CuentaID: " + CuentaID.ToString() + "\r\n " + 
			"CuentaID: " + CuentaDescripcion.ToString() + "\r\n " + 
			"StockID: " + StockID.ToString() + "\r\n " + 
			"ProItemID: " + ProItemID.ToString() + "\r\n " +
			"EstadoID: " + EstadoID.ToString() + "\r\n " +
			"Cantidad: " + EstadoID.ToString() + "\r\n " +
			"Peso: " + EstadoID.ToString() + "\r\n " ;
		}
    }
}

