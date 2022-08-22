using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class INVENTARIO_Unidades
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public string DescripcionCorta { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"DescripcionCorta: " + DescripcionCorta.ToString() + "\r\n " ;
		}
        public INVENTARIO_Unidades()
        {
            Id = -1;

        }



		public List<INVENTARIO_Recetas> GetRelatedINVENTARIO_Recetases()
		{
			return INVENTARIO_RecetasOperator.GetAll().Where(x => x.UnidadId == Id).ToList();
		}
		public List<INVENTARIO_UnidadesConversion> GetRelatedINVENTARIO_UnidadesConversionesUnidadOriginal()
		{
			return INVENTARIO_UnidadesConversionOperator.GetAll().Where(x => x.UnidadOriginalId == Id).ToList();
		}
		public List<INVENTARIO_UnidadesConversion> GetRelatedINVENTARIO_UnidadesConversionesUnidadDestino()
		{
			return INVENTARIO_UnidadesConversionOperator.GetAll().Where(x => x.UnidadDestinoId == Id).ToList();
		}
		public List<INVENTARIO_Producto> GetRelatedINVENTARIO_ProductosUnidad()
		{
			return INVENTARIO_ProductoOperator.GetAll().Where(x => x.UnidadId == Id).ToList();
		}
		public List<INVENTARIO_Producto> GetRelatedINVENTARIO_ProductosUnidadPresentacion()
		{
			return INVENTARIO_ProductoOperator.GetAll().Where(x => x.UnidadPresentacionId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "DescripcionCorta": return false;
				default: return false;
			}
		}
    }
}

