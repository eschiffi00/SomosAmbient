using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Rubros
    {
		public int RubroId { get; set; }
		public string Descripcion { get; set; }
		public string LetraCodigo { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"RubroId: " + RubroId.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"LetraCodigo: " + LetraCodigo.ToString() + "\r\n " ;
		}
        public Rubros()
        {
            RubroId = -1;

        }



		public List<INVENTARIO_Producto> GetRelatedINVENTARIO_Productos()
		{
			return INVENTARIO_ProductoOperator.GetAll().Where(x => x.RubroId == RubroId).ToList();
		}
		public List<Rubros_Proveedores> GetRelatedRubros_Proveedoreses()
		{
			return Rubros_ProveedoresOperator.GetAll().Where(x => x.RubroId == RubroId).ToList();
		}
		public List<CodigoPorRubro> GetRelatedCodigoPorRubros()
		{
			return CodigoPorRubroOperator.GetAll().Where(x => x.RubroId == RubroId).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "RubroId": return false;
				case "Descripcion": return true;
				case "LetraCodigo": return true;
				default: return false;
			}
		}
    }
}

