using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Ciudades
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public string CP { get; set; }
		public int ProvinciaId { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"CP: " + CP.ToString() + "\r\n " + 
			"ProvinciaId: " + ProvinciaId.ToString() + "\r\n " ;
		}
        public Ciudades()
        {
			Id = -1;

        }

		public Provincias GetRelatedProvinciaId()
		{
			Provincias provincias = ProvinciasOperator.GetOneByIdentity(ProvinciaId);
			return provincias;
		}



		public List<Empleados> GetRelatedEmpleadosesLocalidad()
		{
			return EmpleadosOperator.GetAll().Where(x => x.LocalidadId == Id).ToList();
		}
		public List<Empleados> GetRelatedEmpleadosesCiudadLegal()
		{
			return EmpleadosOperator.GetAll().Where(x => x.CiudadLegalId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "CP": return true;
				case "ProvinciaId": return false;
				default: return false;
			}
		}
    }
}

