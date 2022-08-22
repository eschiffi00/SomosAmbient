using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Intermediarios
    {
		public int Id { get; set; }
		public int ProveedorId { get; set; }
		public int UnidadNegocioId { get; set; }
		public int LocacionId { get; set; }
		public string TipoComision { get; set; }
		public decimal Porcentaje { get; set; }
		public decimal Valor { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"UnidadNegocioId: " + UnidadNegocioId.ToString() + "\r\n " + 
			"LocacionId: " + LocacionId.ToString() + "\r\n " + 
			"TipoComision: " + TipoComision.ToString() + "\r\n " + 
			"Porcentaje: " + Porcentaje.ToString() + "\r\n " + 
			"Valor: " + Valor.ToString() + "\r\n " ;
		}
        public Intermediarios()
        {
            Id = -1;

        }

		public Proveedores GetRelatedProveedorId()
		{
			Proveedores proveedores = ProveedoresOperator.GetOneByIdentity(ProveedorId);
			return proveedores;
		}

		public UnidadesNegocios GetRelatedUnidadNegocioId()
		{
			UnidadesNegocios unidadesNegocios = UnidadesNegociosOperator.GetOneByIdentity(UnidadNegocioId);
			return unidadesNegocios;
		}

		public Locaciones GetRelatedLocacionId()
		{
			Locaciones locaciones = LocacionesOperator.GetOneByIdentity(LocacionId);
			return locaciones;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ProveedorId": return false;
				case "UnidadNegocioId": return false;
				case "LocacionId": return false;
				case "TipoComision": return false;
				case "Porcentaje": return true;
				case "Valor": return true;
				default: return false;
			}
		}
    }
}

