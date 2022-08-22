using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class PagosClientes
    {
		public int Id { get; set; }
		public int PresupuestoId { get; set; }
		public int EmpresaId { get; set; }
		public int? ProveedorCannonId { get; set; }
		public int FormadePagoId { get; set; }
		public int TipoMovimientoId { get; set; }
		public DateTime FechaPago { get; set; }
		public decimal Importe { get; set; }
		public string Concepto { get; set; }
		public string NroRecibo { get; set; }
		public DateTime FechaCreate { get; set; }
		public DateTime? FechaUpdate { get; set; }
		public int Delete { get; set; }
		public DateTime? FechaDelete { get; set; }
		public int EmpleadoId { get; set; }
		public decimal Indexacion { get; set; }
		public decimal IvaPorcentaje { get; set; }
		public int CuentaId { get; set; }
		public string TipoPago { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"PresupuestoId: " + PresupuestoId.ToString() + "\r\n " + 
			"EmpresaId: " + EmpresaId.ToString() + "\r\n " + 
			"ProveedorCannonId: " + ProveedorCannonId.ToString() + "\r\n " + 
			"FormadePagoId: " + FormadePagoId.ToString() + "\r\n " + 
			"TipoMovimientoId: " + TipoMovimientoId.ToString() + "\r\n " + 
			"FechaPago: " + FechaPago.ToString() + "\r\n " + 
			"Importe: " + Importe.ToString() + "\r\n " + 
			"Concepto: " + Concepto.ToString() + "\r\n " + 
			"NroRecibo: " + NroRecibo.ToString() + "\r\n " + 
			"FechaCreate: " + FechaCreate.ToString() + "\r\n " + 
			"FechaUpdate: " + FechaUpdate.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"FechaDelete: " + FechaDelete.ToString() + "\r\n " + 
			"EmpleadoId: " + EmpleadoId.ToString() + "\r\n " + 
			"Indexacion: " + Indexacion.ToString() + "\r\n " + 
			"IvaPorcentaje: " + IvaPorcentaje.ToString() + "\r\n " + 
			"CuentaId: " + CuentaId.ToString() + "\r\n " + 
			"TipoPago: " + TipoPago.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public PagosClientes()
        {
            Id = -1;

			Delete = 0;
        }

		public Presupuestos GetRelatedPresupuestoId()
		{
			Presupuestos presupuestos = PresupuestosOperator.GetOneByIdentity(PresupuestoId);
			return presupuestos;
		}

		public Empresas GetRelatedEmpresaId()
		{
			Empresas empresas = EmpresasOperator.GetOneByIdentity(EmpresaId);
			return empresas;
		}

		public FormasdePago GetRelatedFormadePagoId()
		{
			FormasdePago formasdePago = FormasdePagoOperator.GetOneByIdentity(FormadePagoId);
			return formasdePago;
		}

		public TipoMovimientos GetRelatedTipoMovimientoId()
		{
			TipoMovimientos tipoMovimientos = TipoMovimientosOperator.GetOneByIdentity(TipoMovimientoId);
			return tipoMovimientos;
		}

		public Estados GetRelatedEstadoId()
		{
			Estados estados = EstadosOperator.GetOneByIdentity(EstadoId);
			return estados;
		}



		public List<Retenciones> GetRelatedRetencioneses()
		{
			return RetencionesOperator.GetAll().Where(x => x.PagoClienteId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "PresupuestoId": return false;
				case "EmpresaId": return false;
				case "ProveedorCannonId": return true;
				case "FormadePagoId": return false;
				case "TipoMovimientoId": return false;
				case "FechaPago": return false;
				case "Importe": return false;
				case "Concepto": return true;
				case "NroRecibo": return true;
				case "FechaCreate": return false;
				case "FechaUpdate": return true;
				case "Delete": return false;
				case "FechaDelete": return true;
				case "EmpleadoId": return false;
				case "Indexacion": return true;
				case "IvaPorcentaje": return true;
				case "CuentaId": return false;
				case "TipoPago": return true;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

