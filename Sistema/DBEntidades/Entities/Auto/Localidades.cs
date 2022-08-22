using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Localidades
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " ;
		}
        public Localidades()
        {
            Id = -1;

        }



		public List<Locaciones> GetRelatedLocacioneses()
		{
			return LocacionesOperator.GetAll().Where(x => x.LocalidadId == Id).ToList();
		}
		public List<CostoLogistica> GetRelatedCostoLogisticas()
		{
			return CostoLogisticaOperator.GetAll().Where(x => x.Localidad == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return true;
				default: return false;
			}
		}
    }
}

