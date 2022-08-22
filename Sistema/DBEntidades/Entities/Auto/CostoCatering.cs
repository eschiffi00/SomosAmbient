using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class CostoCatering
    {
		public int Id { get; set; }
		public int TipoCateringId { get; set; }
		public int CantidadPersonas { get; set; }
		public decimal Precio { get; set; }
		public decimal ValorMas5PorCiento { get; set; }
		public decimal ValorMenos5PorCiento { get; set; }
		public decimal ValorMenos10PorCiento { get; set; }
		public decimal Costo { get; set; }
		public int? ProveedorId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"TipoCateringId: " + TipoCateringId.ToString() + "\r\n " + 
			"CantidadPersonas: " + CantidadPersonas.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " + 
			"ValorMas5PorCiento: " + ValorMas5PorCiento.ToString() + "\r\n " + 
			"ValorMenos5PorCiento: " + ValorMenos5PorCiento.ToString() + "\r\n " + 
			"ValorMenos10PorCiento: " + ValorMenos10PorCiento.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " ;
		}
        public CostoCatering()
        {
            Id = -1;

        }

		public TipoCatering GetRelatedTipoCateringId()
		{
			TipoCatering tipoCatering = TipoCateringOperator.GetOneByIdentity(TipoCateringId);
			return tipoCatering;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "TipoCateringId": return false;
				case "CantidadPersonas": return false;
				case "Precio": return false;
				case "ValorMas5PorCiento": return false;
				case "ValorMenos5PorCiento": return false;
				case "ValorMenos10PorCiento": return false;
				case "Costo": return true;
				case "ProveedorId": return true;
				default: return false;
			}
		}
    }
}

