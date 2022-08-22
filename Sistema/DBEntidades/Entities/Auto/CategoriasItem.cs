using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class CategoriasItem
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int? CategoriaItemPadreId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"CategoriaItemPadreId: " + CategoriaItemPadreId.ToString() + "\r\n " ;
		}
        public CategoriasItem()
        {
            Id = -1;

        }



		public List<Familias> GetRelatedFamiliases()
		{
			return FamiliasOperator.GetAll().Where(x => x.CategoriaItemId == Id).ToList();
		}
		public List<TipoCateringTiempoProductoItem> GetRelatedTipoCateringTiempoProductoItemes()
		{
			return TipoCateringTiempoProductoItemOperator.GetAll().Where(x => x.CategoriaItemId == Id).ToList();
		}
		public List<Items> GetRelatedItemses()
		{
			return ItemsOperator.GetAll().Where(x => x.CategoriaItemId == Id).ToList();
		}
		public List<TipoBarraCategoriaItem> GetRelatedTipoBarraCategoriaItemes()
		{
			return TipoBarraCategoriaItemOperator.GetAll().Where(x => x.CategoriaItemId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "CategoriaItemPadreId": return true;
				default: return false;
			}
		}
    }
}

