using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace WebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            /*
            routes.MapPageRoute("ExpenseCurrentYearRoute",
            "ExpenseReportCurrent/{locale}", "~/expenses.aspx",
            false,
            new RouteValueDictionary 
                { { "locale", "US" }, { "year", DateTime.Now.Year.ToString() } },
            new RouteValueDictionary 
                { { "locale", "[a-z]{2}" }, { "year", @"\d{4}" } });
            */

            //-----------------app Backend
            routes.Ignore("{resource}.axd/{*pathInfo}"); //sin esta mierda no andaba el ScripManager. Tiene que estar arriba de todo

            //routes.MapPageRoute("", "", "~/app/Seguridad/UsuarioLogin.aspx");
            routes.MapPageRoute("Login", "login", "~/app/Seguridad/UsuarioLogin.aspx");
            routes.MapPageRoute("Main", "main", "~/app/Main.aspx");
            //routes.MapPageRoute("Version", "ver", "~/version.aspx");
            
            routes.MapPageRoute("NuevoUsuario", "nuevo-usuario", "~/app/Seguridad/UsuarioEdit.aspx");
            routes.MapPageRoute("EditaUsuario", "edita-usuario/{id}", "~/app/Seguridad/UsuarioEdit.aspx");
            routes.MapPageRoute("ListaUsuarios", "lista-usuarios", "~/app/Seguridad/UsuarioBrowse.aspx");
            routes.MapPageRoute("ClaveMiUsuario", "clave-usuario", "~/app/Seguridad/UsuarioClave.aspx?id=0");
            routes.MapPageRoute("CambioClave", "cambio-clave", "~/app/Seguridad/UsuarioClave.aspx?id=0");
            routes.MapPageRoute("ClaveUsuario", "clave-usuario/{id}", "~/app/Seguridad/UsuarioClave.aspx");
            routes.MapPageRoute("OlvideClave", "olvide-clave", "~/app/Seguridad/UsuarioOlvideClave.aspx");
            routes.MapPageRoute("EditaPermisos", "edita-permisos", "~/app/Seguridad/UsuarioRolEdit.aspx");

            routes.MapPageRoute("ListaProveedor", "lista-proveedor", "~/app/Administracion/ProveedorBrowse.aspx");
            routes.MapPageRoute("NuevoProveedor", "nuevo-proveedor", "~/app/Administracion/ProveedorEdit.aspx");
            routes.MapPageRoute("EditaProveedor", "edita-proveedor/{id}", "~/app/Administracion/ProveedorEdit.aspx");

            routes.MapPageRoute("ListaItems", "lista-item", "~/app/Stock/ItemsBrowse.aspx");
            routes.MapPageRoute("NuevoItem", "nuevo-item", "~/app/Stock/ItemsEdit.aspx");
            routes.MapPageRoute("EditaItems", "edita-items/{id}", "~/app/Stock/ItemsEdit.aspx");

            //routes.MapPageRoute("ListaProductos", "lista-producto", "~/app/Stock/ProductoBrowse.aspx");
            //routes.MapPageRoute("NuevoProducto", "nuevo-producto", "~/app/Stock/ProductoEdit.aspx");
            //routes.MapPageRoute("EditaProductos", "edita-productos/{id}", "~/app/Stock/ProductoEdit.aspx");

            routes.MapPageRoute("ListaRecetas", "lista-receta", "~/app/Stock/RecetaBrowse.aspx");
            routes.MapPageRoute("NuevoReceta", "nuevo-receta", "~/app/Stock/RecetaEdit.aspx");
            routes.MapPageRoute("EditaRecetas", "edita-recetas/{id}", "~/app/Stock/RecetaEdit.aspx");

            routes.MapPageRoute("NuevoCliente", "nuevo-cliente", "~/app/Administracion/ClienteEdit.aspx");
            routes.MapPageRoute("EditaCliente", "edita-cliente/{id}", "~/app/Administracion/ClienteEdit.aspx");
            routes.MapPageRoute("ListaCliente", "lista-cliente", "~/app/Administracion/ClienteBrowse.aspx");


            /*
            routes.MapPageRoute("NuevoCliente", "nuevo-cliente", "~/app/Ventas/ClienteEdit.aspx");
            routes.MapPageRoute("EditaCliente", "edita-cliente/{id}", "~/app/Ventas/ClienteEdit.aspx");
            routes.MapPageRoute("ListaProveedor", "lista-proveedor", "~/app/Ventas/ProveedorBrowse.aspx");
            routes.MapPageRoute("NuevaOrdenPortal", "nueva-ordenportal/{fin}/{uid}", "~/app/Ventas/OrdenPortalEdit.aspx");
            routes.MapPageRoute("EditaOrdenPortal", "edita-ordenportal/{id}", "~/app/Ventas/OrdenPortalEdit.aspx");
            routes.MapPageRoute("NuevaOrden2", "nueva-orden/{fin}/{uid}", "~/app/Ventas/OrdenEdit.aspx");
            routes.MapPageRoute("EditaOrden", "edita-orden/{id}", "~/app/Ventas/OrdenEdit.aspx");
            routes.MapPageRoute("NuevaCaracteristica", "nueva-caracteristica/{c}", "~/app/Parametros/CaracteristicaEdit.aspx");
            routes.MapPageRoute("ListaCaracteristicas", "lista-caracteristicas/{c}", "~/app/Parametros/CaracteristicaBrowse.aspx");
            routes.MapPageRoute("ListaCaracteristica1", "lista-caracteristicas/1", "~/app/Parametros/CaracteristicaBrowse.aspx");
            routes.MapPageRoute("ListaCaracteristica2", "lista-caracteristicas/2", "~/app/Parametros/CaracteristicaBrowse.aspx");
            routes.MapPageRoute("ListaCaracteristica3", "lista-caracteristicas/3", "~/app/Parametros/CaracteristicaBrowse.aspx");
            routes.MapPageRoute("ListaCaracteristica4", "lista-caracteristicas/4", "~/app/Parametros/CaracteristicaBrowse.aspx");
            routes.MapPageRoute("EditaCaracteristica", "edita-caracteristica/{c}/{id}", "~/app/Parametros/CaracteristicaEdit.aspx");
            routes.MapPageRoute("VerMails", "ver-mail/{id}", "~/app/Parametros/EmailLogEdit.aspx");
*/
            routes.MapPageRoute("z1", "z1", "~/app/z/z1.aspx");
            //routes.MapPageRoute("z2", "z2", "~/app/zzz/WebForm2.aspx");
            //routes.MapPageRoute("z3", "z3", "~/app/zzz/WebForm3.aspx");
            //routes.MapPageRoute("z4", "z4", "~/app/zzz/WebForm4.aspx");
            //routes.MapPageRoute("z5", "z5", "~/app/zzz/WebForm5.aspx");
            //routes.MapPageRoute("z6", "z6", "~/app/zzz/WebForm6.aspx");
            //routes.MapPageRoute("z7", "z7", "~/app/zzz/WebForm7.aspx");
            //routes.MapPageRoute("z8", "z8", "~/app/zzz/WebForm8.aspx");


            //routes.MapPageRoute("Default", "{*redir}", "~/GeneralError.aspx");
            routes.MapPageRoute("Default", "{*redir}", "~/app/Main.aspx");

        }
    }
}