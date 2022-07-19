using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Tiempo
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public int Duracion { get; set; }
		public int Orden { get; set; }
		public override string ToString() 
		{
			return "\r\n " +
			"ID: " + ID.ToString() + "\r\n " +
			"ProductoID: " + Descripcion.ToString() + "\r\n " +
			"TipoRelacion: " + Duracion.ToString() + "\r\n " +
			"CodigoRelacion: " + Orden.ToString() + "\r\n ";
		}
        public Tiempo()
        {
			ID = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "Descripcion": return false;
				case "Duracion": return false;
				case "Orden": return true;
				default: return false;
			}
		}
    }
}

