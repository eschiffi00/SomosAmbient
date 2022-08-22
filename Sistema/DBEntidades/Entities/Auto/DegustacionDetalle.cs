using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class DegustacionDetalle
    {
		public int Id { get; set; }
		public int DegustacionId { get; set; }
		public int SegmentoId { get; set; }
		public int CaracteristicaId { get; set; }
		public int? TipoEventoId { get; set; }
		public int CantidadInvitados { get; set; }
		public DateTime FechaEvento { get; set; }
		public string Empresa { get; set; }
		public string Comensal { get; set; }
		public int? NroMesa { get; set; }
		public int? NroComensal { get; set; }
		public string EstadoEvento { get; set; }
		public int? LocacionId { get; set; }
		public string Comentarios { get; set; }
		public string Telefono { get; set; }
		public string Mail { get; set; }
		public int EmpleadoId { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"DegustacionId: " + DegustacionId.ToString() + "\r\n " + 
			"SegmentoId: " + SegmentoId.ToString() + "\r\n " + 
			"CaracteristicaId: " + CaracteristicaId.ToString() + "\r\n " + 
			"TipoEventoId: " + TipoEventoId.ToString() + "\r\n " + 
			"CantidadInvitados: " + CantidadInvitados.ToString() + "\r\n " + 
			"FechaEvento: " + FechaEvento.ToString() + "\r\n " + 
			"Empresa: " + Empresa.ToString() + "\r\n " + 
			"Comensal: " + Comensal.ToString() + "\r\n " + 
			"NroMesa: " + NroMesa.ToString() + "\r\n " + 
			"NroComensal: " + NroComensal.ToString() + "\r\n " + 
			"EstadoEvento: " + EstadoEvento.ToString() + "\r\n " + 
			"LocacionId: " + LocacionId.ToString() + "\r\n " + 
			"Comentarios: " + Comentarios.ToString() + "\r\n " + 
			"Telefono: " + Telefono.ToString() + "\r\n " + 
			"Mail: " + Mail.ToString() + "\r\n " + 
			"EmpleadoId: " + EmpleadoId.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public DegustacionDetalle()
        {
            Id = -1;

        }

		public Degustacion GetRelatedDegustacionId()
		{
			Degustacion degustacion = DegustacionOperator.GetOneByIdentity(DegustacionId);
			return degustacion;
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

		public TipoEventos GetRelatedTipoEventoId()
		{
			if (TipoEventoId != null)
			{
				TipoEventos tipoEventos = TipoEventosOperator.GetOneByIdentity(TipoEventoId ?? 0);
				return tipoEventos;
			}
			return null;
		}

		public Locaciones GetRelatedLocacionId()
		{
			if (LocacionId != null)
			{
				Locaciones locaciones = LocacionesOperator.GetOneByIdentity(LocacionId ?? 0);
				return locaciones;
			}
			return null;
		}

		public Empleados GetRelatedEmpleadoId()
		{
			Empleados empleados = EmpleadosOperator.GetOneByIdentity(EmpleadoId);
			return empleados;
		}

		public Estados GetRelatedEstadoId()
		{
			Estados estados = EstadosOperator.GetOneByIdentity(EstadoId);
			return estados;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "DegustacionId": return false;
				case "SegmentoId": return false;
				case "CaracteristicaId": return false;
				case "TipoEventoId": return true;
				case "CantidadInvitados": return false;
				case "FechaEvento": return false;
				case "Empresa": return true;
				case "Comensal": return true;
				case "NroMesa": return true;
				case "NroComensal": return true;
				case "EstadoEvento": return true;
				case "LocacionId": return true;
				case "Comentarios": return true;
				case "Telefono": return true;
				case "Mail": return true;
				case "EmpleadoId": return false;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

