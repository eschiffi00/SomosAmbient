using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Form
    {
		public int FormId { get; set; }
		public string Nombre { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"FormId: " + FormId.ToString() + "\r\n " + 
			"Nombre: " + Nombre.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public Form()
        {
            FormId = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "FormId": return false;
				case "Nombre": return false;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

