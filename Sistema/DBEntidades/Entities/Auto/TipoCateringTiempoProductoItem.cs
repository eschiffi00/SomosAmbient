using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoCateringTiempoProductoItem
    {
		public int Id { get; set; }
		public int TipoCateringId { get; set; }
		public int TiempoId { get; set; }
		public int? ProductoCateringId { get; set; }
		public int? CategoriaItemId { get; set; }
		public int? ItemId { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"TipoCateringId: " + TipoCateringId.ToString() + "\r\n " + 
			"TiempoId: " + TiempoId.ToString() + "\r\n " + 
			"ProductoCateringId: " + ProductoCateringId.ToString() + "\r\n " + 
			"CategoriaItemId: " + CategoriaItemId.ToString() + "\r\n " + 
			"ItemId: " + ItemId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public TipoCateringTiempoProductoItem()
        {
            Id = -1;

        }

		public TipoCatering GetRelatedTipoCateringId()
		{
			TipoCatering tipoCatering = TipoCateringOperator.GetOneByIdentity(TipoCateringId);
			return tipoCatering;
		}

		public Tiempos GetRelatedTiempoId()
		{
			Tiempos tiempos = TiemposOperator.GetOneByIdentity(TiempoId);
			return tiempos;
		}

		public ProductosCatering GetRelatedProductoCateringId()
		{
			if (ProductoCateringId != null)
			{
				ProductosCatering productosCatering = ProductosCateringOperator.GetOneByIdentity(ProductoCateringId ?? 0);
				return productosCatering;
			}
			return null;
		}

		public CategoriasItem GetRelatedCategoriaItemId()
		{
			if (CategoriaItemId != null)
			{
				CategoriasItem categoriasItem = CategoriasItemOperator.GetOneByIdentity(CategoriaItemId ?? 0);
				return categoriasItem;
			}
			return null;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "TipoCateringId": return false;
				case "TiempoId": return false;
				case "ProductoCateringId": return true;
				case "CategoriaItemId": return true;
				case "ItemId": return true;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

