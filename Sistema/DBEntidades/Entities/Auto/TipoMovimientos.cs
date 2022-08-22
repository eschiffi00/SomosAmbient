using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoMovimientos
    {
		public int Id { get; set; }
		public string Codigo { get; set; }
		public string Descripcion { get; set; }
		public string Tipo { get; set; }
		public string Visible { get; set; }
		public int? IsEgreso { get; set; }
		public int? IsIngreso { get; set; }
		public string TipoGasto { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Codigo: " + Codigo.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Tipo: " + Tipo.ToString() + "\r\n " + 
			"Visible: " + Visible.ToString() + "\r\n " + 
			"IsEgreso: " + IsEgreso.ToString() + "\r\n " + 
			"IsIngreso: " + IsIngreso.ToString() + "\r\n " + 
			"TipoGasto: " + TipoGasto.ToString() + "\r\n " ;
		}
        public TipoMovimientos()
        {
            Id = -1;

        }



		public List<PagosClientes> GetRelatedPagosClienteses()
		{
			return PagosClientesOperator.GetAll().Where(x => x.TipoMovimientoId == Id).ToList();
		}
		public List<Retenciones> GetRelatedRetencioneses()
		{
			return RetencionesOperator.GetAll().Where(x => x.TipoMovimimientoId == Id).ToList();
		}
		public List<Movimientos> GetRelatedMovimientoses()
		{
			return MovimientosOperator.GetAll().Where(x => x.TipoMoviemientoId == Id).ToList();
		}
		public List<ComprobantesProveedores_Detalles> GetRelatedComprobantesProveedores_Detalleses()
		{
			return ComprobantesProveedores_DetallesOperator.GetAll().Where(x => x.TipoMoviemientoId == Id).ToList();
		}
		public List<UnidadesNegocios_TipoMovimientos> GetRelatedUnidadesNegocios_TipoMovimientoses()
		{
			return UnidadesNegocios_TipoMovimientosOperator.GetAll().Where(x => x.TipoMovimientoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Codigo": return false;
				case "Descripcion": return false;
				case "Tipo": return true;
				case "Visible": return true;
				case "IsEgreso": return true;
				case "IsIngreso": return true;
				case "TipoGasto": return true;
				default: return false;
			}
		}
    }
}

