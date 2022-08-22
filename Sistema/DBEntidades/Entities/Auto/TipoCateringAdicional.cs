using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoCateringAdicional
    {
		public int Id { get; set; }
		public int TipoCateringId { get; set; }
		public int AdicionalId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"TipoCateringId: " + TipoCateringId.ToString() + "\r\n " + 
			"AdicionalId: " + AdicionalId.ToString() + "\r\n " ;
		}
        public TipoCateringAdicional()
        {
            Id = -1;

        }

		public TipoCatering GetRelatedTipoCateringId()
		{
			TipoCatering tipoCatering = TipoCateringOperator.GetOneByIdentity(TipoCateringId);
			return tipoCatering;
		}

		public Adicionales GetRelatedAdicionalId()
		{
			Adicionales adicionales = AdicionalesOperator.GetOneByIdentity(AdicionalId);
			return adicionales;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "TipoCateringId": return false;
				case "AdicionalId": return false;
				default: return false;
			}
		}
    }
}

