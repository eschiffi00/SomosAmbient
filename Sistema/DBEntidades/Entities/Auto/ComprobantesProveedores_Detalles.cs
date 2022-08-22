using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ComprobantesProveedores_Detalles
    {
		public int Id { get; set; }
		public int ComprobanteProveedorId { get; set; }
		public int TipoMoviemientoId { get; set; }
		public int CentroCostoId { get; set; }
		public decimal Importe { get; set; }
		public string Descripcion { get; set; }
		public decimal Cantidad { get; set; }
		public int? TipoImpuestoId { get; set; }
		public decimal ValorImpuesto { get; set; }
		public decimal ValorImpuestoInterno { get; set; }
		public int? PresupuestoId { get; set; }
		public int? UnidadNegocioId { get; set; }
		public int? DegustacionId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ComprobanteProveedorId: " + ComprobanteProveedorId.ToString() + "\r\n " + 
			"TipoMoviemientoId: " + TipoMoviemientoId.ToString() + "\r\n " + 
			"CentroCostoId: " + CentroCostoId.ToString() + "\r\n " + 
			"Importe: " + Importe.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Cantidad: " + Cantidad.ToString() + "\r\n " + 
			"TipoImpuestoId: " + TipoImpuestoId.ToString() + "\r\n " + 
			"ValorImpuesto: " + ValorImpuesto.ToString() + "\r\n " + 
			"ValorImpuestoInterno: " + ValorImpuestoInterno.ToString() + "\r\n " + 
			"PresupuestoId: " + PresupuestoId.ToString() + "\r\n " + 
			"UnidadNegocioId: " + UnidadNegocioId.ToString() + "\r\n " + 
			"DegustacionId: " + DegustacionId.ToString() + "\r\n " ;
		}
        public ComprobantesProveedores_Detalles()
        {
            Id = -1;

        }

		public ComprobantesProveedores GetRelatedComprobanteProveedorId()
		{
			ComprobantesProveedores comprobantesProveedores = ComprobantesProveedoresOperator.GetOneByIdentity(ComprobanteProveedorId);
			return comprobantesProveedores;
		}

		public TipoMovimientos GetRelatedTipoMoviemientoId()
		{
			TipoMovimientos tipoMovimientos = TipoMovimientosOperator.GetOneByIdentity(TipoMoviemientoId);
			return tipoMovimientos;
		}

		public CentroCostos GetRelatedCentroCostoId()
		{
			CentroCostos centroCostos = CentroCostosOperator.GetOneByIdentity(CentroCostoId);
			return centroCostos;
		}

		public Impuestos GetRelatedTipoImpuestoId()
		{
			if (TipoImpuestoId != null)
			{
				Impuestos impuestos = ImpuestosOperator.GetOneByIdentity(TipoImpuestoId ?? 0);
				return impuestos;
			}
			return null;
		}

		public Presupuestos GetRelatedPresupuestoId()
		{
			if (PresupuestoId != null)
			{
				Presupuestos presupuestos = PresupuestosOperator.GetOneByIdentity(PresupuestoId ?? 0);
				return presupuestos;
			}
			return null;
		}

		public UnidadesNegocios GetRelatedUnidadNegocioId()
		{
			if (UnidadNegocioId != null)
			{
				UnidadesNegocios unidadesNegocios = UnidadesNegociosOperator.GetOneByIdentity(UnidadNegocioId ?? 0);
				return unidadesNegocios;
			}
			return null;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ComprobanteProveedorId": return false;
				case "TipoMoviemientoId": return false;
				case "CentroCostoId": return false;
				case "Importe": return false;
				case "Descripcion": return false;
				case "Cantidad": return false;
				case "TipoImpuestoId": return true;
				case "ValorImpuesto": return true;
				case "ValorImpuestoInterno": return true;
				case "PresupuestoId": return true;
				case "UnidadNegocioId": return true;
				case "DegustacionId": return true;
				default: return false;
			}
		}
    }
}

