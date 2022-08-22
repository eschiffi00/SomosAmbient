using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Familias
    {
		public int GrupoId { get; set; }
		public int CategoriaItemId { get; set; }
		public string Titulo { get; set; }
		public string Subtitulo { get; set; }
		public string Comentario { get; set; }
		public string Edad { get; set; }
		public string Fantasia { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"GrupoId: " + GrupoId.ToString() + "\r\n " + 
			"CategoriaItemId: " + CategoriaItemId.ToString() + "\r\n " + 
			"Titulo: " + Titulo.ToString() + "\r\n " + 
			"Subtitulo: " + Subtitulo.ToString() + "\r\n " + 
			"Comentario: " + Comentario.ToString() + "\r\n " + 
			"Edad: " + Edad.ToString() + "\r\n " + 
			"Fantasia: " + Fantasia.ToString() + "\r\n " ;
		}
        public Familias()
        {
			GrupoId = -1;

        }

		public GruposItems GetRelatedGrupoId()
		{
			GruposItems gruposItems = GruposItemsOperator.GetOneByIdentity(GrupoId);
			return gruposItems;
		}

		public CategoriasItem GetRelatedCategoriaItemId()
		{
			CategoriasItem categoriasItem = CategoriasItemOperator.GetOneByIdentity(CategoriaItemId);
			return categoriasItem;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "GrupoId": return false;
				case "CategoriaItemId": return false;
				case "Titulo": return false;
				case "Subtitulo": return true;
				case "Comentario": return false;
				case "Edad": return true;
				case "Fantasia": return false;
				default: return false;
			}
		}
    }
}

