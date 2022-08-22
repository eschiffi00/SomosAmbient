using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ItemsListado
    {
		public int Id { get; set; }
		public string Detalle { get; set; }
		public int? ItemDetalleId { get; set; }
		public int? CategoriaItemId { get; set; }
		public string CategoriaDescripcion { get; set; }
		public int? CuentaId { get; set; }
		public string CuentaDescripcion { get; set; }
		public decimal Costo { get; set; }
		public decimal Margen { get; set; }
		public decimal Precio { get; set; }
		public int? DepositoId { get; set; }
		public string Unidad{ get; set; }
		public decimal? Cantidad { get; set; }
		public int EstadoId { get; set; }
		public string Estado { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + Id.ToString() + "\r\n " + 
			"Detalle: " + Detalle.ToString() + "\r\n " + 
			"Detalle del item: " + ItemDetalleId.ToString() + "\r\n " +
			"CategoriaItemId: " + CategoriaItemId.ToString() + "\r\n " +
			"CategoriaDescripcion: " + CategoriaDescripcion.ToString() + "\r\n " +
			"CuentaID: " + CategoriaDescripcion.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"Margen: " + Margen.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " +
			"DepositoId: " + DepositoId.ToString() + "\r\n " +
			"Unidad: " + Unidad.ToString() + "\r\n " +
			"Cantidad: " + Cantidad.ToString() + "\r\n " +
			"EstadoID: " + EstadoId.ToString() + "\r\n " +
			"Estado: " + Estado.ToString() + "\r\n " ;
		}
		public ItemsListado()
		{
			Id = -1;
			ItemDetalleId = -1;
			CategoriaItemId = -1;
			CuentaId = -1;
			DepositoId = -1;

		}
	}
	public partial class ItemsCombo
    {
		public int Id { get; set; }
		public string Detalle { get; set; }
	}
	
}

