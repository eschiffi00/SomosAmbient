using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class INVENTARIO_Requerimiento_Detalle
    {
		public int Id { get; set; }
		public int? PresupuestoId { get; set; }
		public int? RequerimientoId { get; set; }
		public int? PedidoId { get; set; }
		public int ProductoId { get; set; }
		public decimal Cantidad { get; set; }
		public int UnidadId { get; set; }
		public decimal Costo { get; set; }
		public DateTime CreateFecha { get; set; }
		public DateTime? UpdateFecha { get; set; }
		public DateTime? DeleteFecha { get; set; }
		public int? Delete { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"PresupuestoId: " + PresupuestoId.ToString() + "\r\n " + 
			"RequerimientoId: " + RequerimientoId.ToString() + "\r\n " + 
			"PedidoId: " + PedidoId.ToString() + "\r\n " + 
			"ProductoId: " + ProductoId.ToString() + "\r\n " + 
			"Cantidad: " + Cantidad.ToString() + "\r\n " + 
			"UnidadId: " + UnidadId.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"CreateFecha: " + CreateFecha.ToString() + "\r\n " + 
			"UpdateFecha: " + UpdateFecha.ToString() + "\r\n " + 
			"DeleteFecha: " + DeleteFecha.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " ;
		}
        public INVENTARIO_Requerimiento_Detalle()
        {
            Id = -1;

        }

		public INVENTARIO_Requerimiento GetRelatedRequerimientoId()
		{
			if (RequerimientoId != null)
			{
				INVENTARIO_Requerimiento iNVENTARIO_Requerimiento = INVENTARIO_RequerimientoOperator.GetOneByIdentity(RequerimientoId ?? 0);
				return iNVENTARIO_Requerimiento;
			}
			return null;
		}

		public INVENTARIO_Pedido GetRelatedPedidoId()
		{
			if (PedidoId != null)
			{
				INVENTARIO_Pedido iNVENTARIO_Pedido = INVENTARIO_PedidoOperator.GetOneByIdentity(PedidoId ?? 0);
				return iNVENTARIO_Pedido;
			}
			return null;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "PresupuestoId": return true;
				case "RequerimientoId": return true;
				case "PedidoId": return true;
				case "ProductoId": return false;
				case "Cantidad": return false;
				case "UnidadId": return false;
				case "Costo": return true;
				case "CreateFecha": return false;
				case "UpdateFecha": return true;
				case "DeleteFecha": return true;
				case "Delete": return true;
				default: return false;
			}
		}
    }
}

