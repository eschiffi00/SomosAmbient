using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class AmbientacionCI
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int Flete { get; set; }
		public int EstadoId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Flete: " + Flete.ToString() + "\r\n " + 
			"EstadoId: " + EstadoId.ToString() + "\r\n " ;
		}
        public AmbientacionCI()
        {
            Id = -1;

        }



		public List<CostosPaquetesCIAmbientacion> GetRelatedCostosPaquetesCIAmbientaciones()
		{
			return CostosPaquetesCIAmbientacionOperator.GetAll().Where(x => x.PaqueteCIID == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "Flete": return false;
				case "EstadoId": return false;
				default: return false;
			}
		}
    }
}

