using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class OrganizacionPresupuestoDetalle
    {
		public int Id { get; set; }
		public int PresupuestoId { get; set; }
		public string MotivoFestejo { get; set; }
		public string Mail { get; set; }
		public string Tel { get; set; }
		public string LocacionOtra { get; set; }
		public string EnvioMailPresentacion { get; set; }
		public DateTime? FechaMailPresentacion { get; set; }
		public string RealizoReunionConCliente { get; set; }
		public string Direccion { get; set; }
		public string Bocados { get; set; }
		public string Islas { get; set; }
		public string Entrada { get; set; }
		public string PrincipalAdultos { get; set; }
		public string PrincipalAdolescentes { get; set; }
		public string PostreAdultosAdolescentes { get; set; }
		public string PrincipalChicos { get; set; }
		public string PostreChicos { get; set; }
		public string MesaDulce { get; set; }
		public string FinFiesta { get; set; }
		public string MesaPrincipal { get; set; }
		public string Manteleria { get; set; }
		public string Servilletas { get; set; }
		public string Sillas { get; set; }
		public string InvitadosDespues00 { get; set; }
		public string CumpleaniosEnEvento { get; set; }
		public string TortaAlegorica { get; set; }
		public string LleganAlSalon { get; set; }
		public string PlatosEspeciales { get; set; }
		public string ServiciodeVinoChampagne { get; set; }
		public string ObservacionBarras { get; set; }
		public string ObservacionCatering { get; set; }
		public string ObservacionTecnica { get; set; }
		public string ObservacionAmbientacion { get; set; }
		public string ObservacionParticulares { get; set; }
		public string ObservacionesAdicionales { get; set; }
		public int BocadosEstado { get; set; }
		public int IslasEstado { get; set; }
		public int EntradaEstado { get; set; }
		public int PrincipalAdultosEstado { get; set; }
		public int PrincipalAdolescentesEstado { get; set; }
		public int PostreAdultosAdolescentesEstado { get; set; }
		public int PrincipalChicosEstado { get; set; }
		public int PostreChicosEstado { get; set; }
		public int MesaDulceEstado { get; set; }
		public int FinFiestaEstado { get; set; }
		public int MesaPrincipalEstado { get; set; }
		public int ManteleriaEstado { get; set; }
		public int ServilletasEstado { get; set; }
		public int SillasEstado { get; set; }
		public int InvitadosDespues00Estado { get; set; }
		public int CumpleaniosEnEventoEstado { get; set; }
		public int TortaAlegoricaEstado { get; set; }
		public int LleganAlSalonEstado { get; set; }
		public int PlatosEspecialesEstado { get; set; }
		public int ServiciodeVinoChampagneEstado { get; set; }
		public string Acreditaciones { get; set; }
		public string ListaInvitados { get; set; }
		public string ListaCocheras { get; set; }
		public int AcreditacionesEstado { get; set; }
		public int ListaInvitadosEstado { get; set; }
		public int ListaCocherasEstado { get; set; }
		public string Layout { get; set; }
		public string AlfombraRoja { get; set; }
		public string Anexo7 { get; set; }
		public int LayoutEstado { get; set; }
		public int AlfombraRojaEstado { get; set; }
		public int Anexo7Estado { get; set; }
		public string Ramo { get; set; }
		public string Escenario { get; set; }
		public string IngresoProveedoresLugar { get; set; }
		public string ContactoResponsableLugar { get; set; }
		public string TelefonoResponsableLugar { get; set; }
		public string FechaArmadoLogistica { get; set; }
		public string FechaArmadoSalon { get; set; }
		public string FechaDesarmadoSalon { get; set; }
		public string HoraArmadoLogistica { get; set; }
		public string HoraDesarmadoSalon { get; set; }
		public string HoraArmadoSalon { get; set; }
		public string CantPersonasAfectadasArmado { get; set; }
		public int SePidioHielo { get; set; }
		public int SePidioLogistica { get; set; }
		public int SePidioManteleria { get; set; }
		public int SePidioMoviliario { get; set; }
		public string ObservacionesHielo { get; set; }
		public string ObservacionesMoviliario { get; set; }
		public string ObservacionesLogistica { get; set; }
		public string ObservacionesManteleria { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"PresupuestoId: " + PresupuestoId.ToString() + "\r\n " + 
			"MotivoFestejo: " + MotivoFestejo.ToString() + "\r\n " + 
			"Mail: " + Mail.ToString() + "\r\n " + 
			"Tel: " + Tel.ToString() + "\r\n " + 
			"LocacionOtra: " + LocacionOtra.ToString() + "\r\n " + 
			"EnvioMailPresentacion: " + EnvioMailPresentacion.ToString() + "\r\n " + 
			"FechaMailPresentacion: " + FechaMailPresentacion.ToString() + "\r\n " + 
			"RealizoReunionConCliente: " + RealizoReunionConCliente.ToString() + "\r\n " + 
			"Direccion: " + Direccion.ToString() + "\r\n " + 
			"Bocados: " + Bocados.ToString() + "\r\n " + 
			"Islas: " + Islas.ToString() + "\r\n " + 
			"Entrada: " + Entrada.ToString() + "\r\n " + 
			"PrincipalAdultos: " + PrincipalAdultos.ToString() + "\r\n " + 
			"PrincipalAdolescentes: " + PrincipalAdolescentes.ToString() + "\r\n " + 
			"PostreAdultosAdolescentes: " + PostreAdultosAdolescentes.ToString() + "\r\n " + 
			"PrincipalChicos: " + PrincipalChicos.ToString() + "\r\n " + 
			"PostreChicos: " + PostreChicos.ToString() + "\r\n " + 
			"MesaDulce: " + MesaDulce.ToString() + "\r\n " + 
			"FinFiesta: " + FinFiesta.ToString() + "\r\n " + 
			"MesaPrincipal: " + MesaPrincipal.ToString() + "\r\n " + 
			"Manteleria: " + Manteleria.ToString() + "\r\n " + 
			"Servilletas: " + Servilletas.ToString() + "\r\n " + 
			"Sillas: " + Sillas.ToString() + "\r\n " + 
			"InvitadosDespues00: " + InvitadosDespues00.ToString() + "\r\n " + 
			"CumpleaniosEnEvento: " + CumpleaniosEnEvento.ToString() + "\r\n " + 
			"TortaAlegorica: " + TortaAlegorica.ToString() + "\r\n " + 
			"LleganAlSalon: " + LleganAlSalon.ToString() + "\r\n " + 
			"PlatosEspeciales: " + PlatosEspeciales.ToString() + "\r\n " + 
			"ServiciodeVinoChampagne: " + ServiciodeVinoChampagne.ToString() + "\r\n " + 
			"ObservacionBarras: " + ObservacionBarras.ToString() + "\r\n " + 
			"ObservacionCatering: " + ObservacionCatering.ToString() + "\r\n " + 
			"ObservacionTecnica: " + ObservacionTecnica.ToString() + "\r\n " + 
			"ObservacionAmbientacion: " + ObservacionAmbientacion.ToString() + "\r\n " + 
			"ObservacionParticulares: " + ObservacionParticulares.ToString() + "\r\n " + 
			"ObservacionesAdicionales: " + ObservacionesAdicionales.ToString() + "\r\n " + 
			"BocadosEstado: " + BocadosEstado.ToString() + "\r\n " + 
			"IslasEstado: " + IslasEstado.ToString() + "\r\n " + 
			"EntradaEstado: " + EntradaEstado.ToString() + "\r\n " + 
			"PrincipalAdultosEstado: " + PrincipalAdultosEstado.ToString() + "\r\n " + 
			"PrincipalAdolescentesEstado: " + PrincipalAdolescentesEstado.ToString() + "\r\n " + 
			"PostreAdultosAdolescentesEstado: " + PostreAdultosAdolescentesEstado.ToString() + "\r\n " + 
			"PrincipalChicosEstado: " + PrincipalChicosEstado.ToString() + "\r\n " + 
			"PostreChicosEstado: " + PostreChicosEstado.ToString() + "\r\n " + 
			"MesaDulceEstado: " + MesaDulceEstado.ToString() + "\r\n " + 
			"FinFiestaEstado: " + FinFiestaEstado.ToString() + "\r\n " + 
			"MesaPrincipalEstado: " + MesaPrincipalEstado.ToString() + "\r\n " + 
			"ManteleriaEstado: " + ManteleriaEstado.ToString() + "\r\n " + 
			"ServilletasEstado: " + ServilletasEstado.ToString() + "\r\n " + 
			"SillasEstado: " + SillasEstado.ToString() + "\r\n " + 
			"InvitadosDespues00Estado: " + InvitadosDespues00Estado.ToString() + "\r\n " + 
			"CumpleaniosEnEventoEstado: " + CumpleaniosEnEventoEstado.ToString() + "\r\n " + 
			"TortaAlegoricaEstado: " + TortaAlegoricaEstado.ToString() + "\r\n " + 
			"LleganAlSalonEstado: " + LleganAlSalonEstado.ToString() + "\r\n " + 
			"PlatosEspecialesEstado: " + PlatosEspecialesEstado.ToString() + "\r\n " + 
			"ServiciodeVinoChampagneEstado: " + ServiciodeVinoChampagneEstado.ToString() + "\r\n " + 
			"Acreditaciones: " + Acreditaciones.ToString() + "\r\n " + 
			"ListaInvitados: " + ListaInvitados.ToString() + "\r\n " + 
			"ListaCocheras: " + ListaCocheras.ToString() + "\r\n " + 
			"AcreditacionesEstado: " + AcreditacionesEstado.ToString() + "\r\n " + 
			"ListaInvitadosEstado: " + ListaInvitadosEstado.ToString() + "\r\n " + 
			"ListaCocherasEstado: " + ListaCocherasEstado.ToString() + "\r\n " + 
			"Layout: " + Layout.ToString() + "\r\n " + 
			"AlfombraRoja: " + AlfombraRoja.ToString() + "\r\n " + 
			"Anexo7: " + Anexo7.ToString() + "\r\n " + 
			"LayoutEstado: " + LayoutEstado.ToString() + "\r\n " + 
			"AlfombraRojaEstado: " + AlfombraRojaEstado.ToString() + "\r\n " + 
			"Anexo7Estado: " + Anexo7Estado.ToString() + "\r\n " + 
			"Ramo: " + Ramo.ToString() + "\r\n " + 
			"Escenario: " + Escenario.ToString() + "\r\n " + 
			"IngresoProveedoresLugar: " + IngresoProveedoresLugar.ToString() + "\r\n " + 
			"ContactoResponsableLugar: " + ContactoResponsableLugar.ToString() + "\r\n " + 
			"TelefonoResponsableLugar: " + TelefonoResponsableLugar.ToString() + "\r\n " + 
			"FechaArmadoLogistica: " + FechaArmadoLogistica.ToString() + "\r\n " + 
			"FechaArmadoSalon: " + FechaArmadoSalon.ToString() + "\r\n " + 
			"FechaDesarmadoSalon: " + FechaDesarmadoSalon.ToString() + "\r\n " + 
			"HoraArmadoLogistica: " + HoraArmadoLogistica.ToString() + "\r\n " + 
			"HoraDesarmadoSalon: " + HoraDesarmadoSalon.ToString() + "\r\n " + 
			"HoraArmadoSalon: " + HoraArmadoSalon.ToString() + "\r\n " + 
			"CantPersonasAfectadasArmado: " + CantPersonasAfectadasArmado.ToString() + "\r\n " + 
			"SePidioHielo: " + SePidioHielo.ToString() + "\r\n " + 
			"SePidioLogistica: " + SePidioLogistica.ToString() + "\r\n " + 
			"SePidioManteleria: " + SePidioManteleria.ToString() + "\r\n " + 
			"SePidioMoviliario: " + SePidioMoviliario.ToString() + "\r\n " + 
			"ObservacionesHielo: " + ObservacionesHielo.ToString() + "\r\n " + 
			"ObservacionesMoviliario: " + ObservacionesMoviliario.ToString() + "\r\n " + 
			"ObservacionesLogistica: " + ObservacionesLogistica.ToString() + "\r\n " + 
			"ObservacionesManteleria: " + ObservacionesManteleria.ToString() + "\r\n " ;
		}
        public OrganizacionPresupuestoDetalle()
        {
            Id = -1;

			CantPersonasAfectadasArmado = "0";
        }

		public Presupuestos GetRelatedPresupuestoId()
		{
			Presupuestos presupuestos = PresupuestosOperator.GetOneByIdentity(PresupuestoId);
			return presupuestos;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "PresupuestoId": return false;
				case "MotivoFestejo": return true;
				case "Mail": return true;
				case "Tel": return true;
				case "LocacionOtra": return true;
				case "EnvioMailPresentacion": return true;
				case "FechaMailPresentacion": return true;
				case "RealizoReunionConCliente": return true;
				case "Direccion": return true;
				case "Bocados": return true;
				case "Islas": return true;
				case "Entrada": return true;
				case "PrincipalAdultos": return true;
				case "PrincipalAdolescentes": return true;
				case "PostreAdultosAdolescentes": return true;
				case "PrincipalChicos": return true;
				case "PostreChicos": return true;
				case "MesaDulce": return true;
				case "FinFiesta": return true;
				case "MesaPrincipal": return true;
				case "Manteleria": return true;
				case "Servilletas": return true;
				case "Sillas": return true;
				case "InvitadosDespues00": return true;
				case "CumpleaniosEnEvento": return true;
				case "TortaAlegorica": return true;
				case "LleganAlSalon": return true;
				case "PlatosEspeciales": return true;
				case "ServiciodeVinoChampagne": return true;
				case "ObservacionBarras": return true;
				case "ObservacionCatering": return true;
				case "ObservacionTecnica": return true;
				case "ObservacionAmbientacion": return true;
				case "ObservacionParticulares": return true;
				case "ObservacionesAdicionales": return true;
				case "BocadosEstado": return false;
				case "IslasEstado": return false;
				case "EntradaEstado": return false;
				case "PrincipalAdultosEstado": return false;
				case "PrincipalAdolescentesEstado": return false;
				case "PostreAdultosAdolescentesEstado": return false;
				case "PrincipalChicosEstado": return false;
				case "PostreChicosEstado": return false;
				case "MesaDulceEstado": return false;
				case "FinFiestaEstado": return false;
				case "MesaPrincipalEstado": return false;
				case "ManteleriaEstado": return false;
				case "ServilletasEstado": return false;
				case "SillasEstado": return false;
				case "InvitadosDespues00Estado": return false;
				case "CumpleaniosEnEventoEstado": return false;
				case "TortaAlegoricaEstado": return false;
				case "LleganAlSalonEstado": return false;
				case "PlatosEspecialesEstado": return false;
				case "ServiciodeVinoChampagneEstado": return false;
				case "Acreditaciones": return true;
				case "ListaInvitados": return true;
				case "ListaCocheras": return true;
				case "AcreditacionesEstado": return false;
				case "ListaInvitadosEstado": return false;
				case "ListaCocherasEstado": return false;
				case "Layout": return true;
				case "AlfombraRoja": return true;
				case "Anexo7": return true;
				case "LayoutEstado": return false;
				case "AlfombraRojaEstado": return false;
				case "Anexo7Estado": return false;
				case "Ramo": return false;
				case "Escenario": return false;
				case "IngresoProveedoresLugar": return true;
				case "ContactoResponsableLugar": return true;
				case "TelefonoResponsableLugar": return true;
				case "FechaArmadoLogistica": return true;
				case "FechaArmadoSalon": return true;
				case "FechaDesarmadoSalon": return true;
				case "HoraArmadoLogistica": return true;
				case "HoraDesarmadoSalon": return true;
				case "HoraArmadoSalon": return true;
				case "CantPersonasAfectadasArmado": return true;
				case "SePidioHielo": return false;
				case "SePidioLogistica": return false;
				case "SePidioManteleria": return false;
				case "SePidioMoviliario": return false;
				case "ObservacionesHielo": return true;
				case "ObservacionesMoviliario": return true;
				case "ObservacionesLogistica": return true;
				case "ObservacionesManteleria": return true;
				default: return false;
			}
		}
    }
}

