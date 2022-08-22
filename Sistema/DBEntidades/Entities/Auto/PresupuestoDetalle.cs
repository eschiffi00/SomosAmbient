using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class PresupuestoDetalle
    {
		public int Id { get; set; }
		public int PresupuestoId { get; set; }
		public int UnidadNegocioId { get; set; }
		public int? ProveedorId { get; set; }
		public int? ServicioId { get; set; }
		public int? LocacionId { get; set; }
		public int ProductoId { get; set; }
		public decimal PrecioItem { get; set; }
		public decimal PrecioLista { get; set; }
		public decimal PrecioMas5 { get; set; }
		public decimal PrecioMenos5 { get; set; }
		public decimal PrecioMenos10 { get; set; }
		public string CodigoItem { get; set; }
		public decimal Descuentos { get; set; }
		public decimal Incremento { get; set; }
		public string PrecioSeleccionado { get; set; }
		public decimal PorcentajeComision { get; set; }
		public decimal ValorSeleccionado { get; set; }
		public decimal Comision { get; set; }
		public int? CantidadAdicional { get; set; }
		public decimal Costo { get; set; }
		public decimal Cannon { get; set; }
		public int? TipoLogisticaId { get; set; }
		public int? LocalidadId { get; set; }
		public int? CantInvitadosLogistica { get; set; }
		public decimal Logistica { get; set; }
		public decimal UsoCocina { get; set; }
		public decimal ValorIntermediario { get; set; }
		public decimal CostoSillas { get; set; }
		public decimal CostoMesas { get; set; }
		public decimal PrecioSillas { get; set; }
		public decimal PrecioMesas { get; set; }
		public int? version { get; set; }
		public int? EstadoId { get; set; }
		public DateTime? FechaAprobacion { get; set; }
		public string Comentario { get; set; }
		public string ComentarioProveedor { get; set; }
		public DateTime? FechaCobroItem { get; set; }
		public int? EstadoProveedor { get; set; }
		public int AnuloCanon { get; set; }
		public DateTime? FechaCreate { get; set; }
		public DateTime? FechaUpdate { get; set; }
		public int? Delete { get; set; }
		public DateTime? FechaDelete { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"PresupuestoId: " + PresupuestoId.ToString() + "\r\n " + 
			"UnidadNegocioId: " + UnidadNegocioId.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"ServicioId: " + ServicioId.ToString() + "\r\n " + 
			"LocacionId: " + LocacionId.ToString() + "\r\n " + 
			"ProductoId: " + ProductoId.ToString() + "\r\n " + 
			"PrecioItem: " + PrecioItem.ToString() + "\r\n " + 
			"PrecioLista: " + PrecioLista.ToString() + "\r\n " + 
			"PrecioMas5: " + PrecioMas5.ToString() + "\r\n " + 
			"PrecioMenos5: " + PrecioMenos5.ToString() + "\r\n " + 
			"PrecioMenos10: " + PrecioMenos10.ToString() + "\r\n " + 
			"CodigoItem: " + CodigoItem.ToString() + "\r\n " + 
			"Descuentos: " + Descuentos.ToString() + "\r\n " + 
			"Incremento: " + Incremento.ToString() + "\r\n " + 
			"PrecioSeleccionado: " + PrecioSeleccionado.ToString() + "\r\n " + 
			"PorcentajeComision: " + PorcentajeComision.ToString() + "\r\n " + 
			"ValorSeleccionado: " + ValorSeleccionado.ToString() + "\r\n " + 
			"Comision: " + Comision.ToString() + "\r\n " + 
			"CantidadAdicional: " + CantidadAdicional.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"Cannon: " + Cannon.ToString() + "\r\n " + 
			"TipoLogisticaId: " + TipoLogisticaId.ToString() + "\r\n " + 
			"LocalidadId: " + LocalidadId.ToString() + "\r\n " + 
			"CantInvitadosLogistica: " + CantInvitadosLogistica.ToString() + "\r\n " + 
			"Logistica: " + Logistica.ToString() + "\r\n " + 
			"UsoCocina: " + UsoCocina.ToString() + "\r\n " + 
			"ValorIntermediario: " + ValorIntermediario.ToString() + "\r\n " + 
			"CostoSillas: " + CostoSillas.ToString() + "\r\n " + 
			"CostoMesas: " + CostoMesas.ToString() + "\r\n " + 
			"PrecioSillas: " + PrecioSillas.ToString() + "\r\n " + 
			"PrecioMesas: " + PrecioMesas.ToString() + "\r\n " + 
			"version: " + version.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"FechaAprobacion: " + FechaAprobacion.ToString() + "\r\n " + 
			"Comentario: " + Comentario.ToString() + "\r\n " + 
			"ComentarioProveedor: " + ComentarioProveedor.ToString() + "\r\n " + 
			"FechaCobroItem: " + FechaCobroItem.ToString() + "\r\n " + 
			"EstadoProveedor: " + EstadoProveedor.ToString() + "\r\n " + 
			"AnuloCanon: " + AnuloCanon.ToString() + "\r\n " + 
			"FechaCreate: " + FechaCreate.ToString() + "\r\n " + 
			"FechaUpdate: " + FechaUpdate.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"FechaDelete: " + FechaDelete.ToString() + "\r\n " ;
		}
        public PresupuestoDetalle()
        {
            Id = -1;

			AnuloCanon = 0;
			FechaCreate = DateTime.Now;
			Delete = 0;
        }

		public Presupuestos GetRelatedPresupuestoId()
		{
			Presupuestos presupuestos = PresupuestosOperator.GetOneByIdentity(PresupuestoId);
			return presupuestos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "PresupuestoId": return false;
				case "UnidadNegocioId": return false;
				case "ProveedorId": return true;
				case "ServicioId": return true;
				case "LocacionId": return true;
				case "ProductoId": return false;
				case "PrecioItem": return false;
				case "PrecioLista": return false;
				case "PrecioMas5": return false;
				case "PrecioMenos5": return false;
				case "PrecioMenos10": return false;
				case "CodigoItem": return false;
				case "Descuentos": return true;
				case "Incremento": return true;
				case "PrecioSeleccionado": return false;
				case "PorcentajeComision": return false;
				case "ValorSeleccionado": return false;
				case "Comision": return false;
				case "CantidadAdicional": return true;
				case "Costo": return true;
				case "Cannon": return true;
				case "TipoLogisticaId": return true;
				case "LocalidadId": return true;
				case "CantInvitadosLogistica": return true;
				case "Logistica": return true;
				case "UsoCocina": return true;
				case "ValorIntermediario": return true;
				case "CostoSillas": return true;
				case "CostoMesas": return true;
				case "PrecioSillas": return true;
				case "PrecioMesas": return true;
				case "version": return true;
				case "EstadoId": return true;
				case "FechaAprobacion": return true;
				case "Comentario": return true;
				case "ComentarioProveedor": return true;
				case "FechaCobroItem": return true;
				case "EstadoProveedor": return true;
				case "AnuloCanon": return false;
				case "FechaCreate": return true;
				case "FechaUpdate": return true;
				case "Delete": return true;
				case "FechaDelete": return true;
				default: return false;
			}
		}
    }
}

