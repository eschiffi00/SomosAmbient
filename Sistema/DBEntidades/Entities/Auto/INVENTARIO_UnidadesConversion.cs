using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class INVENTARIO_UnidadesConversion
    {
		public int Id { get; set; }
		public int UnidadOriginalId { get; set; }
		public int UnidadDestinoId { get; set; }
		public decimal Cantidad { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"UnidadOriginalId: " + UnidadOriginalId.ToString() + "\r\n " + 
			"UnidadDestinoId: " + UnidadDestinoId.ToString() + "\r\n " + 
			"Cantidad: " + Cantidad.ToString() + "\r\n " ;
		}
        public INVENTARIO_UnidadesConversion()
        {
            Id = -1;

        }

		public INVENTARIO_Unidades GetRelatedUnidadOriginalId()
		{
			INVENTARIO_Unidades iNVENTARIO_Unidades = INVENTARIO_UnidadesOperator.GetOneByIdentity(UnidadOriginalId);
			return iNVENTARIO_Unidades;
		}

		public INVENTARIO_Unidades GetRelatedUnidadDestinoId()
		{
			INVENTARIO_Unidades iNVENTARIO_Unidades = INVENTARIO_UnidadesOperator.GetOneByIdentity(UnidadDestinoId);
			return iNVENTARIO_Unidades;
		}



		public List<INVENTARIO_Producto> GetRelatedINVENTARIO_Productos()
		{
			return INVENTARIO_ProductoOperator.GetAll().Where(x => x.UnidadMedidaConversionId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "UnidadOriginalId": return false;
				case "UnidadDestinoId": return false;
				case "Cantidad": return false;
				default: return false;
			}
		}
    }
}

