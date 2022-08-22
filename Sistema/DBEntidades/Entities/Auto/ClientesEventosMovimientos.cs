using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ClientesEventosMovimientos
    {
		public int Id { get; set; }
		public int ClienteId { get; set; }
		public int EventoId { get; set; }
		public string Comentario { get; set; }
		public DateTime FechaSeguimiento { get; set; }
		public int? EstadoId { get; set; }
		public int? EmpleadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ClienteId: " + ClienteId.ToString() + "\r\n " + 
			"EventoId: " + EventoId.ToString() + "\r\n " + 
			"Comentario: " + Comentario.ToString() + "\r\n " + 
			"FechaSeguimiento: " + FechaSeguimiento.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"EmpleadoId: " + EmpleadoId.ToString() + "\r\n " ;
		}
        public ClientesEventosMovimientos()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ClienteId": return false;
				case "EventoId": return false;
				case "Comentario": return true;
				case "FechaSeguimiento": return false;
				case "EstadoId": return true;
				case "EmpleadoId": return true;
				default: return false;
			}
		}
    }
}

