using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ClientesPrueba
    {
		public int Id { get; set; }
		public int? ClienteId { get; set; }
		public string Persona { get; set; }
		public string tel { get; set; }
		public string mail { get; set; }
		public string organizacion { get; set; }
		public int? propietario { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ClienteId: " + ClienteId.ToString() + "\r\n " + 
			"Persona: " + Persona.ToString() + "\r\n " + 
			"tel: " + tel.ToString() + "\r\n " + 
			"mail: " + mail.ToString() + "\r\n " + 
			"organizacion: " + organizacion.ToString() + "\r\n " + 
			"propietario: " + propietario.ToString() + "\r\n " ;
		}
        public ClientesPrueba()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ClienteId": return true;
				case "Persona": return true;
				case "tel": return true;
				case "mail": return true;
				case "organizacion": return true;
				case "propietario": return true;
				default: return false;
			}
		}
    }
}

