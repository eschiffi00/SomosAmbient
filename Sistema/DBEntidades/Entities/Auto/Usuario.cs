using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Usuario
    {
		public int UsuarioId { get; set; }
		public string LoginName { get; set; }
		public string Nombre { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime FeCreacion { get; set; }
		public int NroDeFallos { get; set; }
		public int EsAdmin { get; set; }
		public int EstadoId { get; set; }
		public int UsuarioIdCreador { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"UsuarioId: " + UsuarioId.ToString() + "\r\n " + 
			"LoginName: " + LoginName.ToString() + "\r\n " + 
			"Nombre: " + Nombre.ToString() + "\r\n " + 
			"Email: " + Email.ToString() + "\r\n " + 
			"Password: " + Password.ToString() + "\r\n " + 
			"FeCreacion: " + FeCreacion.ToString() + "\r\n " + 
			"NroDeFallos: " + NroDeFallos.ToString() + "\r\n " + 
			"EsAdmin: " + EsAdmin.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"UsuarioIdCreador: " + UsuarioIdCreador.ToString() + "\r\n " ;
		}
        public Usuario()
        {
            UsuarioId = -1;

			EstadoId = 1;
        }



		public List<UsuarioRol> GetRelatedUsuarioRoles()
		{
			return UsuarioRolOperator.GetAll().Where(x => x.UsuarioId == UsuarioId).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "UsuarioId": return false;
				case "LoginName": return false;
				case "Nombre": return true;
				case "Email": return false;
				case "Password": return true;
				case "FeCreacion": return false;
				case "NroDeFallos": return false;
				case "EsAdmin": return false;
				case "EstadoId": return false;
				case "UsuarioIdCreador": return false;
				default: return false;
			}
		}
    }
}

