using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class UnidadesNegocios_TipoMovimientos
    {
		public int Id { get; set; }
		public int UnidadNegocioId { get; set; }
		public int TipoMovimientoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"UnidadNegocioId: " + UnidadNegocioId.ToString() + "\r\n " + 
			"TipoMovimientoId: " + TipoMovimientoId.ToString() + "\r\n " ;
		}
        public UnidadesNegocios_TipoMovimientos()
        {
            Id = -1;

        }

		public UnidadesNegocios GetRelatedUnidadNegocioId()
		{
			UnidadesNegocios unidadesNegocios = UnidadesNegociosOperator.GetOneByIdentity(UnidadNegocioId);
			return unidadesNegocios;
		}

		public TipoMovimientos GetRelatedTipoMovimientoId()
		{
			TipoMovimientos tipoMovimientos = TipoMovimientosOperator.GetOneByIdentity(TipoMovimientoId);
			return tipoMovimientos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "UnidadNegocioId": return false;
				case "TipoMovimientoId": return false;
				default: return false;
			}
		}
    }
}

