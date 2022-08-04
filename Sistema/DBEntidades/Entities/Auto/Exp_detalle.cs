using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Exp_detalle
    {
		public int Id { get; set; }
		public int ExperienciaID { get; set; }
		public int ClienteID { get; set; }
		public int LocacionID { get; set; }
		public DateTime? Fecha { get; set; }
		public int? HoraInicio { get; set; }
		public int? HoraFin { get; set; }
		public int? Asistencia { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"ExperienciaID: " + ExperienciaID.ToString() + "\r\n " + 
			"ClienteID: " + ClienteID.ToString() + "\r\n " + 
			"LocacionID: " + LocacionID.ToString() + "\r\n " + 
			"Fecha: " + Fecha.ToString() + "\r\n " + 
			"HoraInicio: " + HoraInicio.ToString() + "\r\n " + 
			"HoraFin: " + HoraFin.ToString() + "\r\n " + 
			"Asistencia: " + Asistencia.ToString() + "\r\n " ;
		}
        public Exp_detalle()
        {
            Id = -1;

        }

		public Experiencia GetRelatedExperienciaID()
		{
			Experiencia experiencia = ExperienciaOperator.GetOneByIdentity(ExperienciaID);
			return experiencia;
		}





		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "ExperienciaID": return false;
				case "ClienteID": return false;
				case "LocacionID": return false;
				case "Fecha": return true;
				case "HoraInicio": return true;
				case "HoraFin": return true;
				case "Asistencia": return true;
				default: return false;
			}
		}
    }
}

