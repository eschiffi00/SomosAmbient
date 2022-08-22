using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Perfiles
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " ;
		}
        public Perfiles()
        {
            Id = -1;

        }



		public List<Productos> GetRelatedProductoses()
		{
			return ProductosOperator.GetAll().Where(x => x.PerfilId == Id).ToList();
		}
		public List<Usuarios> GetRelatedUsuarioses()
		{
			return UsuariosOperator.GetAll().Where(x => x.PerfilId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				default: return false;
			}
		}
    }
}

