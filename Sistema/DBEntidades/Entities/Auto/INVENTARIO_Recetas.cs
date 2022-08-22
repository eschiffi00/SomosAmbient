using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class INVENTARIO_Recetas
    {
		public int Id { get; set; }
		public int ProductoId { get; set; }
		public int IngredienteId { get; set; }
		public int UnidadId { get; set; }
		public decimal Cantidad { get; set; }
		public int Porciones { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ProductoId: " + ProductoId.ToString() + "\r\n " + 
			"IngredienteId: " + IngredienteId.ToString() + "\r\n " + 
			"UnidadId: " + UnidadId.ToString() + "\r\n " + 
			"Cantidad: " + Cantidad.ToString() + "\r\n " + 
			"Porciones: " + Porciones.ToString() + "\r\n " ;
		}
        public INVENTARIO_Recetas()
        {
            Id = -1;

        }

		public INVENTARIO_Producto GetRelatedProductoId()
		{
			INVENTARIO_Producto iNVENTARIO_Producto = INVENTARIO_ProductoOperator.GetOneByIdentity(ProductoId);
			return iNVENTARIO_Producto;
		}

		public INVENTARIO_Producto GetRelatedIngredienteId()
		{
			INVENTARIO_Producto iNVENTARIO_Producto = INVENTARIO_ProductoOperator.GetOneByIdentity(IngredienteId);
			return iNVENTARIO_Producto;
		}

		public INVENTARIO_Unidades GetRelatedUnidadId()
		{
			INVENTARIO_Unidades iNVENTARIO_Unidades = INVENTARIO_UnidadesOperator.GetOneByIdentity(UnidadId);
			return iNVENTARIO_Unidades;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ProductoId": return false;
				case "IngredienteId": return false;
				case "UnidadId": return false;
				case "Cantidad": return false;
				case "Porciones": return false;
				default: return false;
			}
		}
    }
}

