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
                ItemsListado ItemsDetail = new ItemsListado();
                ItemsDetail.Id = Items.Id;
                ItemsDetail.Detalle = Items.Detalle;
                ItemsDetail.CategoriaItemId = Items.CategoriaItemId;
                ItemsDetail.CategoriaDescripcion = CategoriasOperator.GetOneByIdentity(Items.CategoriaItemId).Descripcion;
                ItemsDetail.CuentaId = Items.CuentaId;
                if (ItemsDetail.CuentaId == 0 || ItemsDetail.CuentaId == null)
                {
                    ItemsDetail.CuentaDescripcion = "";
                }
                else
                {
                    ItemsDetail.CuentaDescripcion = CuentasOperator.GetOneByIdentity((int)ItemsDetail.CuentaId).Descripcion;
                }
                ItemsDetail.Costo = Items.Costo;
                ItemsDetail.Margen = Items.Margen;
                ItemsDetail.Precio = Items.Precio;
                //ItemsDetail.DepositoId = Items.DepositoId;
                ItemsDetail.DepositoId = 1;
                //ItemsDetail.Unidad = INVENTARIO_UnidadesOperator.GetOneByIdentity(INVENTARIO_ProductoOperator.GetOneByIdentity(Items.DepositoId).UnidadId).Descripcion;
                //ItemsDetail.Cantidad = INVENTARIO_ProductoOperator.GetOneByIdentity(INVENTARIO_DepositosOperator.GetOneByIdentity(Items.DepositoId).Id).Cantidad;
                ItemsDetail.Unidad = "";
                ItemsDetail.Cantidad = 0;
                ItemsDetail.EstadoId = Items.EstadoId;
                
                lista.Add(ItemsDetail);
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
            u.EstadoId = EstadosOperator.GetDeshabilitadoID();
            Update(u);
        }
    }
}