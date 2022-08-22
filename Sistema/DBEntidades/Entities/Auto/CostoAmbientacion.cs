using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class CostoAmbientacion
    {
		public int Id { get; set; }
		public int CategoriaId { get; set; }
		public int ProveedorId { get; set; }
		public decimal Precio { get; set; }
		public decimal ValorMas5PorCiento { get; set; }
		public decimal ValorMenos5PorCiento { get; set; }
		public decimal ValorMenos10PorCiento { get; set; }
		public int CantidadInvitados { get; set; }
		public decimal Costo { get; set; }
		public int? SectorId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"CategoriaId: " + CategoriaId.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " + 
			"ValorMas5PorCiento: " + ValorMas5PorCiento.ToString() + "\r\n " + 
			"ValorMenos5PorCiento: " + ValorMenos5PorCiento.ToString() + "\r\n " + 
			"ValorMenos10PorCiento: " + ValorMenos10PorCiento.ToString() + "\r\n " + 
			"CantidadInvitados: " + CantidadInvitados.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"SectorId: " + SectorId.ToString() + "\r\n " ;
		}
        public CostoAmbientacion()
        {
            Id = -1;

        }

		public Categorias GetRelatedCategoriaId()
		{
			Categorias categorias = CategoriasOperator.GetOneByIdentity(CategoriaId);
			return categorias;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "CategoriaId": return false;
				case "ProveedorId": return false;
				case "Precio": return false;
				case "ValorMas5PorCiento": return false;
				case "ValorMenos5PorCiento": return false;
				case "ValorMenos10PorCiento": return false;
				case "CantidadInvitados": return false;
				case "Costo": return true;
				case "SectorId": return true;
				default: return false;
			}
		}
    }
}

