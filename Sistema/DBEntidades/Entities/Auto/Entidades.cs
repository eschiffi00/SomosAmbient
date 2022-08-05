using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Entidades
    {
		public int Id { get; set; }
		public string IsProveedor { get; set; }
		public string IsCliente { get; set; }
		public string IsContacto { get; set; }
		public string RazonSocial { get; set; }
		public string ApellidoNombre { get; set; }
		public string NombreFantasia { get; set; }
		public string CUITCUIL { get; set; }
		public string CBU { get; set; }
		public string NroIIBB { get; set; }
		public string Email { get; set; }
		public int? ProvinciaId { get; set; }
		public string Localidad { get; set; }
		public string CP { get; set; }
		public int? CondicionIvaId { get; set; }
		public int? CondicionGananciaId { get; set; }
		public int? CondicionIIBBId { get; set; }
		public int? EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"IsProveedor: " + IsProveedor.ToString() + "\r\n " + 
			"IsCliente: " + IsCliente.ToString() + "\r\n " + 
			"IsContacto: " + IsContacto.ToString() + "\r\n " + 
			"RazonSocial: " + RazonSocial.ToString() + "\r\n " + 
			"ApellidoNombre: " + ApellidoNombre.ToString() + "\r\n " + 
			"NombreFantasia: " + NombreFantasia.ToString() + "\r\n " + 
			"CUITCUIL: " + CUITCUIL.ToString() + "\r\n " + 
			"CBU: " + CBU.ToString() + "\r\n " + 
			"NroIIBB: " + NroIIBB.ToString() + "\r\n " + 
			"Email: " + Email.ToString() + "\r\n " + 
			"ProvinciaId: " + ProvinciaId.ToString() + "\r\n " + 
			"Localidad: " + Localidad.ToString() + "\r\n " + 
			"CP: " + CP.ToString() + "\r\n " + 
			"CondicionIvaId: " + CondicionIvaId.ToString() + "\r\n " + 
			"CondicionGananciaId: " + CondicionGananciaId.ToString() + "\r\n " + 
			"CondicionIIBBId: " + CondicionIIBBId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public Entidades()
        {
            Id = -1;

        }

		public Provincia GetRelatedProvinciaId()
		{
			if (ProvinciaId != null)
			{
				Provincia provincia = ProvinciaOperator.GetOneByIdentity(ProvinciaId ?? 0);
				return provincia;
			}
			return null;
		}

		public CondicionIva GetRelatedCondicionIvaId()
		{
			if (CondicionIvaId != null)
			{
				CondicionIva condicionIva = CondicionIvaOperator.GetOneByIdentity(CondicionIvaId ?? 0);
				return condicionIva;
			}
			return null;
		}

		public CondicionGanancias GetRelatedCondicionGananciaId()
		{
			if (CondicionGananciaId != null)
			{
				CondicionGanancias condicionGanancias = CondicionGananciasOperator.GetOneByIdentity(CondicionGananciaId ?? 0);
				return condicionGanancias;
			}
			return null;
		}

		public CondicionIIBB GetRelatedCondicionIIBBId()
		{
			if (CondicionIIBBId != null)
			{
				CondicionIIBB condicionIIBB = CondicionIIBBOperator.GetOneByIdentity(CondicionIIBBId ?? 0);
				return condicionIIBB;
			}
			return null;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "IsProveedor": return true;
				case "IsCliente": return true;
				case "IsContacto": return true;
				case "RazonSocial": return true;
				case "ApellidoNombre": return true;
				case "NombreFantasia": return true;
				case "CUITCUIL": return true;
				case "CBU": return true;
				case "NroIIBB": return true;
				case "Email": return true;
				case "ProvinciaId": return true;
				case "Localidad": return true;
				case "CP": return true;
				case "CondicionIvaId": return true;
				case "CondicionGananciaId": return true;
				case "CondicionIIBBId": return true;
				case "EstadoId": return true;
				default: return false;
			}
		}
    }
}

