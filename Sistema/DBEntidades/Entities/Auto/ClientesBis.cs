using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ClientesBis
    {
		public int Id { get; set; }
		public string ApellidoNombre { get; set; }
		public string RazonSocial { get; set; }
		public string CUILCUIT { get; set; }
		public string CondicionIva { get; set; }
		public string Direccion { get; set; }
		public string PersonaFisicaJuridica { get; set; }
		public string TipoCliente { get; set; }
		public string MailContactoContratacion { get; set; }
		public string MailContactoAdministracion { get; set; }
		public string MailContactoTesoreia { get; set; }
		public string MailContactoOrganizacion { get; set; }
		public string TelContactoContratacion { get; set; }
		public string TelContactoAdministracion { get; set; }
		public string TelContactoTesoreria { get; set; }
		public string TelContactoOrganizacion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ApellidoNombre: " + ApellidoNombre.ToString() + "\r\n " + 
			"RazonSocial: " + RazonSocial.ToString() + "\r\n " + 
			"CUILCUIT: " + CUILCUIT.ToString() + "\r\n " + 
			"CondicionIva: " + CondicionIva.ToString() + "\r\n " + 
			"Direccion: " + Direccion.ToString() + "\r\n " + 
			"PersonaFisicaJuridica: " + PersonaFisicaJuridica.ToString() + "\r\n " + 
			"TipoCliente: " + TipoCliente.ToString() + "\r\n " + 
			"MailContactoContratacion: " + MailContactoContratacion.ToString() + "\r\n " + 
			"MailContactoAdministracion: " + MailContactoAdministracion.ToString() + "\r\n " + 
			"MailContactoTesoreia: " + MailContactoTesoreia.ToString() + "\r\n " + 
			"MailContactoOrganizacion: " + MailContactoOrganizacion.ToString() + "\r\n " + 
			"TelContactoContratacion: " + TelContactoContratacion.ToString() + "\r\n " + 
			"TelContactoAdministracion: " + TelContactoAdministracion.ToString() + "\r\n " + 
			"TelContactoTesoreria: " + TelContactoTesoreria.ToString() + "\r\n " + 
			"TelContactoOrganizacion: " + TelContactoOrganizacion.ToString() + "\r\n " ;
		}
        public ClientesBis()
        {
            Id = -1;

        }



		public List<FacturasCliente> GetRelatedFacturasClientes()
		{
			return FacturasClienteOperator.GetAll().Where(x => x.ClienteId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ApellidoNombre": return true;
				case "RazonSocial": return true;
				case "CUILCUIT": return false;
				case "CondicionIva": return false;
				case "Direccion": return false;
				case "PersonaFisicaJuridica": return false;
				case "TipoCliente": return false;
				case "MailContactoContratacion": return true;
				case "MailContactoAdministracion": return true;
				case "MailContactoTesoreia": return true;
				case "MailContactoOrganizacion": return true;
				case "TelContactoContratacion": return true;
				case "TelContactoAdministracion": return true;
				case "TelContactoTesoreria": return true;
				case "TelContactoOrganizacion": return true;
				default: return false;
			}
		}
    }
}

