using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class TipoMovimientos
    {
		public int Id { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " ;
		}
        public TipoMovimientos()
        {
            Id = -1;

        }



		public List<Productos> GetRelatedProductoses()
		{
			return ProductosOperator.GetAll().Where(x => x.TipoMovimientoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				default: return false;
			}
		}
    }
}

