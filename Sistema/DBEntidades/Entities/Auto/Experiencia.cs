using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Experiencia
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public int ExpTipoID { get; set; }
		public int Caracteristica { get; set; }
		public int MomentoID { get; set; }
		public string Jornada { get; set; }
		public int EstadoID { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"ExpTipoID: " + ExpTipoID.ToString() + "\r\n " + 
			"Caracteristica: " + Caracteristica.ToString() + "\r\n " + 
			"MomentoID: " + MomentoID.ToString() + "\r\n " + 
			"Jornada: " + Jornada.ToString() + "\r\n " + 
			"EstadoID: " + EstadoID.ToString() + "\r\n " ;
		}
        public Experiencia()
        {
            ID = -1;

        }

		public TipoExperiencia GetRelatedExpTipoID()
		{
			TipoExperiencia tipoExperiencia = TipoExperienciaOperator.GetOneByIdentity(ExpTipoID);
			return tipoExperiencia;
		}

		public Momento GetRelatedMomentoID()
		{
			Momento momento = MomentoOperator.GetOneByIdentity(MomentoID);
			return momento;
		}

		public Estado GetRelatedEstadoID()
		{
			Estado estado = EstadoOperator.GetOneByIdentity(EstadoID);
			return estado;
		}



		public List<Exp_detalle> GetRelatedExp_detalles()
		{
			return Exp_detalleOperator.GetAll().Where(x => x.ExperienciaID == ID).ToList();
		}
		public List<ExpTiempoProd> GetRelatedExpTiempoProdes()
		{
			return ExpTiempoProdOperator.GetAll().Where(x => x.ExperienciaID == ID).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "Descripcion": return false;
				case "ExpTipoID": return false;
				case "Caracteristica": return false;
				case "MomentoID": return false;
				case "Jornada": return false;
				case "EstadoID": return false;
				default: return false;
			}
		}
    }
}

