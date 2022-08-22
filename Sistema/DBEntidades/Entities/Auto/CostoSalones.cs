using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class CostoSalones
    {
		public int Id { get; set; }
		public int LocacionId { get; set; }
		public int SectorId { get; set; }
		public int JornadaId { get; set; }
		public decimal PorcentajedelTotal { get; set; }
		public int Anio { get; set; }
		public int Mes { get; set; }
		public string Dia { get; set; }
		public decimal ValorMas5PorCiento { get; set; }
		public decimal ValorMenos5PorCiento { get; set; }
		public decimal ValorMenos10PorCiento { get; set; }
		public int? CantidadInvitados { get; set; }
		public decimal Costo { get; set; }
		public decimal Precio { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"LocacionId: " + LocacionId.ToString() + "\r\n " + 
			"SectorId: " + SectorId.ToString() + "\r\n " + 
			"JornadaId: " + JornadaId.ToString() + "\r\n " + 
			"PorcentajedelTotal: " + PorcentajedelTotal.ToString() + "\r\n " + 
			"Anio: " + Anio.ToString() + "\r\n " + 
			"Mes: " + Mes.ToString() + "\r\n " + 
			"Dia: " + Dia.ToString() + "\r\n " + 
			"ValorMas5PorCiento: " + ValorMas5PorCiento.ToString() + "\r\n " + 
			"ValorMenos5PorCiento: " + ValorMenos5PorCiento.ToString() + "\r\n " + 
			"ValorMenos10PorCiento: " + ValorMenos10PorCiento.ToString() + "\r\n " + 
			"CantidadInvitados: " + CantidadInvitados.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " ;
		}
        public CostoSalones()
        {
            Id = -1;

        }

		public Locaciones GetRelatedLocacionId()
		{
			Locaciones locaciones = LocacionesOperator.GetOneByIdentity(LocacionId);
			return locaciones;
		}

		public Sectores GetRelatedSectorId()
		{
			Sectores sectores = SectoresOperator.GetOneByIdentity(SectorId);
			return sectores;
		}

		public Jornadas GetRelatedJornadaId()
		{
			Jornadas jornadas = JornadasOperator.GetOneByIdentity(JornadaId);
			return jornadas;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "LocacionId": return false;
				case "SectorId": return false;
				case "JornadaId": return false;
				case "PorcentajedelTotal": return false;
				case "Anio": return false;
				case "Mes": return false;
				case "Dia": return false;
				case "ValorMas5PorCiento": return false;
				case "ValorMenos5PorCiento": return false;
				case "ValorMenos10PorCiento": return false;
				case "CantidadInvitados": return true;
				case "Costo": return true;
				case "Precio": return true;
				default: return false;
			}
		}
    }
}

