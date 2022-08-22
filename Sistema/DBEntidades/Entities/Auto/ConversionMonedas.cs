using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ConversionMonedas
    {
		public int Id { get; set; }
		public int MonedaOrigenId { get; set; }
		public int MonedaDestinoId { get; set; }
		public string Conversion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"MonedaOrigenId: " + MonedaOrigenId.ToString() + "\r\n " + 
			"MonedaDestinoId: " + MonedaDestinoId.ToString() + "\r\n " + 
			"Conversion: " + Conversion.ToString() + "\r\n " ;
		}
        public ConversionMonedas()
        {
            Id = -1;

        }

		public Monedas GetRelatedMonedaOrigenId()
		{
			Monedas monedas = MonedasOperator.GetOneByIdentity(MonedaOrigenId);
			return monedas;
		}

		public Monedas GetRelatedMonedaDestinoId()
		{
			Monedas monedas = MonedasOperator.GetOneByIdentity(MonedaDestinoId);
			return monedas;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "MonedaOrigenId": return false;
				case "MonedaDestinoId": return false;
				case "Conversion": return false;
				default: return false;
			}
		}
    }
}

