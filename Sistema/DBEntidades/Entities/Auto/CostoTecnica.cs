using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class CostoTecnica
    {
		public int Id { get; set; }
		public int SegmentoId { get; set; }
		public int TipoServicioId { get; set; }
		public decimal Precio { get; set; }
		public int Anio { get; set; }
		public int Mes { get; set; }
		public string Dia { get; set; }
		public int ProveedorId { get; set; }
		public decimal ValorMas5PorCiento { get; set; }
		public decimal ValorMenos5PorCiento { get; set; }
		public decimal ValorMenos10PorCiento { get; set; }
		public decimal Costo { get; set; }
		public int? SectorId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"SegmentoId: " + SegmentoId.ToString() + "\r\n " + 
			"TipoServicioId: " + TipoServicioId.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " + 
			"Anio: " + Anio.ToString() + "\r\n " + 
			"Mes: " + Mes.ToString() + "\r\n " + 
			"Dia: " + Dia.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"ValorMas5PorCiento: " + ValorMas5PorCiento.ToString() + "\r\n " + 
			"ValorMenos5PorCiento: " + ValorMenos5PorCiento.ToString() + "\r\n " + 
			"ValorMenos10PorCiento: " + ValorMenos10PorCiento.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"SectorId: " + SectorId.ToString() + "\r\n " ;
		}
        public CostoTecnica()
        {
            Id = -1;

        }

		public Segmentos GetRelatedSegmentoId()
		{
			Segmentos segmentos = SegmentosOperator.GetOneByIdentity(SegmentoId);
			return segmentos;
		}

		public TipoServicios GetRelatedTipoServicioId()
		{
			TipoServicios tipoServicios = TipoServiciosOperator.GetOneByIdentity(TipoServicioId);
			return tipoServicios;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "SegmentoId": return false;
				case "TipoServicioId": return false;
				case "Precio": return false;
				case "Anio": return false;
				case "Mes": return false;
				case "Dia": return false;
				case "ProveedorId": return false;
				case "ValorMas5PorCiento": return false;
				case "ValorMenos5PorCiento": return false;
				case "ValorMenos10PorCiento": return false;
				case "Costo": return true;
				case "SectorId": return true;
				default: return false;
			}
		}
    }
}

