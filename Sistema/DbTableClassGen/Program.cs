using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using LibDB2;

namespace DbTableClassGen
{
    class Program
    {
        public static string CR = "\r\n";
        public static string ConnString = ConfigurationSettings.AppSettings["ConnectionString"];
        public static string Tablas = ConfigurationSettings.AppSettings["Tablas"];
        public static string TableName;
        public static string EntidadBase = string.Empty;
        public static string OperatorBase = string.Empty;
        public static string SQLTypes = string.Empty;

        public class Columna
        {
            public string Nombre;
            public int NroOrden;
            public string Tipo;
            public bool isPk;
            public bool Identity;
            public bool isNullable;
            public string FK_Table;
            public string FK_Column;
            public bool FK_isNullable;
            public bool FK_isUnique;
            public string Column_Default;
            public int MaxLength;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Ejecutando...");
            //string outputdir = @"..\..\..\DBEntidades\";
            string outputdir = ConfigurationSettings.AppSettings["DirDestino"];
            Console.WriteLine($"Directorio destino: {Path.GetFullPath(outputdir)}");
            //string[] TableNameList = {

            //    "Usuario", "Bloqueo", "UsuarioRol", "Rol", "Permiso", "Form",
            //    "Parametro",
            //    "Empresa", "Pais", "Provincia",
            //    "Cliente", "Subcliente",
            //    "TipoUnidad", "TipoServicio", 
            //    "ClienteServicio", "Tarifa", 
            //    "Deposito", "Ubicacion",
            //    "Producto", "ProductoUnidadServicio", "Ingreso", "IngresoDetalle", "Almacenamiento",
            //    "ClienteSequenceB"
            //};
            Tablas = Tablas.Replace("\n", "").Replace("\r", "").Replace(" ", "");
            string[] TableNameList = Tablas.Split(',');

            foreach (string tableName in TableNameList)
            {
                Console.WriteLine("Procesando tabla: " + tableName);
                TableName = tableName;
                GeneraBase();
                Directory.CreateDirectory(outputdir + @"Entities\Auto");
                System.IO.File.WriteAllText(outputdir + @"Entities\Auto\" + TableName + ".cs", EntidadBase);
                Directory.CreateDirectory(outputdir + @"Operators\Auto");
                System.IO.File.WriteAllText(outputdir + @"Operators\Auto\" + TableName + "Operator.cs", OperatorBase);
            }

            GeneraSQLTypes();
            System.IO.File.WriteAllText(outputdir + @"SQLTypes.cs", SQLTypes);

            try { System.IO.File.Copy(@"..\..\Templates\PermisoException.cs", outputdir + "PermisoException.cs"); } catch { }
            //finalmente copio el Seguridad.cs si es que no existe
            try { System.IO.File.Copy(@"..\..\Templates\Seguridad.cs", outputdir + "Seguridad.cs"); } catch { }


            try { System.IO.File.Copy(@"..\..\Templates\Utility.cs", outputdir + "Utility.cs"); } catch { }





            Console.Write("\nOprima Enter para finalizar ");
            Console.ReadLine();
        }

        public static void GeneraSQLTypes()
        {
            DB db = new DB(ConnString);
            string sql = @"select tt.name, c.name colname, st.name coltype
                            from sys.table_types tt
                            inner join sys.columns c on c.object_id = tt.type_table_object_id
                            INNER JOIN sys.systypes AS ST  ON ST.xtype = c.system_type_id
                            order by tt.name, c.column_id";
            DataTable dt = db.GetDataSet(sql).Tables[0];

            string typeClasses = "public class ";
            if (dt.Rows.Count > 0)
            {
                string nombreType = dt.Rows[0]["name"].ToString();
                typeClasses += nombreType + CR + "\t{" + CR;
                foreach (DataRow dr in dt.AsEnumerable())
                {
                    if (dr["name"].ToString() == nombreType)
                    {
                        typeClasses += "\t\tpublic " + ConvierteTipos(dr["coltype"].ToString(), "NO") + " " + dr["colname"].ToString() + ";" + CR;
                    }
                    else
                    {
                        typeClasses += "\t}" + CR + CR + "\tpublic class ";
                        nombreType = dr["name"].ToString();
                        typeClasses += nombreType + CR + "\t{" + CR;
                        typeClasses += "\t\tpublic " + ConvierteTipos(dr["coltype"].ToString(), "NO") + " " + dr["colname"].ToString() + ";" + CR;
                    }
                }
                typeClasses += "\t}" + CR;
                
                //Genera types de sql
                SQLTypes = System.IO.File.ReadAllText(@"..\..\Templates\SQLTypes.cs");
                SQLTypes = SQLTypes.Replace("<SQLTypeClass>", typeClasses);
            }
        }

        public static void GeneraBase()
        {
            //Dictionary<string, string> colTipo = new Dictionary<string, string>();

            List<Columna> columnas = new List<Columna>();
            DB.deshabilita_encripcion = true;
            Boolean tablaConColumnaEstadoId = false;
            DB db = new DB(ConnString);
            string sql = @"
                select 
                c.TABLE_CATALOG, c.TABLE_SCHEMA, c.TABLE_NAME, c.COLUMN_NAME, c.ORDINAL_POSITION,
                c.IS_NULLABLE, c.DATA_TYPE, c.CHARACTER_MAXIMUM_LENGTH,
                IsNull(OBJECTPROPERTY(OBJECT_ID(u.CONSTRAINT_SCHEMA + '.' + u.CONSTRAINT_NAME), 'IsPrimaryKey'),0) [isPK], i.is_identity [isIdentity],
                fk.ReferenceTableName [FK_Table], fk.ReferenceColumnName [FK_Column], fks.IS_NULLABLE FK_IS_NULLABLE, uni.[FK_IS_UNIQUE], c.COLUMN_DEFAULT
                from information_schema.[COLUMNS] c
                left outer join INFORMATION_SCHEMA.KEY_COLUMN_USAGE u on u.COLUMN_NAME = c.COLUMN_NAME and u.TABLE_NAME = c.TABLE_NAME and OBJECTPROPERTY(OBJECT_ID(u.CONSTRAINT_SCHEMA + '.' + u.CONSTRAINT_NAME), 'IsPrimaryKey') = 1
                left outer join 
	                (SELECT f.name AS ForeignKey, OBJECT_NAME(f.parent_object_id) AS TableName,
                    COL_NAME(fc.parent_object_id, fc.parent_column_id) AS ColumnName,
                    OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName,
                    COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS ReferenceColumnName
	                FROM sys.foreign_keys AS f
	                INNER JOIN sys.foreign_key_columns AS fc
	                ON f.OBJECT_ID = fc.constraint_object_id) fk on fk.TableName = c.TABLE_NAME and fk.ColumnName = c.COLUMN_NAME   
                left outer join sys.identity_columns i on OBJECT_NAME(i.[object_id]) = c.TABLE_NAME and i.name = c.COLUMN_NAME     
                left outer join INFORMATION_SCHEMA.[COLUMNS] fks on fks.TABLE_NAME = fk.ReferenceTableName and fks.COLUMN_NAME = fk.ReferenceColumnName
                left outer join (
                	select distinct cu.[TABLE_NAME], cu.[COLUMN_NAME], 'YES' [FK_IS_UNIQUE] from INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
					left outer join INFORMATION_SCHEMA.TABLE_CONSTRAINTS cs on cs.CONSTRAINT_NAME = cu.CONSTRAINT_NAME 
					where cs.CONSTRAINT_TYPE in ('UNIQUE', 'PRIMARY KEY')
					) uni on uni.TABLE_NAME = fk.ReferenceTableName and uni.COLUMN_NAME = fk.ReferenceColumnName
                where c.TABLE_NAME = '" + TableName + "' order by ORDINAL_POSITION";
            DataTable dt = db.GetDataSet(sql).Tables[0];

            sql = @"select 
                t.name as TableWithForeignKey, fk.constraint_column_id as FK_PartNo, d.name as ForeignKeyColumn, c.name as LocalKeyColumn
                from sys.foreign_key_columns as fk
                inner join sys.tables as t on fk.parent_object_id = t.object_id
                inner join sys.columns as c on fk.parent_object_id = c.object_id and fk.parent_column_id = c.column_id
                inner join sys.columns as d on d.[object_id] = fk.referenced_object_id and d.column_id = fk.referenced_column_id                
                where fk.referenced_object_id = (select object_id from sys.tables where name = '" + TableName + "')";
            DataTable dtfk = db.GetDataSet(sql).Tables[0];


            /////////////////////////////////////////////
            //Carga columnas de la base
            EntidadBase = string.Empty;
            EntidadBase = System.IO.File.ReadAllText(@"..\..\Templates\EntityBase.cs");
            foreach (DataRow dr in dt.AsEnumerable())
            {
                Columna c = new Columna();
                c.Nombre = dr["COLUMN_NAME"].ToString();
                if (c.Nombre == "EstadoId") tablaConColumnaEstadoId = true;
                c.NroOrden = Convert.ToInt32(dr["ORDINAL_POSITION"].ToString());
                c.Tipo = ConvierteTipos(dr["DATA_TYPE"].ToString(), dr["IS_NULLABLE"].ToString());
                c.isPk = dr["isPK"].ToString() == "1";
                if (dr["isIdentity"] is DBNull) c.Identity = false;
                else c.Identity = Convert.ToBoolean(dr["isIdentity"]);
                c.FK_Table = dr["FK_Table"].ToString();
                c.FK_Column = dr["FK_Column"].ToString();
                if ((dr["FK_IS_NULLABLE"] is DBNull) || (dr["FK_IS_NULLABLE"].ToString() == "NO")) c.FK_isNullable = false;
                else c.FK_isNullable = Convert.ToBoolean(dr["FK_IS_NULLABLE"]);
                if (dr["FK_IS_UNIQUE"] is DBNull) c.FK_isUnique = false;
                else c.FK_isUnique = true;
                c.isNullable = !(dr["IS_NULLABLE"].ToString() == "NO") ;
                if (dr["COLUMN_DEFAULT"] is DBNull) ;
                else
                {
                    c.Column_Default = c.Nombre + " = ";
                    if (c.Tipo == "decimal") c.Column_Default += "[decimal] ";
                    //if (c.Tipo == "DateTime" && dr["COLUMN_DEFAULT"].ToString() == "(getdate())") c.Column_Default += "DateTime.Now";
                    //if (c.Tipo == "string") c.Column_Default += dr["COLUMN_DEFAULT"].ToString();
                    c.Column_Default += dr["COLUMN_DEFAULT"].ToString();
                }
                if (dr["CHARACTER_MAXIMUM_LENGTH"] is DBNull) ;
                else c.MaxLength = Convert.ToInt32(dr["CHARACTER_MAXIMUM_LENGTH"]);
                columnas.Add(c);
            }

            ////////////////////////
            //Genera propiedades
            string propiedades = string.Empty;
            string maximos = string.Empty;
            string stringify = "\t\tpublic override string ToString() " + CR + "\t\t{" + CR + "\t\t\t" + "return \"\\r\\n \" + ";
            foreach (var c in columnas)
            {
                stringify += CR + "\t\t\t\"" + c.Nombre + ": \" + " + c.Nombre + ".ToString() + \"\\r\\n \" + ";
                if (Ignorar(c.Tipo)) continue;
                propiedades += "\t\tpublic " + c.Tipo + " " + c.Nombre + " { get; set; }" + CR;
                if (c.Tipo == "string" && c.MaxLength != -1)
                maximos += "\t\t\tpublic static int " + c.Nombre + " { get; set; } = " + c.MaxLength + ";" + CR;
            }
            stringify = stringify.Substring(0, stringify.Length - 2);
            stringify += ";" + CR + "\t\t}";
            propiedades += CR + stringify;
            EntidadBase = EntidadBase.Replace("<TableName>", TableName);
            EntidadBase = EntidadBase.Replace("<properties>", propiedades);
            //EntidadBase = EntidadBase.Replace("<MAXLENGTH>", maximos); los pongo en el operador

            //Genera defaults de columnas
            string defaults = string.Empty;
            foreach (var c in columnas)
            {
                if (Ignorar(c.Tipo)) continue;
                if (c.Column_Default == null) ;
                else defaults += CR + "\t\t\t" + c.Column_Default + ";";
            }
            defaults = defaults.Replace("(", string.Empty);
            defaults = defaults.Replace(")", string.Empty);
            defaults = defaults.Replace("'", "\"");
            defaults = defaults.Replace("[", "(");
            defaults = defaults.Replace("]", ")");
            EntidadBase = EntidadBase.Replace("<DEFAULT>", defaults);

            //Genera constructor
            string identityName = string.Empty;
            foreach (var col in columnas) if (col.Identity) identityName = col.Nombre;
            EntidadBase = EntidadBase.Replace("<identity>", identityName);

            //Agrego un metodo por cada FK
            string metodosFK = string.Empty;
            foreach (var c in columnas)
            {
                //Console.WriteLine("\tColumna: " + c.Nombre);
                if (Ignorar(c.Tipo)) continue;
                if (c.FK_Table != string.Empty)
                {
                    if ((!c.isNullable) && (c.FK_isUnique))  //fk obligatoria y relacion a un solo registro en la fk_table
                    {
                        metodosFK += "\t\tpublic " + c.FK_Table + " GetRelated" + /*c.FK_Table +*/ c.Nombre + "()" + CR;
                        metodosFK += "\t\t{" + CR;
                        metodosFK += "\t\t\t" + c.FK_Table + " " + NombreVariable(c.FK_Table) + " = " + c.FK_Table + "Operator.GetOneByIdentity(" + c.Nombre + ");" + CR;
                        metodosFK += "\t\t\treturn " + NombreVariable(c.FK_Table) + ";" + CR;
                        metodosFK += "\t\t}" + CR + CR;
                    }
                    if ((c.isNullable) && (c.FK_isUnique))  //fk opcional y relacion a un solo registro en la fk_table
                    {
                        metodosFK += "\t\tpublic " + c.FK_Table + " GetRelated" + /*c.FK_Table +*/ c.Nombre + "()" + CR;
                        metodosFK += "\t\t{" + CR;
                        metodosFK += "\t\t\tif (" + c.Nombre + " != null)" + CR;
                        metodosFK += "\t\t\t{" + CR;
                        metodosFK += "\t\t\t\t" + c.FK_Table + " " + NombreVariable(c.FK_Table) + " = " + c.FK_Table + "Operator.GetOneByIdentity(" + c.Nombre + " ?? 0);" + CR;
                        metodosFK += "\t\t\t\treturn " + NombreVariable(c.FK_Table) + ";" + CR;
                        metodosFK += "\t\t\t}" + CR;
                        metodosFK += "\t\t\treturn null;" + CR;
                        metodosFK += "\t\t}" + CR + CR;
                    }
                    if ((!c.isNullable) && !(c.FK_isUnique))  //fk obligatoria y relacion a varios registros en la fk_table
                    {
                        metodosFK += "\t\tpublic List<" + c.FK_Table + "> GetRelated" + /*c.FK_Table +*/ c.Nombre + "List()" + CR;
                        metodosFK += "\t\t{" + CR;
                        metodosFK += "\t\t\tList<" + c.FK_Table + "> " + NombreVariable(c.FK_Table) + "List = " + c.FK_Table + "Operator.XXX();" + CR;
                        metodosFK += "\t\t\treturn " + NombreVariable(c.FK_Table) + "List;" + CR;
                        metodosFK += "\t\t}" + CR + CR;
                    }
                }
            }
            EntidadBase = EntidadBase.Replace("<FK>", metodosFK);

            //Agrego un metodo por cada foreign key de otras tablas con esta
            string metodosFKR = string.Empty;

            //repeticiones guarda las columnas y si esta repetida el GetRelated + tname se duplica y da error, entonces agrego el adicional
            List<string> repeticiones = new List<string>();
            foreach (DataRow row in dtfk.Rows) repeticiones.Add(row["TableWithForeignKey"].ToString());

            foreach (DataRow row in dtfk.Rows)
            {
                string tname = row["TableWithForeignKey"].ToString();
                string localColumn = row["LocalKeyColumn"].ToString();
                string fkColumn = row["ForeignKeyColumn"].ToString();
                string adicional = string.Empty;
                int first = repeticiones.IndexOf(tname);
                int second = repeticiones.LastIndexOf(tname);
                if (first != second) adicional = localColumn.Replace(fkColumn, "");
                metodosFKR += "\t\tpublic List<" + tname + "> GetRelated" + Plural(tname) + adicional + "()" + CR;
                metodosFKR += "\t\t{" + CR;
                metodosFKR += "\t\t\treturn " + tname + "Operator.GetAll().Where(x => x." + localColumn + " == " + fkColumn + ").ToList();" + CR;

                metodosFKR += "\t\t}" + CR;
                
            }
            EntidadBase = EntidadBase.Replace("<FKR>", metodosFKR);

            string cc = "\t\tpublic static bool CanBeNull(string colName)" + CR;
            cc += "\t\t{" + CR;
            cc += "\t\t\tswitch (colName) " + CR;
            cc += "\t\t\t{" + CR;
            foreach (var c in columnas)
            {
                cc += "\t\t\t\tcase \"" + c.Nombre + "\": return " + (c.isNullable ? "true" : "false") + ";" + CR;
            }
            cc += "\t\t\t\tdefault: return false;" + CR;
            cc += "\t\t\t}" + CR;
            cc += "\t\t}";

            EntidadBase = EntidadBase.Replace("<BENULL>", cc);

            ////////////////////////
            //Genera operadores
            OperatorBase = string.Empty;
            OperatorBase = System.IO.File.ReadAllText(@"..\..\Templates\OperatorBase.cs");
            if (tablaConColumnaEstadoId)
            {
                string proc = "\t\tpublic static List<<TableName>> GetAllEstado1()" + CR;
                proc += "\t\t{" + CR + "\t\t\treturn GetAll().Where(x => x.EstadoId == 1).ToList();" + CR + "\t\t}" + CR;
                proc += "\t\tpublic static List<<TableName>> GetAllEstadoNot1()" + CR;
                proc += "\t\t{" + CR + "\t\t\treturn GetAll().Where(x => x.EstadoId != 1).ToList();" + CR + "\t\t}" + CR;
                proc += "\t\tpublic static List<<TableName>> GetAllEstadoN(int estado)" + CR;
                proc += "\t\t{" + CR + "\t\t\treturn GetAll().Where(x => x.EstadoId == estado).ToList();" + CR + "\t\t}" + CR;
                proc += "\t\tpublic static List<<TableName>> GetAllEstadoNotN(int estado)" + CR;
                proc += "\t\t{" + CR + "\t\t\treturn GetAll().Where(x => x.EstadoId != estado).ToList();" + CR + "\t\t}" + CR;
                OperatorBase = OperatorBase.Replace("<GetAllEstado1>", proc);
            }
            else OperatorBase = OperatorBase.Replace("<GetAllEstado1>", "");
            string varName = NombreVariable(TableName);
            OperatorBase = OperatorBase.Replace("<TableName>", TableName);
            OperatorBase = OperatorBase.Replace("<identity>", identityName);
            OperatorBase = OperatorBase.Replace("<varName>", varName);
            OperatorBase = OperatorBase.Replace("<MaxLength>", maximos);
            

        }

        public static bool Ignorar(string tipo)
        {
            //if (tipo == "ignorar") return true;
            return false;
        }

        public static string ConvierteTipos(string tipo, string esNull)
        {
            switch (tipo)
            {
                case "int": return "int" + (esNull == "YES" ? "?" : ""); 
                case "bigint": return "long" + (esNull == "YES" ? "?" : "");
                case "bit": return "int" + (esNull == "YES" ? "?" : "");
                case "varchar":return "string";
                case "nvarchar": return "string";
                case "char": return "string";
                case "nchar": return "string";
                case "smallint": return "int" + (esNull == "YES" ? "?" : ""); 
                case "datetime": return "DateTime" + (esNull == "YES" ? "?" : "");
                case "date": return "DateTime" + (esNull == "YES" ? "?" : "");
                case "smalldatetime": return "DateTime" + (esNull == "YES" ? "?" : "");
                case "text": return "string";
				case "decimal": return "decimal";
                case "float": return "decimal";
                case "real": return "decimal";
                case "money": return "decimal";
                case "numeric": return "decimal";

                case "varbinary": return "object";

            }
            return "DB_TYPE_DESCONOCIDO";
        }

        public static string GetComilla(string tipo)
        {
            switch (tipo)
            {
                case "int": return ""; 
                case "bigint": return ""; 
                case "bit": return ""; 
                case "varchar": return "'"; 
                case "nvarchar": return "'";
                case "char": return "'";
                case "nchar": return "'";
                case "smallint": return ""; 
                case "datetime": return "'";
				case "text": return "'";
				case "decimal": return "";

            }
            return "DB_TYPE_DESCONOCIDO";
        }

        public static string NombreVariable(string tabla)
        {
            if (Char.IsUpper(tabla[0])) return tabla[0].ToString().ToLower() + tabla.Substring(1, tabla.Length - 1);
            else return "_" + tabla;
        }

        public static string Plural(string s)
        {
            string last = s.Substring(s.Length - 1, 1);
            if (last == "a" || last == "e" || last == "i" || last == "o" || last == "u") return s + "s";

            else return s + "es";
        }
    }

}
