using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class AdicionalesItems
    {
		public int Id { get; set; }
		public int AdicionalId { get; set; }
		public int ItemId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"AdicionalId: " + AdicionalId.ToString() + "\r\n " + 
			"ItemId: " + ItemId.ToString() + "\r\n " ;
		}
        public AdicionalesItems()
        {
            Id = -1;

        }

		public Adicionales GetRelatedAdicionalId()
		{
			Adicionales adicionales = AdicionalesOperator.GetOneByIdentity(AdicionalId);
			return adicionales;
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
				case "AdicionalId": return false;
				case "ItemId": return false;
				default: return false;
			}
		}
    }
}

