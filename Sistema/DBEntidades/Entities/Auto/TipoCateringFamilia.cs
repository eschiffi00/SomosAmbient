using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoCateringFamilia
    {
		public int Id { get; set; }
		public int GrupoId { get; set; }
		public int? TipoCateringId { get; set; }
		public int? AdicionalId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"GrupoId: " + GrupoId.ToString() + "\r\n " + 
			"TipoCateringId: " + TipoCateringId.ToString() + "\r\n " + 
			"AdicionalId: " + AdicionalId.ToString() + "\r\n " ;
		}
        public TipoCateringFamilia()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "GrupoId": return false;
				case "TipoCateringId": return true;
				case "AdicionalId": return true;
				default: return false;
			}
		}
    }
}

