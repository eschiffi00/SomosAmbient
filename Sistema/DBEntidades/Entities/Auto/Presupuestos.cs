using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Presupuestos
    {
		public int Id { get; set; }
		public int EventoId { get; set; }
		public DateTime FechaPresupuesto { get; set; }
		public decimal PrecioTotal { get; set; }
		public decimal PrecioPorPersona { get; set; }
		public string Comentario { get; set; }
		public int SegmentoId { get; set; }
		public int CaracteristicaId { get; set; }
		public int? SectorId { get; set; }
		public int TipoEventoId { get; set; }
		public string TipoEventoOtro { get; set; }
		public int LocacionId { get; set; }
		public string LocacionOtra { get; set; }
		public string DireccionOtra { get; set; }
		public int? DuracionId { get; set; }
		public int? JornadaId { get; set; }
		public int? MomentoDiaID { get; set; }
		public string HorarioEvento { get; set; }
		public string HoraFinalizado { get; set; }
		public string HorarioArmado { get; set; }
		public int? CantidadInicialInvitados { get; set; }
		public DateTime? FechaEvento { get; set; }
		public int EstadoId { get; set; }
		public int? CantidadInvitadosMenores3 { get; set; }
		public int? CantidadInvitadosMenores3y8 { get; set; }
		public int? CantidadInvitadosAdolecentes { get; set; }
		public int? PresupuestoIdAnterior { get; set; }
		public decimal ImporteOrganizador { get; set; }
		public decimal PorcentajeOrganizador { get; set; }
		public decimal ValorOrganizador { get; set; }
		public DateTime? FechaCaducidad { get; set; }
		public int? CantidadAdultosFinal { get; set; }
		public int? CantidadAdolescentesFinal { get; set; }
		public int? CantidadMenores3Final { get; set; }
		public int? CantidadMenoresEntre3y8Final { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"EventoId: " + EventoId.ToString() + "\r\n " + 
			"FechaPresupuesto: " + FechaPresupuesto.ToString() + "\r\n " + 
			"PrecioTotal: " + PrecioTotal.ToString() + "\r\n " + 
			"PrecioPorPersona: " + PrecioPorPersona.ToString() + "\r\n " + 
			"Comentario: " + Comentario.ToString() + "\r\n " + 
			"SegmentoId: " + SegmentoId.ToString() + "\r\n " + 
			"CaracteristicaId: " + CaracteristicaId.ToString() + "\r\n " + 
			"SectorId: " + SectorId.ToString() + "\r\n " + 
			"TipoEventoId: " + TipoEventoId.ToString() + "\r\n " + 
			"TipoEventoOtro: " + TipoEventoOtro.ToString() + "\r\n " + 
			"LocacionId: " + LocacionId.ToString() + "\r\n " + 
			"LocacionOtra: " + LocacionOtra.ToString() + "\r\n " + 
			"DireccionOtra: " + DireccionOtra.ToString() + "\r\n " + 
			"DuracionId: " + DuracionId.ToString() + "\r\n " + 
			"JornadaId: " + JornadaId.ToString() + "\r\n " + 
			"MomentoDiaID: " + MomentoDiaID.ToString() + "\r\n " + 
			"HorarioEvento: " + HorarioEvento.ToString() + "\r\n " + 
			"HoraFinalizado: " + HoraFinalizado.ToString() + "\r\n " + 
			"HorarioArmado: " + HorarioArmado.ToString() + "\r\n " + 
			"CantidadInicialInvitados: " + CantidadInicialInvitados.ToString() + "\r\n " + 
			"FechaEvento: " + FechaEvento.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"CantidadInvitadosMenores3: " + CantidadInvitadosMenores3.ToString() + "\r\n " + 
			"CantidadInvitadosMenores3y8: " + CantidadInvitadosMenores3y8.ToString() + "\r\n " + 
			"CantidadInvitadosAdolecentes: " + CantidadInvitadosAdolecentes.ToString() + "\r\n " + 
			"PresupuestoIdAnterior: " + PresupuestoIdAnterior.ToString() + "\r\n " + 
			"ImporteOrganizador: " + ImporteOrganizador.ToString() + "\r\n " + 
			"PorcentajeOrganizador: " + PorcentajeOrganizador.ToString() + "\r\n " + 
			"ValorOrganizador: " + ValorOrganizador.ToString() + "\r\n " + 
			"FechaCaducidad: " + FechaCaducidad.ToString() + "\r\n " + 
			"CantidadAdultosFinal: " + CantidadAdultosFinal.ToString() + "\r\n " + 
			"CantidadAdolescentesFinal: " + CantidadAdolescentesFinal.ToString() + "\r\n " + 
			"CantidadMenores3Final: " + CantidadMenores3Final.ToString() + "\r\n " + 
			"CantidadMenoresEntre3y8Final: " + CantidadMenoresEntre3y8Final.ToString() + "\r\n " ;
		}
        public Presupuestos()
        {
            Id = -1;

        }

		public Eventos GetRelatedEventoId()
		{
			Eventos eventos = EventosOperator.GetOneByIdentity(EventoId);
			return eventos;
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
			TipoEventos tipoEventos = TipoEventosOperator.GetOneByIdentity(TipoEventoId);
			return tipoEventos;
		}

		public Locaciones GetRelatedLocacionId()
		{
			Locaciones locaciones = LocacionesOperator.GetOneByIdentity(LocacionId);
			return locaciones;
		}

		public DuracionEvento GetRelatedDuracionId()
		{
			if (DuracionId != null)
			{
				DuracionEvento duracionEvento = DuracionEventoOperator.GetOneByIdentity(DuracionId ?? 0);
				return duracionEvento;
			}
			return null;
		}

		public Jornadas GetRelatedJornadaId()
		{
			if (JornadaId != null)
			{
				Jornadas jornadas = JornadasOperator.GetOneByIdentity(JornadaId ?? 0);
				return jornadas;
			}
			return null;
		}

		public MomentosDias GetRelatedMomentoDiaID()
		{
			if (MomentoDiaID != null)
			{
				MomentosDias momentosDias = MomentosDiasOperator.GetOneByIdentity(MomentoDiaID ?? 0);
				return momentosDias;
			}
			return null;
		}

		public Estados GetRelatedEstadoId()
		{
			Estados estados = EstadosOperator.GetOneByIdentity(EstadoId);
			return estados;
		}



		public List<OrganizacionPresupuestoDetalle> GetRelatedOrganizacionPresupuestoDetalles()
		{
			return OrganizacionPresupuestoDetalleOperator.GetAll().Where(x => x.PresupuestoId == Id).ToList();
		}
		public List<PagosClientes> GetRelatedPagosClienteses()
		{
			return PagosClientesOperator.GetAll().Where(x => x.PresupuestoId == Id).ToList();
		}
		public List<EmpleadosPresupuestosAprobados> GetRelatedEmpleadosPresupuestosAprobadoses()
		{
			return EmpleadosPresupuestosAprobadosOperator.GetAll().Where(x => x.PresupuestoId == Id).ToList();
		}
		public List<OrganizacionPresupuestoTimming> GetRelatedOrganizacionPresupuestoTimminges()
		{
			return OrganizacionPresupuestoTimmingOperator.GetAll().Where(x => x.PresupuestoId == Id).ToList();
		}
		public List<ReciboEventoPresupuesto> GetRelatedReciboEventoPresupuestos()
		{
			return ReciboEventoPresupuestoOperator.GetAll().Where(x => x.PresupuestoId == Id).ToList();
		}
		public List<OrganizacionPresupuestoProveedoresExternos> GetRelatedOrganizacionPresupuestoProveedoresExternoses()
		{
			return OrganizacionPresupuestoProveedoresExternosOperator.GetAll().Where(x => x.PresupuestoId == Id).ToList();
		}
		public List<OrganizacionPresupuestosArchivos> GetRelatedOrganizacionPresupuestosArchivoses()
		{
			return OrganizacionPresupuestosArchivosOperator.GetAll().Where(x => x.PresupuestoId == Id).ToList();
		}
		public List<ComprobantesProveedores_Detalles> GetRelatedComprobantesProveedores_Detalleses()
		{
			return ComprobantesProveedores_DetallesOperator.GetAll().Where(x => x.PresupuestoId == Id).ToList();
		}
		public List<PresupuestoDetalle> GetRelatedPresupuestoDetalles()
		{
			return PresupuestoDetalleOperator.GetAll().Where(x => x.PresupuestoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "EventoId": return false;
				case "FechaPresupuesto": return false;
				case "PrecioTotal": return true;
				case "PrecioPorPersona": return true;
				case "Comentario": return true;
				case "SegmentoId": return false;
				case "CaracteristicaId": return false;
				case "SectorId": return true;
				case "TipoEventoId": return false;
				case "TipoEventoOtro": return true;
				case "LocacionId": return false;
				case "LocacionOtra": return true;
				case "DireccionOtra": return true;
				case "DuracionId": return true;
				case "JornadaId": return true;
				case "MomentoDiaID": return true;
				case "HorarioEvento": return true;
				case "HoraFinalizado": return true;
				case "HorarioArmado": return true;
				case "CantidadInicialInvitados": return true;
				case "FechaEvento": return true;
				case "EstadoId": return false;
				case "CantidadInvitadosMenores3": return true;
				case "CantidadInvitadosMenores3y8": return true;
				case "CantidadInvitadosAdolecentes": return true;
				case "PresupuestoIdAnterior": return true;
				case "ImporteOrganizador": return true;
				case "PorcentajeOrganizador": return true;
				case "ValorOrganizador": return true;
				case "FechaCaducidad": return true;
				case "CantidadAdultosFinal": return true;
				case "CantidadAdolescentesFinal": return true;
				case "CantidadMenores3Final": return true;
				case "CantidadMenoresEntre3y8Final": return true;
				default: return false;
			}
		}
    }
}

