using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class UsuariosGrupos
    {
		public int Id { get; set; }
		public string Nombre { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Nombre: " + Nombre.ToString() + "\r\n " ;
		}
        public UsuariosGrupos()
        {
            Id = -1;

        }



		public List<ObjetivosGrupos> GetRelatedObjetivosGruposes()
		{
			return ObjetivosGruposOperator.GetAll().Where(x => x.GrupoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Nombre": return false;
				default: return false;
			}
		}
    }
}

