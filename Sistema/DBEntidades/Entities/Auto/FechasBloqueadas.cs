using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class FechasBloqueadas
    {
		public int Id { get; set; }
		public DateTime FechaBloqueada { get; set; }
		public int LocacionId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"FechaBloqueada: " + FechaBloqueada.ToString() + "\r\n " + 
			"LocacionId: " + LocacionId.ToString() + "\r\n " ;
		}
        public FechasBloqueadas()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "FechaBloqueada": return false;
				case "LocacionId": return false;
				default: return false;
			}
		}
    }
}

