using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class LiquidacionHorasPersonal_Detalle
    {
		public int Id { get; set; }
		public int LiquidacionPersonalHoraId { get; set; }
		public int SectorEmpresaId { get; set; }
		public int TipoEmpleadoId { get; set; }
		public int EmpleadoId { get; set; }
		public int TipoPagoId { get; set; }
		public string HoraEntrada { get; set; }
		public string HoraSalida { get; set; }
		public decimal Valor { get; set; }
		public int EstadoId { get; set; }
		public DateTime FechaCreate { get; set; }
		public DateTime? FechaUpdate { get; set; }
		public int Delete { get; set; }
		public DateTime? FechaDelete { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"LiquidacionPersonalHoraId: " + LiquidacionPersonalHoraId.ToString() + "\r\n " + 
			"SectorEmpresaId: " + SectorEmpresaId.ToString() + "\r\n " + 
			"TipoEmpleadoId: " + TipoEmpleadoId.ToString() + "\r\n " + 
			"EmpleadoId: " + EmpleadoId.ToString() + "\r\n " + 
			"TipoPagoId: " + TipoPagoId.ToString() + "\r\n " + 
			"HoraEntrada: " + HoraEntrada.ToString() + "\r\n " + 
			"HoraSalida: " + HoraSalida.ToString() + "\r\n " + 
			"Valor: " + Valor.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"FechaCreate: " + FechaCreate.ToString() + "\r\n " + 
			"FechaUpdate: " + FechaUpdate.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"FechaDelete: " + FechaDelete.ToString() + "\r\n " ;
		}
        public LiquidacionHorasPersonal_Detalle()
        {
            Id = -1;

        }

		public LiquidacionHorasPersonal GetRelatedLiquidacionPersonalHoraId()
		{
			LiquidacionHorasPersonal liquidacionHorasPersonal = LiquidacionHorasPersonalOperator.GetOneByIdentity(LiquidacionPersonalHoraId);
			return liquidacionHorasPersonal;
		}

		public Empleados GetRelatedEmpleadoId()
		{
			Empleados empleados = EmpleadosOperator.GetOneByIdentity(EmpleadoId);
			return empleados;
		}

		public TipoPagoEmpleados GetRelatedTipoPagoId()
		{
			TipoPagoEmpleados tipoPagoEmpleados = TipoPagoEmpleadosOperator.GetOneByIdentity(TipoPagoId);
			return tipoPagoEmpleados;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "LiquidacionPersonalHoraId": return false;
				case "SectorEmpresaId": return false;
				case "TipoEmpleadoId": return false;
				case "EmpleadoId": return false;
				case "TipoPagoId": return false;
				case "HoraEntrada": return false;
				case "HoraSalida": return false;
				case "Valor": return false;
				case "EstadoId": return false;
				case "FechaCreate": return false;
				case "FechaUpdate": return true;
				case "Delete": return false;
				case "FechaDelete": return true;
				default: return false;
			}
		}
    }
}

