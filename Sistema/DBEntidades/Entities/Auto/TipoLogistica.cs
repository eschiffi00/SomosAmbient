using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoLogistica
    {
		public int Id { get; set; }
		public string Concepto { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Concepto: " + Concepto.ToString() + "\r\n " ;
		}
        public TipoLogistica()
        {
            Id = -1;

        }



		public List<CostoLogistica> GetRelatedCostoLogisticas()
		{
			return CostoLogisticaOperator.GetAll().Where(x => x.ConceptoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Concepto": return false;
				default: return false;
			}
		}
    }
}

