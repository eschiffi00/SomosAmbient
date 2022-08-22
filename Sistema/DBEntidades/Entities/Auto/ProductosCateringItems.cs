using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ProductosCateringItems
    {
		public int Id { get; set; }
		public int ProductoCateringId { get; set; }
		public int ItemId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ProductoCateringId: " + ProductoCateringId.ToString() + "\r\n " + 
			"ItemId: " + ItemId.ToString() + "\r\n " ;
		}
        public ProductosCateringItems()
        {
            Id = -1;

        }

		public ProductosCatering GetRelatedProductoCateringId()
		{
			ProductosCatering productosCatering = ProductosCateringOperator.GetOneByIdentity(ProductoCateringId);
			return productosCatering;
		}

		public Items GetRelatedItemId()
		{
			Items items = ItemsOperator.GetOneByIdentity(ItemId);
			return items;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ProductoCateringId": return false;
				case "ItemId": return false;
				default: return false;
			}
		}
    }
}

