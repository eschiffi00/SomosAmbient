using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class LocacionesValorAnio
    {
		public int Id { get; set; }
		public int LocacionId { get; set; }
		public int Anio { get; set; }
		public decimal Costo { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"LocacionId: " + LocacionId.ToString() + "\r\n " + 
			"Anio: " + Anio.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " ;
		}
        public LocacionesValorAnio()
        {
            Id = -1;

        }

		public Locaciones GetRelatedLocacionId()
		{
			Locaciones locaciones = LocacionesOperator.GetOneByIdentity(LocacionId);
			return locaciones;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "LocacionId": return false;
				case "Anio": return false;
				case "Costo": return false;
				default: return false;
			}
		}
    }
}

