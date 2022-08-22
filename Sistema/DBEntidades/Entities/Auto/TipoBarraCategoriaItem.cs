using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoBarraCategoriaItem
    {
		public int Id { get; set; }
		public int TipoBarraId { get; set; }
		public int? CategoriaItemId { get; set; }
		public int? ItemId { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"TipoBarraId: " + TipoBarraId.ToString() + "\r\n " + 
			"CategoriaItemId: " + CategoriaItemId.ToString() + "\r\n " + 
			"ItemId: " + ItemId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public TipoBarraCategoriaItem()
        {
            Id = -1;

        }

		public TiposBarras GetRelatedTipoBarraId()
		{
			TiposBarras tiposBarras = TiposBarrasOperator.GetOneByIdentity(TipoBarraId);
			return tiposBarras;
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
				case "TipoBarraId": return false;
				case "CategoriaItemId": return true;
				case "ItemId": return true;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

