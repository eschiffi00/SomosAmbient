using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Email
    {
		public int Id { get; set; }
		public string MailDestino { get; set; }
		public string MailOrigen { get; set; }
		public string Asunto { get; set; }
		public string Cuerpo { get; set; }
		public int? EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"MailDestino: " + MailDestino.ToString() + "\r\n " + 
			"MailOrigen: " + MailOrigen.ToString() + "\r\n " + 
			"Asunto: " + Asunto.ToString() + "\r\n " + 
			"Cuerpo: " + Cuerpo.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public Email()
        {
            Id = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "MailDestino": return true;
				case "MailOrigen": return true;
				case "Asunto": return true;
				case "Cuerpo": return true;
				case "EstadoId": return true;
				default: return false;
			}
		}
    }
}

