using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Degustacion
    {
		public int Id { get; set; }
		public DateTime FechaDegustacion { get; set; }
		public int? CantidadMesas { get; set; }
		public int Locacion { get; set; }
		public string HoraCorporativo { get; set; }
		public string HoraSocial { get; set; }
		public int EstadoId { get; set; }
		public int ConfirmoTecnica { get; set; }
		public int ConfirmoAmbientacion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"FechaDegustacion: " + FechaDegustacion.ToString() + "\r\n " + 
			"CantidadMesas: " + CantidadMesas.ToString() + "\r\n " + 
			"Locacion: " + Locacion.ToString() + "\r\n " + 
			"HoraCorporativo: " + HoraCorporativo.ToString() + "\r\n " + 
			"HoraSocial: " + HoraSocial.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " + 
			"ConfirmoTecnica: " + ConfirmoTecnica.ToString() + "\r\n " + 
			"ConfirmoAmbientacion: " + ConfirmoAmbientacion.ToString() + "\r\n " ;
		}
        public Degustacion()
        {
            Id = -1;

        }

		public Estados GetRelatedEstadoId()
		{
			Estados estados = EstadosOperator.GetOneByIdentity(EstadoId);
			return estados;
		}



		public List<DegustacionDetalle> GetRelatedDegustacionDetalles()
		{
			return DegustacionDetalleOperator.GetAll().Where(x => x.DegustacionId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "FechaDegustacion": return false;
				case "CantidadMesas": return true;
				case "Locacion": return false;
				case "HoraCorporativo": return false;
				case "HoraSocial": return false;
				case "EstadoId": return false;
				case "ConfirmoTecnica": return false;
				case "ConfirmoAmbientacion": return false;
				default: return false;
			}
		}
    }
}

