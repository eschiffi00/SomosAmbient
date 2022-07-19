using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ProductoDetalle
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public int CategoriaID { get; set; }
		public string CategoriaDescripcion { get; set; }
		public decimal Costo { get; set; }
		public decimal Margen { get; set; }
		public decimal Precio { get; set; }
		public int StockID { get; set; }
		public int? Cantidad { get; set; }
		public decimal? Peso { get; set; }
		public int EstadoID { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"CategoriaID: " + CategoriaID.ToString() + "\r\n " +
			"CategoriaDescripcion: " + CategoriaDescripcion.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"Margen: " + Margen.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " + 
			"StockID: " + StockID.ToString() + "\r\n " +
			"Cantidad: " + Cantidad.ToString() + "\r\n " +
			"Peso: " + Peso.ToString() + "\r\n " + 
			"EstadoID: " + EstadoID.ToString() + "\r\n " ;
		}
    }
}

