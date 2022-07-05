using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class CondicionGanancias
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public decimal Tope { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"Tope: " + Tope.ToString() + "\r\n " ;
		}
        public CondicionGanancias()
        {
            Id = -1;

        }



		public List<Entidades> GetRelatedEntidadeses()
		{
			return EntidadesOperator.GetAll().Where(x => x.CondicionGananciaId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "Tope": return true;
				default: return false;
			}
		}
    }
}

