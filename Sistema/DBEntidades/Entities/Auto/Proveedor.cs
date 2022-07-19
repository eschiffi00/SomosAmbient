using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Proveedor
    {
		public int ID { get; set; }
		public string RazonSocial { get; set; }
		public int? Cuit { get; set; }
		public int Propio { get; set; }
		public string NombreFantasia { get; set; }
		public int? Telefono { get; set; }
		public int? CBU { get; set; }
		public int? NroCliente { get; set; }
		public int? NroIIBB { get; set; }
		public string Localidad { get; set; }
		public string Provincia { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"RazonSocial: " + RazonSocial.ToString() + "\r\n " + 
			"Cuit: " + Cuit.ToString() + "\r\n " + 
			"Propio: " + Propio.ToString() + "\r\n " + 
			"NombreFantasia: " + NombreFantasia.ToString() + "\r\n " + 
			"Telefono: " + Telefono.ToString() + "\r\n " + 
			"CBU: " + CBU.ToString() + "\r\n " + 
			"NroCliente: " + NroCliente.ToString() + "\r\n " + 
			"NroIIBB: " + NroIIBB.ToString() + "\r\n " + 
			"Localidad: " + Localidad.ToString() + "\r\n " + 
			"Provincia: " + Provincia.ToString() + "\r\n " ;
		}
        public Proveedor()
        {
			ID = -1;

        }





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "RazonSocial": return true;
				case "Cuit": return true;
				case "Propio": return false;
				case "NombreFantasia": return true;
				case "Telefono": return true;
				case "CBU": return true;
				case "NroCliente": return true;
				case "NroIIBB": return true;
				case "Localidad": return true;
				case "Provincia": return true;
				default: return false;
			}
		}
    }
}

