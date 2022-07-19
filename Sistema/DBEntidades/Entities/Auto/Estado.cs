using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Estado
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public string Entidad { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Entidad: " + Entidad.ToString() + "\r\n " ;
		}
        public Estado()
        {
            ID = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "Descripcion": return false;
				case "Entidad": return false;
				default: return false;
			}
		}
    }
}

