using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Movimientos
    {
		public int Id { get; set; }
		public int TipoMoviemientoId { get; set; }
		public decimal Importe { get; set; }
		public string Descripcion { get; set; }
		public DateTime Fecha { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"TipoMoviemientoId: " + TipoMoviemientoId.ToString() + "\r\n " + 
			"Importe: " + Importe.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " ;
		}
        public Movimientos()
        {
            Id = -1;

        }

		public TipoMovimientos GetRelatedTipoMoviemientoId()
		{
			TipoMovimientos tipoMovimientos = TipoMovimientosOperator.GetOneByIdentity(TipoMoviemientoId);
			return tipoMovimientos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "TipoMoviemientoId": return false;
				case "Importe": return false;
				case "Descripcion": return false;
				case "Fecha": return false;
				default: return false;
			}
		}
    }
}

