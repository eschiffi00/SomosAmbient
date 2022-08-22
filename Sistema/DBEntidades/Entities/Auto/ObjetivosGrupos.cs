using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ObjetivosGrupos
    {
		public int Id { get; set; }
		public int GrupoId { get; set; }
		public int Mes { get; set; }
		public int Anio { get; set; }
		public decimal Facturacion { get; set; }
		public int CantidadAperturas { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"GrupoId: " + GrupoId.ToString() + "\r\n " + 
			"Mes: " + Mes.ToString() + "\r\n " + 
			"Anio: " + Anio.ToString() + "\r\n " + 
			"Facturacion: " + Facturacion.ToString() + "\r\n " + 
			"CantidadAperturas: " + CantidadAperturas.ToString() + "\r\n " ;
		}
        public ObjetivosGrupos()
        {
            Id = -1;

        }

		public UsuariosGrupos GetRelatedGrupoId()
		{
			UsuariosGrupos usuariosGrupos = UsuariosGruposOperator.GetOneByIdentity(GrupoId);
			return usuariosGrupos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "GrupoId": return false;
				case "Mes": return false;
				case "Anio": return false;
				case "Facturacion": return false;
				case "CantidadAperturas": return false;
				default: return false;
			}
		}
    }
}

