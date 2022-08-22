using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class LiquidacionHorasPersonal
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public DateTime Fecha { get; set; }
		public int EstadoId { get; set; }
		public int PresupuestoId { get; set; }
		public DateTime FechaCreate { get; set; }
		public DateTime? FechaUpdate { get; set; }
		public int Delete { get; set; }
		public DateTime? FechaDelete { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"PresupuestoId: " + PresupuestoId.ToString() + "\r\n " + 
			"FechaCreate: " + FechaCreate.ToString() + "\r\n " + 
			"FechaUpdate: " + FechaUpdate.ToString() + "\r\n " + 
			"Delete: " + Delete.ToString() + "\r\n " + 
			"FechaDelete: " + FechaDelete.ToString() + "\r\n " ;
		}
        public LiquidacionHorasPersonal()
        {
            Id = -1;

        }



		public List<LiquidacionHorasPersonal_Detalle> GetRelatedLiquidacionHorasPersonal_Detalles()
		{
			return LiquidacionHorasPersonal_DetalleOperator.GetAll().Where(x => x.LiquidacionPersonalHoraId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "Fecha": return false;
				case "EstadoId": return false;
				case "PresupuestoId": return false;
				case "FechaCreate": return false;
				case "FechaUpdate": return true;
				case "Delete": return false;
				case "FechaDelete": return true;
				default: return false;
			}
		}
    }
}

