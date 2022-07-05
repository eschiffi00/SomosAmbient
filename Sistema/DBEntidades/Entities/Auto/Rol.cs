using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Rol
    {
		public int RolId { get; set; }
		public string Nombre { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"RolId: " + RolId.ToString() + "\r\n " + 
			"Nombre: " + Nombre.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public Rol()
        {
            RolId = -1;

			EstadoId = 1;
        }



		public List<Bloqueo> GetRelatedBloqueos()
		{
			return BloqueoOperator.GetAll().Where(x => x.RolId == RolId).ToList();
		}
		public List<Permiso> GetRelatedPermisos()
		{
			return PermisoOperator.GetAll().Where(x => x.RolId == RolId).ToList();
		}
		public List<UsuarioRol> GetRelatedUsuarioRoles()
		{
			return UsuarioRolOperator.GetAll().Where(x => x.RolId == RolId).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "RolId": return false;
				case "Nombre": return false;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

