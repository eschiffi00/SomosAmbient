using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class INVENTARIO_Producto
    {
		public int Id { get; set; }
		public int RubroId { get; set; }
		public string Codigo { get; set; }
		public string CodigoBarra { get; set; }
		public string Descripcion { get; set; }
		public decimal CantidadNominal { get; set; }
		public decimal Cantidad { get; set; }
		public decimal Costo { get; set; }
		public int UnidadId { get; set; }
		public int UnidadPresentacionId { get; set; }
		public int UnidadMedidaConversionId { get; set; }
		public int? TipoMovimientoId { get; set; }
		public int? CentroCostoId { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public int Delete { get; set; }
		public DateTime? DeleteDate { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"RubroId: " + RubroId.ToString() + "\r\n " + 
			"Codigo: " + Codigo.ToString() + "\r\n " + 
			"CodigoBarra: " + CodigoBarra.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"CantidadNominal: " + CantidadNominal.ToString() + "\r\n " + 
			"Cantidad: " + Cantidad.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"UnidadId: " + UnidadId.ToString() + "\r\n " + 
			"UnidadPresentacionId: " + UnidadPresentacionId.ToString() + "\r\n " + 
			"UnidadMedidaConversionId: " + UnidadMedidaConversionId.ToString() + "\r\n " + 
			"TipoMovimientoId: " + TipoMovimientoId.ToString() + "\r\n " + 
			"CentroCostoId: " + CentroCostoId.ToString() + "\r\n " + 
			"CreateDate: " + CreateDate.ToString() + "\r\n " + 
			"UpdateDate: " + UpdateDate.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"DeleteDate: " + DeleteDate.ToString() + "\r\n " ;
		}
        public INVENTARIO_Producto()
        {
            Id = -1;

			CantidadNominal = (decimal) 0;
			Costo = (decimal) 0;
			CreateDate = DateTime.Now;
			Delete = 0;
        }

		public Rubros GetRelatedRubroId()
		{
			Rubros rubros = RubrosOperator.GetOneByIdentity(RubroId);
			return rubros;
		}

		public INVENTARIO_Unidades GetRelatedUnidadId()
		{
			INVENTARIO_Unidades iNVENTARIO_Unidades = INVENTARIO_UnidadesOperator.GetOneByIdentity(UnidadId);
			return iNVENTARIO_Unidades;
		}

		public INVENTARIO_Unidades GetRelatedUnidadPresentacionId()
		{
			INVENTARIO_Unidades iNVENTARIO_Unidades = INVENTARIO_UnidadesOperator.GetOneByIdentity(UnidadPresentacionId);
			return iNVENTARIO_Unidades;
		}

		public INVENTARIO_UnidadesConversion GetRelatedUnidadMedidaConversionId()
		{
			INVENTARIO_UnidadesConversion iNVENTARIO_UnidadesConversion = INVENTARIO_UnidadesConversionOperator.GetOneByIdentity(UnidadMedidaConversionId);
			return iNVENTARIO_UnidadesConversion;
		}



		public List<INVENTARIO_Movimiento_Producto> GetRelatedINVENTARIO_Movimiento_Productos()
		{
			return INVENTARIO_Movimiento_ProductoOperator.GetAll().Where(x => x.ProductoId == Id).ToList();
		}
		public List<INVENTARIO_ProductoDeposito> GetRelatedINVENTARIO_ProductoDepositos()
		{
			return INVENTARIO_ProductoDepositoOperator.GetAll().Where(x => x.ProductoId == Id).ToList();
		}
		public List<INVENTARIO_Recetas> GetRelatedINVENTARIO_RecetasesProducto()
		{
			return INVENTARIO_RecetasOperator.GetAll().Where(x => x.ProductoId == Id).ToList();
		}
		public List<INVENTARIO_Recetas> GetRelatedINVENTARIO_RecetasesIngrediente()
		{
			return INVENTARIO_RecetasOperator.GetAll().Where(x => x.IngredienteId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "RubroId": return false;
				case "Codigo": return false;
				case "CodigoBarra": return true;
				case "Descripcion": return false;
				case "CantidadNominal": return false;
				case "Cantidad": return false;
				case "Costo": return false;
				case "UnidadId": return false;
				case "UnidadPresentacionId": return false;
				case "UnidadMedidaConversionId": return false;
				case "TipoMovimientoId": return true;
				case "CentroCostoId": return true;
				case "CreateDate": return false;
				case "UpdateDate": return true;
				case "Delete": return false;
				case "DeleteDate": return true;
				default: return false;
			}
		}
    }
}

