using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using LibDB2;
using DbEntidades.Operators;

namespace DbEntidades.Entities
{
    public partial class Tiempos
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public object ImagenMarcoSuperior { get; set; }
		public string ImagenMarcoSuperiorExtension { get; set; }
		public int? Orden { get; set; }

		public override string ToString() 
		{
			return "\r\n " + 
			"Id: " + Id.ToString() + "\r\n " + 
			"Descripcion: " + Descripcion.ToString() + "\r\n " + 
			"ImagenMarcoSuperior: " + ImagenMarcoSuperior.ToString() + "\r\n " + 
			"ImagenMarcoSuperiorExtension: " + ImagenMarcoSuperiorExtension.ToString() + "\r\n " + 
			"Orden: " + Orden.ToString() + "\r\n " ;
		}
        public Tiempos()
        {
            Id = -1;

        }



		public List<TipoCateringTiempoProductoItem> GetRelatedTipoCateringTiempoProductoItemes()
		{
			return TipoCateringTiempoProductoItemOperator.GetAll().Where(x => x.TiempoId == Id).ToList();
		}


		public static bool CanBeNull(string colName)
		{
			switch (colName) 
			{
				case "Id": return false;
				case "Descripcion": return false;
				case "ImagenMarcoSuperior": return true;
				case "ImagenMarcoSuperiorExtension": return true;
				case "Orden": return true;
				default: return false;
			}
		}
    }
}

