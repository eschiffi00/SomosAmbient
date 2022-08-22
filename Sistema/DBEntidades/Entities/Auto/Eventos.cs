using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Eventos
    {
		public int Id { get; set; }
		public int ClienteId { get; set; }
		public string ApellidoNombreCliente { get; set; }
		public string RazonSocial { get; set; }
		public string Mail { get; set; }
		public string Tel { get; set; }
		public DateTime Fecha { get; set; }
		public int EstadoId { get; set; }
		public int EmpleadoId { get; set; }
		public string Comentario { get; set; }
		public decimal MontoSena { get; set; }
		public DateTime? FechaSena { get; set; }
		public int? ClienteBisId { get; set; }
		public int? PresupuestoAprobadoId { get; set; }
		public object ComprobanteAprovacion { get; set; }
		public string ComprobanteAprovacionExtension { get; set; }
		public int? FormadePagoId { get; set; }
		public string NroComprobanteTransSenia { get; set; }
		public DateTime? FechaComprobanteTransSenia { get; set; }
		public object ComprobanteTransferencia { get; set; }
		public string ComprobanteTransferenciaExtension { get; set; }
		public int? ChequeSenaId { get; set; }
		public decimal Indexacion { get; set; }
		public string TipoIndexacion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ClienteId: " + ClienteId.ToString() + "\r\n " + 
			"ApellidoNombreCliente: " + ApellidoNombreCliente.ToString() + "\r\n " + 
			"RazonSocial: " + RazonSocial.ToString() + "\r\n " + 
			"Mail: " + Mail.ToString() + "\r\n " + 
			"Tel: " + Tel.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"EmpleadoId: " + EmpleadoId.ToString() + "\r\n " + 
			"Comentario: " + Comentario.ToString() + "\r\n " + 
			"MontoSena: " + MontoSena.ToString() + "\r\n " + 
			"FechaSena: " + FechaSena.ToString() + "\r\n " + 
			"ClienteBisId: " + ClienteBisId.ToString() + "\r\n " + 
			"PresupuestoAprobadoId: " + PresupuestoAprobadoId.ToString() + "\r\n " + 
			"ComprobanteAprovacion: " + ComprobanteAprovacion.ToString() + "\r\n " + 
			"ComprobanteAprovacionExtension: " + ComprobanteAprovacionExtension.ToString() + "\r\n " + 
			"FormadePagoId: " + FormadePagoId.ToString() + "\r\n " + 
			"NroComprobanteTransSenia: " + NroComprobanteTransSenia.ToString() + "\r\n " + 
			"FechaComprobanteTransSenia: " + FechaComprobanteTransSenia.ToString() + "\r\n " + 
			"ComprobanteTransferencia: " + ComprobanteTransferencia.ToString() + "\r\n " + 
			"ComprobanteTransferenciaExtension: " + ComprobanteTransferenciaExtension.ToString() + "\r\n " + 
			"ChequeSenaId: " + ChequeSenaId.ToString() + "\r\n " + 
			"Indexacion: " + Indexacion.ToString() + "\r\n " + 
			"TipoIndexacion: " + TipoIndexacion.ToString() + "\r\n " ;
		}
        public Eventos()
        {
            Id = -1;

        }

		public Estados GetRelatedEstadoId()
		{
			Estados estados = EstadosOperator.GetOneByIdentity(EstadoId);
			return estados;
		}

		public Empleados GetRelatedEmpleadoId()
		{
			Empleados empleados = EmpleadosOperator.GetOneByIdentity(EmpleadoId);
			return empleados;
		}

		public FormasdePago GetRelatedFormadePagoId()
		{
			if (FormadePagoId != null)
			{
				FormasdePago formasdePago = FormasdePagoOperator.GetOneByIdentity(FormadePagoId ?? 0);
				return formasdePago;
			}
			return null;
		}

		public Cheques GetRelatedChequeSenaId()
		{
			if (ChequeSenaId != null)
			{
				Cheques cheques = ChequesOperator.GetOneByIdentity(ChequeSenaId ?? 0);
				return cheques;
			}
			return null;
		}



		public List<ReciboEventoPresupuesto> GetRelatedReciboEventoPresupuestos()
		{
			return ReciboEventoPresupuestoOperator.GetAll().Where(x => x.EventoId == Id).ToList();
		}
		public List<Presupuestos> GetRelatedPresupuestoses()
		{
			return PresupuestosOperator.GetAll().Where(x => x.EventoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ClienteId": return false;
				case "ApellidoNombreCliente": return true;
				case "RazonSocial": return true;
				case "Mail": return true;
				case "Tel": return true;
				case "Fecha": return false;
				case "EstadoId": return false;
				case "EmpleadoId": return false;
				case "Comentario": return true;
				case "MontoSena": return true;
				case "FechaSena": return true;
				case "ClienteBisId": return true;
				case "PresupuestoAprobadoId": return true;
				case "ComprobanteAprovacion": return true;
				case "ComprobanteAprovacionExtension": return true;
				case "FormadePagoId": return true;
				case "NroComprobanteTransSenia": return true;
				case "FechaComprobanteTransSenia": return true;
				case "ComprobanteTransferencia": return true;
				case "ComprobanteTransferenciaExtension": return true;
				case "ChequeSenaId": return true;
				case "Indexacion": return true;
				case "TipoIndexacion": return true;
				default: return false;
			}
		}
    }
}

