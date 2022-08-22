using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Adicionales
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int? RubroId { get; set; }
		public decimal Precio { get; set; }
		public int EstadoId { get; set; }
		public string RequiereCantidad { get; set; }
		public string RequiereCantidadRango { get; set; }
		public int? ProveedorId { get; set; }
		public decimal Costo { get; set; }
		public int? LocacionId { get; set; }
		public string SoloMayores { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"RubroId: " + RubroId.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"RequiereCantidad: " + RequiereCantidad.ToString() + "\r\n " + 
			"RequiereCantidadRango: " + RequiereCantidadRango.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"LocacionId: " + LocacionId.ToString() + "\r\n " + 
			"SoloMayores: " + SoloMayores.ToString() + "\r\n " ;
		}
        public Adicionales()
        {
            Id = -1;

        }

		public UnidadesNegocios GetRelatedRubroId()
		{
			if (RubroId != null)
			{
				UnidadesNegocios unidadesNegocios = UnidadesNegociosOperator.GetOneByIdentity(RubroId ?? 0);
				return unidadesNegocios;
			}
			return null;
		}

		public Estados GetRelatedEstadoId()
		{
			Estados estados = EstadosOperator.GetOneByIdentity(EstadoId);
			return estados;
		}



		public List<TipoServicioAdicional> GetRelatedTipoServicioAdicionales()
		{
			return TipoServicioAdicionalOperator.GetAll().Where(x => x.AdicionalId == Id).ToList();
		}
		public List<CostoAdicionales> GetRelatedCostoAdicionaleses()
		{
			return CostoAdicionalesOperator.GetAll().Where(x => x.AdicionalId == Id).ToList();
		}
		public List<TipoCateringAdicional> GetRelatedTipoCateringAdicionales()
		{
			return TipoCateringAdicionalOperator.GetAll().Where(x => x.AdicionalId == Id).ToList();
		}
		public List<AdicionalesItems> GetRelatedAdicionalesItemses()
		{
			return AdicionalesItemsOperator.GetAll().Where(x => x.AdicionalId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "RubroId": return true;
				case "Precio": return true;
				case "EstadoId": return false;
				case "RequiereCantidad": return false;
				case "RequiereCantidadRango": return false;
				case "ProveedorId": return true;
				case "Costo": return true;
				case "LocacionId": return true;
				case "SoloMayores": return false;
				default: return false;
			}
		}
    }
}

