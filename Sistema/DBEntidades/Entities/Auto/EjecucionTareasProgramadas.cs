using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class EjecucionTareasProgramadas
    {
		public int Id { get; set; }
		public DateTime FechaEjecucion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"FechaEjecucion: " + FechaEjecucion.ToString() + "\r\n " ;
		}
        public EjecucionTareasProgramadas()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "FechaEjecucion": return false;
				default: return false;
			}
		}
    }
}

