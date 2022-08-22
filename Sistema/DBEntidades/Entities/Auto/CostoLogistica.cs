using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class CostoLogistica
    {
		public int Id { get; set; }
		public int ConceptoId { get; set; }
		public int Localidad { get; set; }
		public int CantidadInvitados { get; set; }
		public int? TipoEventoId { get; set; }
		public decimal Costo { get; set; }
		public decimal Margen { get; set; }
		public decimal Valor { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ConceptoId: " + ConceptoId.ToString() + "\r\n " + 
			"Localidad: " + Localidad.ToString() + "\r\n " + 
			"CantidadInvitados: " + CantidadInvitados.ToString() + "\r\n " + 
			"TipoEventoId: " + TipoEventoId.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"Margen: " + Margen.ToString() + "\r\n " + 
			"Valor: " + Valor.ToString() + "\r\n " ;
		}
        public CostoLogistica()
        {
            Id = -1;

        }

		public TipoLogistica GetRelatedConceptoId()
		{
			TipoLogistica tipoLogistica = TipoLogisticaOperator.GetOneByIdentity(ConceptoId);
			return tipoLogistica;
		}

		public Localidades GetRelatedLocalidad()
		{
			Localidades localidades = LocalidadesOperator.GetOneByIdentity(Localidad);
			return localidades;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ConceptoId": return false;
				case "Localidad": return false;
				case "CantidadInvitados": return false;
				case "TipoEventoId": return true;
				case "Costo": return false;
				case "Margen": return false;
				case "Valor": return false;
				default: return false;
			}
		}
    }
}

