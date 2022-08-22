using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class ConfiguracionCateringTecnica
    {
		public int Id { get; set; }
		public int SegmentoId { get; set; }
		public int CaracteristicaId { get; set; }
		public int DuracionId { get; set; }
		public int MomentoDiaId { get; set; }
		public int TipoCateringId { get; set; }
		public int? TipoServicioId { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"SegmentoId: " + SegmentoId.ToString() + "\r\n " + 
			"CaracteristicaId: " + CaracteristicaId.ToString() + "\r\n " + 
			"DuracionId: " + DuracionId.ToString() + "\r\n " + 
			"MomentoDiaId: " + MomentoDiaId.ToString() + "\r\n " + 
			"TipoCateringId: " + TipoCateringId.ToString() + "\r\n " + 
			"TipoServicioId: " + TipoServicioId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public ConfiguracionCateringTecnica()
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

		public DuracionEvento GetRelatedDuracionId()
		{
			DuracionEvento duracionEvento = DuracionEventoOperator.GetOneByIdentity(DuracionId);
			return duracionEvento;
		}

		public MomentosDias GetRelatedMomentoDiaId()
		{
			MomentosDias momentosDias = MomentosDiasOperator.GetOneByIdentity(MomentoDiaId);
			return momentosDias;
		}

		public TipoCatering GetRelatedTipoCateringId()
		{
			TipoCatering tipoCatering = TipoCateringOperator.GetOneByIdentity(TipoCateringId);
			return tipoCatering;
		}

		public TipoServicios GetRelatedTipoServicioId()
		{
			if (TipoServicioId != null)
			{
				TipoServicios tipoServicios = TipoServiciosOperator.GetOneByIdentity(TipoServicioId ?? 0);
				return tipoServicios;
			}
			return null;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "SegmentoId": return false;
				case "CaracteristicaId": return false;
				case "DuracionId": return false;
				case "MomentoDiaId": return false;
				case "TipoCateringId": return false;
				case "TipoServicioId": return true;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

