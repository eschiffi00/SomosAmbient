using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoExperiencia
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public int EstadoID { get; set; }
		public override string ToString() 
		{
			return "\r\n " +
			"ID: " + ID.ToString() + "\r\n " +
			"ProductoID: " + Descripcion.ToString() + "\r\n " +
			"TipoRelacion: " + EstadoID.ToString() + "\r\n ";
		}
        public TipoExperiencia()
        {
			ID = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "Descripcion": return false;
				case "EstadoID": return false;
				default: return false;
			}
		}
    }
}

