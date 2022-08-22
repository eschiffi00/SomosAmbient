using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ProductosCatering
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public string Titulo { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Titulo: " + Titulo.ToString() + "\r\n " ;
		}
        public ProductosCatering()
        {
            Id = -1;

        }



		public List<ProductosCateringItems> GetRelatedProductosCateringItemses()
		{
			return ProductosCateringItemsOperator.GetAll().Where(x => x.ProductoCateringId == Id).ToList();
		}
		public List<TipoCateringTiempoProductoItem> GetRelatedTipoCateringTiempoProductoItemes()
		{
			return TipoCateringTiempoProductoItemOperator.GetAll().Where(x => x.ProductoCateringId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "Titulo": return true;
				default: return false;
			}
		}
    }
}

