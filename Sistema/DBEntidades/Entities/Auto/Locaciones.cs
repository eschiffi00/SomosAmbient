using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Locaciones
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public string TipoLocacion { get; set; }
		public int? CapacidadFormal { get; set; }
		public int? CapacidadInformal { get; set; }
		public int? CapacidadAuditorio { get; set; }
		public string Verde { get; set; }
		public string AireLibre { get; set; }
		public string Estacionamiento { get; set; }
		public string Comentarios { get; set; }
		public decimal UsoCocina { get; set; }
		public string TipoCannonCatering { get; set; }
		public decimal Cannon { get; set; }
		public string TipoCannonBarra { get; set; }
		public decimal CannonBarra { get; set; }
		public string TipoCannonAmbientacion { get; set; }
		public decimal CannonAmbientacion { get; set; }
		public string Telefono { get; set; }
		public string Direccion { get; set; }
		public string Mail { get; set; }
		public string web { get; set; }
		public int LocalidadId { get; set; }
		public string TieneLogistica { get; set; }
		public string Comisiona { get; set; }
		public int RequiereMesasSillas { get; set; }
		public decimal CostoSillas { get; set; }
		public decimal CostoMesas { get; set; }
		public decimal PrecioSillas { get; set; }
		public decimal PrecioMesas { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"TipoLocacion: " + TipoLocacion.ToString() + "\r\n " + 
			"CapacidadFormal: " + CapacidadFormal.ToString() + "\r\n " + 
			"CapacidadInformal: " + CapacidadInformal.ToString() + "\r\n " + 
			"CapacidadAuditorio: " + CapacidadAuditorio.ToString() + "\r\n " + 
			"Verde: " + Verde.ToString() + "\r\n " + 
			"AireLibre: " + AireLibre.ToString() + "\r\n " + 
			"Estacionamiento: " + Estacionamiento.ToString() + "\r\n " + 
			"Comentarios: " + Comentarios.ToString() + "\r\n " + 
			"UsoCocina: " + UsoCocina.ToString() + "\r\n " + 
			"TipoCannonCatering: " + TipoCannonCatering.ToString() + "\r\n " + 
			"Cannon: " + Cannon.ToString() + "\r\n " + 
			"TipoCannonBarra: " + TipoCannonBarra.ToString() + "\r\n " + 
			"CannonBarra: " + CannonBarra.ToString() + "\r\n " + 
			"TipoCannonAmbientacion: " + TipoCannonAmbientacion.ToString() + "\r\n " + 
			"CannonAmbientacion: " + CannonAmbientacion.ToString() + "\r\n " + 
			"Telefono: " + Telefono.ToString() + "\r\n " + 
			"Direccion: " + Direccion.ToString() + "\r\n " + 
			"Mail: " + Mail.ToString() + "\r\n " + 
			"web: " + web.ToString() + "\r\n " + 
			"LocalidadId: " + LocalidadId.ToString() + "\r\n " + 
			"TieneLogistica: " + TieneLogistica.ToString() + "\r\n " + 
			"Comisiona: " + Comisiona.ToString() + "\r\n " + 
			"RequiereMesasSillas: " + RequiereMesasSillas.ToString() + "\r\n " + 
			"CostoSillas: " + CostoSillas.ToString() + "\r\n " + 
			"CostoMesas: " + CostoMesas.ToString() + "\r\n " + 
			"PrecioSillas: " + PrecioSillas.ToString() + "\r\n " + 
			"PrecioMesas: " + PrecioMesas.ToString() + "\r\n " ;
		}
        public Locaciones()
        {
			Id = -1;

        }

		public Localidades GetRelatedLocalidadId()
		{
			Localidades localidades = LocalidadesOperator.GetOneByIdentity(LocalidadId);
			return localidades;
		}



		public List<Sectores> GetRelatedSectoreses()
		{
			return SectoresOperator.GetAll().Where(x => x.LocacionId == Id).ToList();
		}
		public List<LocacionesValorAnio> GetRelatedLocacionesValorAnios()
		{
			return LocacionesValorAnioOperator.GetAll().Where(x => x.LocacionId == Id).ToList();
		}
		public List<CostoSalones> GetRelatedCostoSaloneses()
		{
			return CostoSalonesOperator.GetAll().Where(x => x.LocacionId == Id).ToList();
		}
		public List<TecnicaSalon> GetRelatedTecnicaSalones()
		{
			return TecnicaSalonOperator.GetAll().Where(x => x.LocacionId == Id).ToList();
		}
		public List<AmbientacionSalon> GetRelatedAmbientacionSalones()
		{
			return AmbientacionSalonOperator.GetAll().Where(x => x.LocacionId == Id).ToList();
		}
		public List<Intermediarios> GetRelatedIntermediarioses()
		{
			return IntermediariosOperator.GetAll().Where(x => x.LocacionId == Id).ToList();
		}
		public List<Presupuestos> GetRelatedPresupuestoses()
		{
			return PresupuestosOperator.GetAll().Where(x => x.LocacionId == Id).ToList();
		}
		public List<DegustacionDetalle> GetRelatedDegustacionDetalles()
		{
			return DegustacionDetalleOperator.GetAll().Where(x => x.LocacionId == Id).ToList();
		}
		public List<Categorias> GetRelatedCategoriases()
		{
			return CategoriasOperator.GetAll().Where(x => x.LocacionId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "TipoLocacion": return false;
				case "CapacidadFormal": return true;
				case "CapacidadInformal": return true;
				case "CapacidadAuditorio": return true;
				case "Verde": return true;
				case "AireLibre": return true;
				case "Estacionamiento": return true;
				case "Comentarios": return true;
				case "UsoCocina": return true;
				case "TipoCannonCatering": return false;
				case "Cannon": return true;
				case "TipoCannonBarra": return true;
				case "CannonBarra": return true;
				case "TipoCannonAmbientacion": return true;
				case "CannonAmbientacion": return true;
				case "Telefono": return true;
				case "Direccion": return true;
				case "Mail": return true;
				case "web": return true;
				case "LocalidadId": return false;
				case "TieneLogistica": return false;
				case "Comisiona": return false;
				case "RequiereMesasSillas": return false;
				case "CostoSillas": return true;
				case "CostoMesas": return true;
				case "PrecioSillas": return true;
				case "PrecioMesas": return true;
				default: return false;
			}
		}
    }
}

