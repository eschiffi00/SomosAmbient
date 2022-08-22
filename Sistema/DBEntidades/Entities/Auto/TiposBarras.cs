using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TiposBarras
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int SegmentoId { get; set; }
		public int DuracionId { get; set; }
		public string RangoEtareo { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"SegmentoId: " + SegmentoId.ToString() + "\r\n " + 
			"DuracionId: " + DuracionId.ToString() + "\r\n " + 
			"RangoEtareo: " + RangoEtareo.ToString() + "\r\n " ;
		}
        public TiposBarras()
        {
            Id = -1;

        }

		public Segmentos GetRelatedSegmentoId()
		{
			Segmentos segmentos = SegmentosOperator.GetOneByIdentity(SegmentoId);
			return segmentos;
		}

		public DuracionEvento GetRelatedDuracionId()
		{
			DuracionEvento duracionEvento = DuracionEventoOperator.GetOneByIdentity(DuracionId);
			return duracionEvento;
		}



		public List<CostoBarra> GetRelatedCostoBarras()
		{
			return CostoBarraOperator.GetAll().Where(x => x.TipoBarraId == Id).ToList();
		}
		public List<TipoBarraCategoriaItem> GetRelatedTipoBarraCategoriaItemes()
		{
			return TipoBarraCategoriaItemOperator.GetAll().Where(x => x.TipoBarraId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "SegmentoId": return false;
				case "DuracionId": return false;
				case "RangoEtareo": return false;
				default: return false;
			}
		}
    }
}

