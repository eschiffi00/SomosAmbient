using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Momento
    {
		public int ID { get; set; }
		public string Descripcion { get; set; }
		public int EstadoID { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"ID: " + ID.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"EstadoID: " + EstadoID.ToString() + "\r\n " ;
		}
        public Momento()
        {
            ID = -1;

        }

		public Estado GetRelatedEstadoID()
		{
			Estado estado = EstadoOperator.GetOneByIdentity(EstadoID);
			return estado;
		}



		public List<Experiencia> GetRelatedExperiencias()
		{
			return ExperienciaOperator.GetAll().Where(x => x.MomentoID == ID).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "ID": return false;
				case "Descripcion": return false;
				case "EstadoID": return false;
				default: return false;
			}
		}
    }
}

