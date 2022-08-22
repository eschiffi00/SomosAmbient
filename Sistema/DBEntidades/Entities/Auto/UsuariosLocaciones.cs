using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class UsuariosLocaciones
    {
		public int Id { get; set; }
		public int? EmpleadoId { get; set; }
		public int? LocacionId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"EmpleadoId: " + EmpleadoId.ToString() + "\r\n " + 
			"LocacionId: " + LocacionId.ToString() + "\r\n " ;
		}
        public UsuariosLocaciones()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "EmpleadoId": return true;
				case "LocacionId": return true;
				default: return false;
			}
		}
    }
}

