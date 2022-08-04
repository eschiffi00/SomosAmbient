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
    public partial class ItemsOperator
    {

        public static void Delete(int id)
        {
            Items u = GetOneByIdentity(id);
            u.EstadoId = EstadoOperator.GetDeshabilitadoID();
            Update(u);
        }
        public static List<ItemsListado> GetAllWithDetails()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoItemBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Items).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ItemsListado> lista = new List<ItemsListado>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Item").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Items item = new Items();
                foreach (PropertyInfo prop in typeof(Items).GetProperties())
                {
                    object value = dr[prop.Name];
                    if (value == DBNull.Value) value = null;
                    try { prop.SetValue(item, value, null); }
                    catch (System.ArgumentException) { }
                }
                    ItemsListado itemStock = new ItemsListado();
                    itemStock.Id = item.Id;
                    itemStock.Detalle = item.Detalle;
                    itemStock.CategoriaID = 1;
                    itemStock.CategoriaDescripcion = "aqui va la vategoria";
                    itemStock.CuentaId = item.CuentaId;
                    itemStock.CuentaDescripcion = "Pendiente tabla cuentas";
                    itemStock.DepositoId = item.DepositoId;
                    itemStock.ItemDetalleId = item.ItemDetalleId;
                    itemStock.EstadoId = item.EstadoId;
                var unidadid = INVENTARIO_ProductoOperator.GetOneByIdentity(itemStock.DepositoId).UnidadId;
                    itemStock.Unidad = INVENTARIO_UnidadesOperator.GetOneByIdentity(unidadid).Descripcion;
                    itemStock.Cantidad = INVENTARIO_ProductoOperator.GetOneByIdentity(itemStock.DepositoId).Cantidad;
                    lista.Add(itemStock);
            }
            return lista;
        }
        public static List<ItemsCombo> GetAllForCombo()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoItemBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Items).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ItemsCombo> lista = new List<ItemsCombo>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Item").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Items item = new Items();
                foreach (PropertyInfo prop in typeof(Items).GetProperties())
                {
                    object value = dr[prop.Name];
                    if (value == DBNull.Value) value = null;
                    try { prop.SetValue(item, value, null); }
                    catch (System.ArgumentException) { }
                }
                    ItemsCombo ItemsCombo = new ItemsCombo();
                    ItemsCombo.Id = item.Id;
                    ItemsCombo.Detalle = item.Detalle;
                    lista.Add(ItemsCombo);

            }
            return lista;
        }
    }
}
