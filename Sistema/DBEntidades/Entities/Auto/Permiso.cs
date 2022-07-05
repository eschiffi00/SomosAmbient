using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Permiso
    {
		public int PermisoId { get; set; }
		public int RolId { get; set; }
		public int FormId { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"PermisoId: " + PermisoId.ToString() + "\r\n " + 
			"RolId: " + RolId.ToString() + "\r\n " + 
			"FormId: " + FormId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public Permiso()
        {
            PermisoId = -1;

			EstadoId = 1;
        }

		public Rol GetRelatedRolId()
		{
			Rol rol = RolOperator.GetOneByIdentity(RolId);
			return rol;
		}

		public Form GetRelatedFormId()
		{
			Form form = FormOperator.GetOneByIdentity(FormId);
			return form;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "PermisoId": return false;
				case "RolId": return false;
				case "FormId": return false;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

