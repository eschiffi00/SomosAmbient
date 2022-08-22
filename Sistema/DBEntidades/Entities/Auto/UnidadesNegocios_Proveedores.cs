using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class UnidadesNegocios_Proveedores
    {
		public int Id { get; set; }
		public int UnidadNegocioId { get; set; }
		public int ProveedorId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"UnidadNegocioId: " + UnidadNegocioId.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " ;
		}
        public UnidadesNegocios_Proveedores()
        {
            Id = -1;

        }

		public UnidadesNegocios GetRelatedUnidadNegocioId()
		{
			UnidadesNegocios unidadesNegocios = UnidadesNegociosOperator.GetOneByIdentity(UnidadNegocioId);
			return unidadesNegocios;
		}

		public Proveedores GetRelatedProveedorId()
		{
			Proveedores proveedores = ProveedoresOperator.GetOneByIdentity(ProveedorId);
			return proveedores;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "UnidadNegocioId": return false;
				case "ProveedorId": return false;
				default: return false;
			}
		}
    }
}

