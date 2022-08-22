using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Empleados
    {
		public int Id { get; set; }
		public string ApellidoNombre { get; set; }
		public string Nombre { get; set; }
		public int NroLegajo { get; set; }
		public string Mail { get; set; }
		public string MailLaboral { get; set; }
		public string TipoDocumento { get; set; }
		public int? NroDocumento { get; set; }
		public string Cuil { get; set; }
		public DateTime? FechaNacimiento { get; set; }
		public string Direccion { get; set; }
		public string DireccionLegal { get; set; }
		public int LocalidadId { get; set; }
		public int? CiudadLegalId { get; set; }
		public string CP { get; set; }
		public string CPLegal { get; set; }
		public string TelefonoFijo { get; set; }
		public string TelefonoMovil { get; set; }
		public DateTime? FechaIngreso { get; set; }
		public string TelefonoFijoLaboral { get; set; }
		public string CelularFijoLaboral { get; set; }
		public int UsaPc { get; set; }
		public string NroPc { get; set; }
		public int? EstadoId { get; set; }
		public int SectorEmpresaId { get; set; }
		public int TipoEmpleadoId { get; set; }
		public string HorarioDesde { get; set; }
		public string HorarioHasta { get; set; }
		public decimal Sueldo { get; set; }
		public decimal Premio { get; set; }
		public decimal SAC { get; set; }
		public string Observaciones { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ApellidoNombre: " + ApellidoNombre.ToString() + "\r\n " + 
			"Nombre: " + Nombre.ToString() + "\r\n " + 
			"NroLegajo: " + NroLegajo.ToString() + "\r\n " + 
			"Mail: " + Mail.ToString() + "\r\n " + 
			"MailLaboral: " + MailLaboral.ToString() + "\r\n " + 
			"TipoDocumento: " + TipoDocumento.ToString() + "\r\n " + 
			"NroDocumento: " + NroDocumento.ToString() + "\r\n " + 
			"Cuil: " + Cuil.ToString() + "\r\n " + 
			"FechaNacimiento: " + FechaNacimiento.ToString() + "\r\n " + 
			"Direccion: " + Direccion.ToString() + "\r\n " + 
			"DireccionLegal: " + DireccionLegal.ToString() + "\r\n " + 
			"LocalidadId: " + LocalidadId.ToString() + "\r\n " + 
			"CiudadLegalId: " + CiudadLegalId.ToString() + "\r\n " + 
			"CP: " + CP.ToString() + "\r\n " + 
			"CPLegal: " + CPLegal.ToString() + "\r\n " + 
			"TelefonoFijo: " + TelefonoFijo.ToString() + "\r\n " + 
			"TelefonoMovil: " + TelefonoMovil.ToString() + "\r\n " + 
			"FechaIngreso: " + FechaIngreso.ToString() + "\r\n " + 
			"TelefonoFijoLaboral: " + TelefonoFijoLaboral.ToString() + "\r\n " + 
			"CelularFijoLaboral: " + CelularFijoLaboral.ToString() + "\r\n " + 
			"UsaPc: " + UsaPc.ToString() + "\r\n " + 
			"NroPc: " + NroPc.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"SectorEmpresaId: " + SectorEmpresaId.ToString() + "\r\n " + 
			"TipoEmpleadoId: " + TipoEmpleadoId.ToString() + "\r\n " + 
			"HorarioDesde: " + HorarioDesde.ToString() + "\r\n " + 
			"HorarioHasta: " + HorarioHasta.ToString() + "\r\n " + 
			"Sueldo: " + Sueldo.ToString() + "\r\n " + 
			"Premio: " + Premio.ToString() + "\r\n " + 
			"SAC: " + SAC.ToString() + "\r\n " + 
			"Observaciones: " + Observaciones.ToString() + "\r\n " ;
		}
        public Empleados()
        {
            Id = -1;

        }

		public Ciudades GetRelatedLocalidadId()
		{
			Ciudades ciudades = CiudadesOperator.GetOneByIdentity(LocalidadId);
			return ciudades;
		}

		public Ciudades GetRelatedCiudadLegalId()
		{
			if (CiudadLegalId != null)
			{
				Ciudades ciudades = CiudadesOperator.GetOneByIdentity(CiudadLegalId ?? 0);
				return ciudades;
			}
			return null;
		}

		public Estados GetRelatedEstadoId()
		{
			if (EstadoId != null)
			{
				Estados estados = EstadosOperator.GetOneByIdentity(EstadoId ?? 0);
				return estados;
			}
			return null;
		}

		public SectoresEmpresa GetRelatedSectorEmpresaId()
		{
			SectoresEmpresa sectoresEmpresa = SectoresEmpresaOperator.GetOneByIdentity(SectorEmpresaId);
			return sectoresEmpresa;
		}

		public TipoEmpleados GetRelatedTipoEmpleadoId()
		{
			TipoEmpleados tipoEmpleados = TipoEmpleadosOperator.GetOneByIdentity(TipoEmpleadoId);
			return tipoEmpleados;
		}



		public List<ObjetivosEmpleados> GetRelatedObjetivosEmpleadoses()
		{
			return ObjetivosEmpleadosOperator.GetAll().Where(x => x.EmpleadoId == Id).ToList();
		}
		public List<Eventos> GetRelatedEventoses()
		{
			return EventosOperator.GetAll().Where(x => x.EmpleadoId == Id).ToList();
		}
		public List<EmpleadoDepartamentos> GetRelatedEmpleadoDepartamentoses()
		{
			return EmpleadoDepartamentosOperator.GetAll().Where(x => x.EmpleadoId == Id).ToList();
		}
		public List<OrganizacionPresupuestosArchivos> GetRelatedOrganizacionPresupuestosArchivoses()
		{
			return OrganizacionPresupuestosArchivosOperator.GetAll().Where(x => x.EmpleadoId == Id).ToList();
		}
		public List<DegustacionDetalle> GetRelatedDegustacionDetalles()
		{
			return DegustacionDetalleOperator.GetAll().Where(x => x.EmpleadoId == Id).ToList();
		}
		public List<LiquidacionHorasPersonal_Detalle> GetRelatedLiquidacionHorasPersonal_Detalles()
		{
			return LiquidacionHorasPersonal_DetalleOperator.GetAll().Where(x => x.EmpleadoId == Id).ToList();
		}
		public List<Usuarios> GetRelatedUsuarioses()
		{
			return UsuariosOperator.GetAll().Where(x => x.EmpleadoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ApellidoNombre": return true;
				case "Nombre": return true;
				case "NroLegajo": return false;
				case "Mail": return true;
				case "MailLaboral": return true;
				case "TipoDocumento": return true;
				case "NroDocumento": return true;
				case "Cuil": return true;
				case "FechaNacimiento": return true;
				case "Direccion": return true;
				case "DireccionLegal": return true;
				case "LocalidadId": return false;
				case "CiudadLegalId": return true;
				case "CP": return true;
				case "CPLegal": return true;
				case "TelefonoFijo": return true;
				case "TelefonoMovil": return true;
				case "FechaIngreso": return true;
				case "TelefonoFijoLaboral": return true;
				case "CelularFijoLaboral": return true;
				case "UsaPc": return false;
				case "NroPc": return true;
				case "EstadoId": return true;
				case "SectorEmpresaId": return false;
				case "TipoEmpleadoId": return false;
				case "HorarioDesde": return true;
				case "HorarioHasta": return true;
				case "Sueldo": return true;
				case "Premio": return true;
				case "SAC": return true;
				case "Observaciones": return true;
				default: return false;
			}
		}
    }
}

