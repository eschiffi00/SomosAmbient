using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoPagoEmpleados
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " ;
		}
        public TipoPagoEmpleados()
        {
            Id = -1;

        }



		public List<LiquidacionHorasPersonal_Detalle> GetRelatedLiquidacionHorasPersonal_Detalles()
		{
			return LiquidacionHorasPersonal_DetalleOperator.GetAll().Where(x => x.TipoPagoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				default: return false;
			}
		}
    }
}

