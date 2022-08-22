using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Categorias
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int LocacionId { get; set; }
		public int SectorId { get; set; }
		public int CaracteristicaId { get; set; }
		public int SegmentoId { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"LocacionId: " + LocacionId.ToString() + "\r\n " + 
			"SectorId: " + SectorId.ToString() + "\r\n " + 
			"CaracteristicaId: " + CaracteristicaId.ToString() + "\r\n " + 
			"SegmentoId: " + SegmentoId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public Categorias()
        {
            Id = -1;

        }

		public Locaciones GetRelatedLocacionId()
		{
			Locaciones locaciones = LocacionesOperator.GetOneByIdentity(LocacionId);
			return locaciones;
		}

		public Caracteristicas GetRelatedCaracteristicaId()
		{
			Caracteristicas caracteristicas = CaracteristicasOperator.GetOneByIdentity(CaracteristicaId);
			return caracteristicas;
		}

		public Segmentos GetRelatedSegmentoId()
		{
			Segmentos segmentos = SegmentosOperator.GetOneByIdentity(SegmentoId);
			return segmentos;
		}



		public List<CostoAmbientacion> GetRelatedCostoAmbientaciones()
		{
			return CostoAmbientacionOperator.GetAll().Where(x => x.CategoriaId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "LocacionId": return false;
				case "SectorId": return false;
				case "CaracteristicaId": return false;
				case "SegmentoId": return false;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

