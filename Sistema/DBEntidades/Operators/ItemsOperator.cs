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
        public static List<ItemsListado> GetAllWithDetails()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProductoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Items).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ItemsListado> lista = new List<ItemsListado>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Items").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Items Items = new Items();
                foreach (PropertyInfo prop in typeof(Items).GetProperties())
                {
                    object value = dr[prop.Name];
                    if (value == DBNull.Value) value = null;
                    try { prop.SetValue(Items, value, null); }
                    catch (System.ArgumentException) { }
                }
                ItemsListado ItemsStock = new ItemsListado();
                ItemsStock.Id = Items.Id;
                ItemsStock.Detalle = Items.Detalle;
                ItemsStock.CategoriaItemId = Items.CategoriaItemId;
                ItemsStock.CategoriaDescripcion = "Pendiente tabla cuentas";
                ItemsStock.CuentaId = Items.CuentaId;
                ItemsStock.CuentaDescripcion = CuentasOperator.GetOneByIdentity(ItemsStock.CuentaId).Descripcion;
                ItemsStock.Costo = Items.Costo;
                ItemsStock.Margen = Items.Margen;
                ItemsStock.Precio = Items.Precio;
                ItemsStock.DepositoId = Items.DepositoId;
                ItemsStock.Unidad = INVENTARIO_UnidadesOperator.GetOneByIdentity(INVENTARIO_ProductoOperator.GetOneByIdentity(Items.DepositoId).UnidadId).Descripcion;
                ItemsStock.Cantidad = INVENTARIO_ProductoOperator.GetOneByIdentity(INVENTARIO_DepositosOperator.GetOneByIdentity(Items.DepositoId).Id).Cantidad;
                ItemsStock.EstadoId = Items.EstadoId;
                
                lista.Add(ItemsStock);
            }
            return lista;
        }
        public static List<ItemsCombo> GetAllForCombo()
        {
            if (!DbEntidades.Seguridad.Permiso("PermisoProductoBrowse")) throw new PermisoException();
            string columnas = string.Empty;
            foreach (PropertyInfo prop in typeof(Items).GetProperties()) columnas += prop.Name + ", ";
            columnas = columnas.Substring(0, columnas.Length - 2);
            DB db = new DB();
            List<ItemsCombo> lista = new List<ItemsCombo>();
            DataTable dt = db.GetDataSet("select " + columnas + " from Items").Tables[0];
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Items Items = new Items();
                foreach (PropertyInfo prop in typeof(Items).GetProperties())
                {
                    object value = dr[prop.Name];
                    if (value == DBNull.Value) value = null;
                    try { prop.SetValue(Items, value, null); }
                    catch (System.ArgumentException) { }
                }
                ItemsCombo ItemsStock = new ItemsCombo();
                ItemsStock.Id = Items.Id;
                ItemsStock.Detalle = Items.Detalle;
                lista.Add(ItemsStock);
            }
            return lista;
        }
        public static void Delete(int id)
        {
            Items u = ItemsOperator.GetOneByIdentity(id);
            u.EstadoId = EstadoOperator.GetDeshabilitadoID();
            Update(u);
        }
    }
}