using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using DbEntidades.Entities;
using System.Data.SqlClient;
using LibDB2;

namespace DbEntidades.Operators
{
    public partial class RecetaOperator
    {
        public static List<REC_detalle> GetDetalleById(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRecetaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(REC_detalle).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<REC_detalle> lista = new List<REC_detalle>();
            DataTable dt = db.GetDataSet("select " + columnas + " from REC_detalle where RecetaID = " + ID.ToString()).Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                REC_detalle receta = new REC_detalle();
                foreach (PropertyInfo prop in typeof(REC_detalle).GetProperties())
                {
                    object value = dr[prop.Name];
                    if (value == DBNull.Value) value = null;
                    try { prop.SetValue(receta, value, null); }
                    catch (System.ArgumentException) { }
                }
                lista.Add(receta);
            }
            return lista;
        }
        public static List<REC_pasos> GetPasosById(int ID)
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRecetaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(REC_pasos).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<REC_pasos> lista = new List<REC_pasos>();
            DataTable dt = db.GetDataSet("select " + columnas + " from REC_pasos where PasosID = " + ID.ToString()).Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                REC_pasos paso = new REC_pasos();
                foreach (PropertyInfo prop in typeof(REC_pasos).GetProperties())
                {
                    object value = dr[prop.Name];
                    if (value == DBNull.Value) value = null;
                    try { prop.SetValue(paso, value, null); }
                    catch (System.ArgumentException) { }
                }
                lista.Add(paso);
            }
            return lista;
        }

        public static List<RecetaDetalle> GetAllWithDetails()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoRecetaBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Receta).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<RecetaDetalle> lista = new List<RecetaDetalle>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Receta").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Receta Receta = new Receta();
                foreach (PropertyInfo prop in typeof(Receta).GetProperties())
                {
                    object value = dr[prop.Name];
                    if (value == DBNull.Value) value = null;
                    try { prop.SetValue(Receta, value, null); }
                    catch (System.ArgumentException) { }
                }
                if (Receta.StockID > 0)
                {
                    RecetaDetalle RecetaDetail = new RecetaDetalle();
                    RecetaDetail.ID = Receta.ID;
                    RecetaDetail.Descripcion = Receta.Descripcion;
                    RecetaDetail.Detalle = GetDetalleById(Receta.ID);
                    RecetaDetail.EstadoID = Receta.EstadoID;
                    RecetaDetail.Pasos = GetPasosById(Receta.RecPasosID);
                    RecetaDetail.Cantidad = StockOperator.GetOneByIdentity(RecetaDetail.StockID).Cantidad;
                    RecetaDetail.Peso = StockOperator.GetOneByIdentity(RecetaDetail.StockID).Peso;

                    lista.Add(RecetaDetail);
                }
            }
            return lista;
        }
        public static void Delete(int id)
        {
            Receta u = GetOneByIdentity(id);
            u.EstadoID = EstadoOperator.GetDeshabilitadoID();
            Update(u);
        }
    }
}
