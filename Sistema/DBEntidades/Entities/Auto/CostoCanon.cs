using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class CostoCanon
    {
		public int Id { get; set; }
		public int SegmentoId { get; set; }
		public int CaracteristicaId { get; set; }
		public int TipoCateringId { get; set; }
		public decimal CanonInterno { get; set; }
		public decimal CanonExterno { get; set; }
		public decimal UsoCocina { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"SegmentoId: " + SegmentoId.ToString() + "\r\n " + 
			"CaracteristicaId: " + CaracteristicaId.ToString() + "\r\n " + 
			"TipoCateringId: " + TipoCateringId.ToString() + "\r\n " + 
			"CanonInterno: " + CanonInterno.ToString() + "\r\n " + 
			"CanonExterno: " + CanonExterno.ToString() + "\r\n " + 
			"UsoCocina: " + UsoCocina.ToString() + "\r\n " ;
		}
        public CostoCanon()
        {
            Id = -1;

        }

		public Segmentos GetRelatedSegmentoId()
		{
			Segmentos segmentos = SegmentosOperator.GetOneByIdentity(SegmentoId);
			return segmentos;
		}

		public Caracteristicas GetRelatedCaracteristicaId()
		{
			Caracteristicas caracteristicas = CaracteristicasOperator.GetOneByIdentity(CaracteristicaId);
			return caracteristicas;
		}

		public TipoCatering GetRelatedTipoCateringId()
		{
			TipoCatering tipoCatering = TipoCateringOperator.GetOneByIdentity(TipoCateringId);
			return tipoCatering;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "SegmentoId": return false;
				case "CaracteristicaId": return false;
				case "TipoCateringId": return false;
				case "CanonInterno": return true;
				case "CanonExterno": return true;
				case "UsoCocina": return true;
				default: return false;
			}
		}
    }
}

