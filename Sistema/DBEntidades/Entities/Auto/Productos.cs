using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Productos
    {
		public int Id { get; set; }
		public string Codigo { get; set; }
		public string Descripcion { get; set; }
		public decimal Precio { get; set; }
		public decimal Margen { get; set; }
		public decimal Costo { get; set; }
		public int UnidadNegocioId { get; set; }
		public int Estado { get; set; }
		public DateTime? FechaVendimiento { get; set; }
		public int? PerfilId { get; set; }
		public int? TipoCateringId { get; set; }
		public int? AdicionalId { get; set; }
		public int? TipoBarraId { get; set; }
		public int? TipoServicioId { get; set; }
		public int? CantidadInvitados { get; set; }
		public int? LocacionId { get; set; }
		public int? ProveedorId { get; set; }
		public int? SegmentoId { get; set; }
		public int? JornadaId { get; set; }
		public int? SectorId { get; set; }
		public int? CategoriaId { get; set; }
		public int? Anio { get; set; }
		public int? Mes { get; set; }
		public string Dia { get; set; }
		public int? CaracteristicaId { get; set; }
		public int? OrganizacionItemId { get; set; }
		public int? Semestre { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Codigo: " + Codigo.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Precio: " + Precio.ToString() + "\r\n " + 
			"Margen: " + Margen.ToString() + "\r\n " + 
			"Costo: " + Costo.ToString() + "\r\n " + 
			"UnidadNegocioId: " + UnidadNegocioId.ToString() + "\r\n " + 
			"Estado: " + Estado.ToString() + "\r\n " + 
			"FechaVendimiento: " + FechaVendimiento.ToString() + "\r\n " + 
			"PerfilId: " + PerfilId.ToString() + "\r\n " + 
			"TipoCateringId: " + TipoCateringId.ToString() + "\r\n " + 
			"AdicionalId: " + AdicionalId.ToString() + "\r\n " + 
			"TipoBarraId: " + TipoBarraId.ToString() + "\r\n " + 
			"TipoServicioId: " + TipoServicioId.ToString() + "\r\n " + 
			"CantidadInvitados: " + CantidadInvitados.ToString() + "\r\n " + 
			"LocacionId: " + LocacionId.ToString() + "\r\n " + 
			"ProveedorId: " + ProveedorId.ToString() + "\r\n " + 
			"SegmentoId: " + SegmentoId.ToString() + "\r\n " + 
			"JornadaId: " + JornadaId.ToString() + "\r\n " + 
			"SectorId: " + SectorId.ToString() + "\r\n " + 
			"CategoriaId: " + CategoriaId.ToString() + "\r\n " + 
			"Anio: " + Anio.ToString() + "\r\n " + 
			"Mes: " + Mes.ToString() + "\r\n " + 
			"Dia: " + Dia.ToString() + "\r\n " + 
			"CaracteristicaId: " + CaracteristicaId.ToString() + "\r\n " + 
			"OrganizacionItemId: " + OrganizacionItemId.ToString() + "\r\n " + 
			"Semestre: " + Semestre.ToString() + "\r\n " ;
		}
        public Productos()
        {
            Id = -1;

        }

		public UnidadesNegocios GetRelatedUnidadNegocioId()
		{
			UnidadesNegocios unidadesNegocios = UnidadesNegociosOperator.GetOneByIdentity(UnidadNegocioId);
			return unidadesNegocios;
		}

		public Perfiles GetRelatedPerfilId()
		{
			if (PerfilId != null)
			{
				Perfiles perfiles = PerfilesOperator.GetOneByIdentity(PerfilId ?? 0);
				return perfiles;
			}
			return null;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Codigo": return false;
				case "Descripcion": return false;
				case "Precio": return false;
				case "Margen": return true;
				case "Costo": return false;
				case "UnidadNegocioId": return false;
				case "Estado": return false;
				case "FechaVendimiento": return true;
				case "PerfilId": return true;
				case "TipoCateringId": return true;
				case "AdicionalId": return true;
				case "TipoBarraId": return true;
				case "TipoServicioId": return true;
				case "CantidadInvitados": return true;
				case "LocacionId": return true;
				case "ProveedorId": return true;
				case "SegmentoId": return true;
				case "JornadaId": return true;
				case "SectorId": return true;
				case "CategoriaId": return true;
				case "Anio": return true;
				case "Mes": return true;
				case "Dia": return true;
				case "CaracteristicaId": return true;
				case "OrganizacionItemId": return true;
				case "Semestre": return true;
				default: return false;
			}
		}
    }
}

