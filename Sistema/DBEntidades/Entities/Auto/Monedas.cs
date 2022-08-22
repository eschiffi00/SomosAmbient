using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Monedas
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public string DescripcionCorta { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"DescripcionCorta: " + DescripcionCorta.ToString() + "\r\n " ;
		}
        public Monedas()
        {
            Id = -1;

        }



		public List<Cuentas> GetRelatedCuentases()
		{
			return CuentasOperator.GetAll().Where(x => x.MonedaId == Id).ToList();
		}
		public List<ConversionMonedas> GetRelatedConversionMonedasesMonedaOrigen()
		{
			return ConversionMonedasOperator.GetAll().Where(x => x.MonedaOrigenId == Id).ToList();
		}
		public List<ConversionMonedas> GetRelatedConversionMonedasesMonedaDestino()
		{
			return ConversionMonedasOperator.GetAll().Where(x => x.MonedaDestinoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "DescripcionCorta": return true;
				default: return false;
			}
		}
    }
}

