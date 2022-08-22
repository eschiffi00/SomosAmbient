using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Usuarios
    {
		public int EmpleadoId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public int? PerfilId { get; set; }
		public int EstadoId { get; set; }
		public string CodigoSeguridad { get; set; }
		public string RutaCodigoSeguridad { get; set; }
		public int HabilitarCambioPassword { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"EmpleadoId: " + EmpleadoId.ToString() + "\r\n " + 
			"UserName: " + UserName.ToString() + "\r\n " + 
			"Password: " + Password.ToString() + "\r\n " + 
			"PerfilId: " + PerfilId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"CodigoSeguridad: " + CodigoSeguridad.ToString() + "\r\n " + 
			"RutaCodigoSeguridad: " + RutaCodigoSeguridad.ToString() + "\r\n " + 
			"HabilitarCambioPassword: " + HabilitarCambioPassword.ToString() + "\r\n " ;
		}
        public Usuarios()
        {
			EmpleadoId = -1;

			HabilitarCambioPassword = 0;
        }

		public Empleados GetRelatedEmpleadoId()
		{
			Empleados empleados = EmpleadosOperator.GetOneByIdentity(EmpleadoId);
			return empleados;
		}

		public Perfiles GetRelatedPerfilId()
		{
			if (PerfilId != null)
			{
				Perfiles perfiles = PerfilesOperator.GetOneByIdentity(PerfilId ?? 0);
				return perfiles;
			}
			return null;
		}

		public Estados GetRelatedEstadoId()
		{
			Estados estados = EstadosOperator.GetOneByIdentity(EstadoId);
			return estados;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "EmpleadoId": return false;
				case "UserName": return false;
				case "Password": return false;
				case "PerfilId": return true;
				case "EstadoId": return false;
				case "CodigoSeguridad": return true;
				case "RutaCodigoSeguridad": return true;
				case "HabilitarCambioPassword": return false;
				default: return false;
			}
		}
    }
}

