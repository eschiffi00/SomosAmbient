using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoServicioAdicional
    {
		public int Id { get; set; }
		public int TipoServicioId { get; set; }
		public int AdicionalId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"TipoServicioId: " + TipoServicioId.ToString() + "\r\n " + 
			"AdicionalId: " + AdicionalId.ToString() + "\r\n " ;
		}
        public TipoServicioAdicional()
        {
            Id = -1;

        }

		public TipoServicios GetRelatedTipoServicioId()
		{
			TipoServicios tipoServicios = TipoServiciosOperator.GetOneByIdentity(TipoServicioId);
			return tipoServicios;
		}

		public Adicionales GetRelatedAdicionalId()
		{
			Adicionales adicionales = AdicionalesOperator.GetOneByIdentity(AdicionalId);
			return adicionales;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "TipoServicioId": return false;
				case "AdicionalId": return false;
				default: return false;
			}
		}
    }
}

