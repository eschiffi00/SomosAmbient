using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class UsuarioRol
    {
		public int UsuarioRolId { get; set; }
		public int UsuarioId { get; set; }
		public int RolId { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"UsuarioRolId: " + UsuarioRolId.ToString() + "\r\n " + 
			"UsuarioId: " + UsuarioId.ToString() + "\r\n " + 
			"RolId: " + RolId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public UsuarioRol()
        {
            UsuarioRolId = -1;

			EstadoId = 1;
        }

		public Usuario GetRelatedUsuarioId()
		{
			Usuario usuario = UsuarioOperator.GetOneByIdentity(UsuarioId);
			return usuario;
		}

		public Rol GetRelatedRolId()
		{
			Rol rol = RolOperator.GetOneByIdentity(RolId);
			return rol;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "UsuarioRolId": return false;
				case "UsuarioId": return false;
				case "RolId": return false;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

