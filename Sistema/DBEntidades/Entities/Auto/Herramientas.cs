using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Herramientas
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public object Archivo { get; set; }
		public string ExtencionArchivo { get; set; }
		public int? CategoriaArchivoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Archivo: " + Archivo.ToString() + "\r\n " + 
			"ExtencionArchivo: " + ExtencionArchivo.ToString() + "\r\n " + 
			"CategoriaArchivoId: " + CategoriaArchivoId.ToString() + "\r\n " ;
		}
        public Herramientas()
        {
            Id = -1;

        }

		public CategoriasArchivos GetRelatedCategoriaArchivoId()
		{
			if (CategoriaArchivoId != null)
			{
				CategoriasArchivos categoriasArchivos = CategoriasArchivosOperator.GetOneByIdentity(CategoriaArchivoId ?? 0);
				return categoriasArchivos;
			}
			return null;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "Archivo": return true;
				case "ExtencionArchivo": return true;
				case "CategoriaArchivoId": return true;
				default: return false;
			}
		}
    }
}

