using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace LibDB2
{

    public class DB
    {
        readonly log4net.ILog logDB = log4net.LogManager.GetLogger("logDB");
        public void LOGIni(string sql)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            logDB.Debug(timestamp);
            logDB.Debug(sql);
        }
        public void LOGFin()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            logDB.Debug(timestamp);
        }


        public static string connection_string {
            get { return DesencriptaPasswordDeConnectionString(System.Web.HttpContext.Current.Session["WebApplicationConStr"].ToString()); } 
        }
        public static int command_timeout { get; set; } = -1; //en segundos .Un command timeout de -1 indica que se use el default. 0 es infinito
        public static bool deshabilita_encripcion { get; set; } = false;  // por default espera claves encriptadas
        public string prints_sql { get; set; } = string.Empty;
        public string command_sql { get; set; } = string.Empty;

        private ListDictionary openedTransactions = new ListDictionary(); //guardo las transacciones activas

        public DB()
        { 
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(@"D:\FFF\Work\WebApplication\Web\WebApplication\Web.config")); 
        }  // si se usa este contructor usas la connection_string que tenga hasta el momento

        public DB(string connectionString)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(@"D:\FFF\Work\WebApplication\Web\WebApplication\Web.config"));
            //connection_string = DesencriptaPasswordDeConnectionString(connectionString);
        }
        public DB(string connectionString, int timeoutInSecs)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(@"D:\FFF\Work\WebApplication\Web\WebApplication\Web.config"));
            //connection_string = DesencriptaPasswordDeConnectionString(connectionString);
            command_timeout = timeoutInSecs;
        }
        public SqlConnection GetSqlConnection()
        {
            //connection_string = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            SqlConnection sqlconn = new SqlConnection(connection_string);
            return sqlconn;
        }
        public SqlTransaction BeginTransaction()
        {
            SqlConnection conn = GetSqlConnection();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            openedTransactions.Add(trans, conn);
            return trans;
        }
        public void CommitTransaction(SqlTransaction trans)
        {
            trans.Commit();
            (openedTransactions[trans] as SqlConnection).Close();
            openedTransactions.Remove(trans);
        }
        public void RollbackTransaction(SqlTransaction trans)
        {
            trans.Rollback();
            (openedTransactions[trans] as SqlConnection).Close();
            openedTransactions.Remove(trans);
        }
        public object ExecuteScalar(string sql, params SqlParameter[] args)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                CreateParameters(args, cmd);
                if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
                LOGIni(sql);
                object returnValue = cmd.ExecuteScalar();
                LOGFin();
                conn.Close();
                return returnValue;
            }
        }
        public int ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
                LOGIni(sql);
                int rows = cmd.ExecuteNonQuery();
                LOGFin();
                conn.Close();
                return rows;
            }
        }
        public object ExecuteNonQuery(string sql, params SqlParameter[] args)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                CreateParameters(args, cmd);
                if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
                LOGIni(sql);
                object returnValue = cmd.ExecuteNonQuery();
                LOGFin();
                conn.Close();
                return returnValue;
            }
        }
        public int ExecuteNonQuery(SqlTransaction trans, string sql)
        {
            SqlConnection conn = openedTransactions[trans] as SqlConnection;
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
            LOGIni(sql);
            int rows = cmd.ExecuteNonQuery();
            LOGFin();
            return rows;
        }
        public object ExecuteScalar(string sql)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                object returnValue;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
                LOGIni(sql);
                returnValue = cmd.ExecuteScalar();
                LOGFin();
                conn.Close();
                if (returnValue is null) return null;
                return returnValue;
            }
        }
        public object ExecuteScalar(SqlTransaction trans, string sql)
        {
            SqlConnection conn = openedTransactions[trans] as SqlConnection;
            object returnValue;
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
            LOGIni(sql);
            returnValue = cmd.ExecuteScalar();
            LOGFin();
            if (returnValue is null) return null;
            return returnValue;
        }
        public string ExecuteScalarString(string sql)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                object returnValue;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
                LOGIni(sql);
                returnValue = cmd.ExecuteScalar();
                LOGFin();
                conn.Close();
                if (returnValue is null) return null;
                return returnValue.ToString();
            }
        }
        public string ExecuteScalarString(SqlTransaction trans, string sql)
        {
            SqlConnection conn = openedTransactions[trans] as SqlConnection;
            object returnValue;
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
            LOGIni(sql);
            returnValue = cmd.ExecuteScalar();
            LOGFin();
            if (returnValue is null) return null;
            return returnValue.ToString();
        }
        public int? ExecuteScalarInt(string sql)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                object returnValue;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
                LOGIni(sql);
                returnValue = cmd.ExecuteScalar();
                LOGFin();
                conn.Close();
                if (returnValue is null) return null;
                if (returnValue.GetType() == typeof(DBNull)) return null;
                return Convert.ToInt32(returnValue);
            }
        }
        public int? ExecuteScalarInt(SqlTransaction trans, string sql)
        {
            SqlConnection conn = openedTransactions[trans] as SqlConnection;
            object returnValue;
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
            LOGIni(sql);
            returnValue = cmd.ExecuteScalar();
            LOGFin();
            if (returnValue is null) return null;
            if (returnValue.GetType() == typeof(DBNull)) return null;
            return Convert.ToInt32(returnValue);

        }
        public double? ExecuteScalarDouble(string sql)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                object returnValue;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
                LOGIni(sql);
                returnValue = cmd.ExecuteScalar();
                LOGFin();
                conn.Close();
                if (returnValue is null) return null;
                if (returnValue.GetType() == typeof(DBNull)) return null;
                return Convert.ToDouble(returnValue);
            }
        }
        public double? ExecuteScalarDouble(SqlTransaction trans, string sql)
        {
            SqlConnection conn = openedTransactions[trans] as SqlConnection;
            object returnValue;
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
            LOGIni(sql);
            returnValue = cmd.ExecuteScalar();
            LOGFin();
            if (returnValue is null) return null;
            if (returnValue.GetType() == typeof(DBNull)) return null;
            return Convert.ToDouble(returnValue);
            
        }
        public bool? ExecuteScalarBool(string sql)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                object returnValue;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
                LOGIni(sql);
                returnValue = cmd.ExecuteScalar();
                LOGFin();
                conn.Close();
                if (returnValue is null) return null;
                if (returnValue.GetType() == typeof(DBNull)) return null;
                return Convert.ToBoolean(returnValue);
            }
        }
        public bool? ExecuteScalarBool(SqlTransaction trans, string sql)
        {
            SqlConnection conn = openedTransactions[trans] as SqlConnection;
            object returnValue;
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            if (command_timeout > -1) cmd.CommandTimeout = command_timeout;
            LOGIni(sql);
            returnValue = cmd.ExecuteScalar();
            LOGFin();
            if (returnValue is null) return null;
            if (returnValue.GetType() == typeof(DBNull)) return null;
            return Convert.ToBoolean(returnValue);
        }
        public DataSet GetDataSet(string sql)
        {
            LOGIni(sql);
            DataSet ds = new DataSet();
            using (SqlConnection conn = GetSqlConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                if (command_timeout > -1) da.SelectCommand.CommandTimeout = command_timeout;
                da.Fill(ds);
                LOGFin();
                return ds;
            }
        }
        public DataSet GetDataSet(SqlTransaction trans, string sql)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = openedTransactions[trans] as SqlConnection;
            SqlCommand cmd = new SqlCommand(sql, conn, trans);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            LOGIni(sql);
            if (command_timeout > -1) da.SelectCommand.CommandTimeout = command_timeout;
            da.Fill(ds);
            LOGFin();
            return ds;
        }
        public DataSet GetDataSet(string sql, CommandType cmdType, params SqlParameter[] args)
        {
            using (SqlConnection sqlConn = GetSqlConnection())
            {
                sqlConn.Open();
                using (SqlCommand sqlComm = new SqlCommand(sql, sqlConn))
                {
                    sqlComm.CommandType = cmdType;
                    CreateParameters(args, sqlComm);
                    SqlDataAdapter adap = new SqlDataAdapter(sqlComm);
                    DataSet ds = new DataSet();
                    LOGIni(sql);
                    adap.Fill(ds);
                    LOGFin();
                    return ds;
                }
            }
        }
        public DataSet GetDataSet(SqlTransaction trans, string sql, CommandType cmdType, params SqlParameter[] args)
        {
            SqlConnection conn = openedTransactions[trans] as SqlConnection;
            using (SqlCommand sqlComm = new SqlCommand(sql, conn, trans))
            {
                sqlComm.CommandType = cmdType;
                CreateParameters(args, sqlComm);
                SqlDataAdapter adap = new SqlDataAdapter(sqlComm);
                DataSet ds = new DataSet();
                LOGIni(sql);
                adap.Fill(ds);
                LOGFin();
                return ds;
            }
        }
        public DataTable GetDataTable(string sql, CommandType cmdType, params SqlParameter[] args)
        {
            return this.GetDataSet(sql, cmdType, args).Tables[0];
        }
        public DataTable GetDataTable(SqlTransaction trans, string sql, CommandType cmdType, params SqlParameter[] args)
        {
            return this.GetDataSet(trans, sql, cmdType, args).Tables[0];
        }
        public DataSet GetDataSetWithDebug(string sql, CommandType cmdType, params SqlParameter[] args)
        {
            int nroTabla = 1;
            prints_sql = string.Empty;
            command_sql = "\n--------------------------------------------------------------------------------\n";
            using (SqlConnection conn = GetSqlConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandTimeout = command_timeout;
                    foreach (SqlParameter pp in args)
                    {
                        if (pp.Value != null && pp.DbType.ToString() == "Object")
                        {
                            DataTable dt = (DataTable)pp.Value;
                            command_sql += "declare @Table" + nroTabla.ToString() + "\n";
                            command_sql += "insert into @Table" + nroTabla++.ToString() + " values ";
                            foreach (DataRow dr in dt.Rows)
                            {
                                command_sql += "\n('";
                                foreach (var dc in dr.ItemArray)
                                {
                                    command_sql += dc.ToString() + "', '";
                                }
                                command_sql = command_sql.Substring(0, command_sql.Length - 4);
                                command_sql += "'),";
                            }
                            command_sql = command_sql.Substring(0, command_sql.Length - 1);
                            command_sql += "\n\n";
                        }
                    }
                    command_sql += "exec " + sql + " \n";
                    nroTabla = 1;
                    foreach (SqlParameter pp in args)
                    {
                        string comilla = "";

                        if (pp.Value != null && pp.DbType.ToString() == "String") comilla = "'";
                        command_sql += pp.ParameterName + " = " + comilla;
                        if (pp.Value != null && pp.DbType.ToString() == "Object") command_sql += "@Table" + nroTabla++.ToString() + ", \n";
                        else command_sql += pp.Value + comilla + ", \n";
                    }
                    command_sql = command_sql.Substring(0, command_sql.Length - 3);
                    command_sql += "\n";
                    CreateParameters(args, cmd);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    conn.InfoMessage += delegate (object sender, SqlInfoMessageEventArgs e)
                    {
                        prints_sql += "\n" + e.Message;
                    };
                    prints_sql = prints_sql + "--------------------------------------------------------------------------------";

                    adap.Fill(ds);

                    return ds;

                }
            }
        }
        public DataSet GetDataSetWithDebug(SqlTransaction trans, string sql, CommandType cmdType, params SqlParameter[] args)
        {
            int nroTabla = 1;
            prints_sql = string.Empty;
            command_sql = "\n--------------------------------------------------------------------------------\n";
            SqlConnection conn = openedTransactions[trans] as SqlConnection;
            using (SqlCommand sqlComm = new SqlCommand(sql, conn, trans))
            {
                sqlComm.CommandType = cmdType;
                sqlComm.CommandTimeout = command_timeout;
                foreach (SqlParameter pp in args)
                {
                    if (pp.Value != null && pp.DbType.ToString() == "Object")
                    {
                        DataTable dt = (DataTable)pp.Value;
                        command_sql += "declare @Table" + nroTabla.ToString() + "\n";
                        command_sql += "insert into @Table" + nroTabla++.ToString() + " values ";
                        foreach (DataRow dr in dt.Rows)
                        {
                            command_sql += "\n('";
                            foreach (var dc in dr.ItemArray)
                            {
                                command_sql += dc.ToString() + "', '";
                            }
                            command_sql = command_sql.Substring(0, command_sql.Length - 4);
                            command_sql += "'),";
                        }
                        command_sql = command_sql.Substring(0, command_sql.Length - 1);
                        command_sql += "\n\n";
                    }
                }
                command_sql += "exec " + sql + " \n";
                nroTabla = 1;
                foreach (SqlParameter pp in args)
                {
                    string comilla = "";

                    if (pp.Value != null && pp.DbType.ToString() == "String") comilla = "'";
                    command_sql += pp.ParameterName + " = " + comilla;
                    if (pp.Value != null && pp.DbType.ToString() == "Object") command_sql += "@Table" + nroTabla++.ToString() + ", \n";
                    else command_sql += pp.Value + comilla + ", \n";
                }
                command_sql = command_sql.Substring(0, command_sql.Length - 3);
                command_sql += "\n";
                CreateParameters(args, sqlComm);
                SqlDataAdapter adap = new SqlDataAdapter(sqlComm);
                DataSet ds = new DataSet();
                conn.InfoMessage += delegate (object sender, SqlInfoMessageEventArgs e)
                {
                    prints_sql += "\n" + e.Message;
                };
                prints_sql = prints_sql + "--------------------------------------------------------------------------------";
                LOGIni(sql);
                adap.Fill(ds);
                LOGFin();
                return ds;
            }
            
        }
        public DataTable GetDataTableWithDebug(string sql, CommandType cmdType, params SqlParameter[] args)
        {
            return GetDataSetWithDebug(sql, cmdType, args).Tables[0];
        }
        public DataTable GetDataTableWithDebug(SqlTransaction trans, string sql, CommandType cmdType, params SqlParameter[] args)
        {
            return GetDataSetWithDebug(trans, sql, cmdType, args).Tables[0] ;
        }


        






        private void CreateParameters(SqlParameter[] args, SqlCommand sqlComm)
        {
            foreach (SqlParameter param in args)
            {
                if (param.Value == null)
                {
                    sqlComm.Parameters.Add(new SqlParameter(param.ParameterName, DBNull.Value));
                }
                else if (param.Value is string && (String.IsNullOrEmpty(((string)param.Value).Replace("%", "").Trim()) || (string)param.Value == "-1"))
                {
                    sqlComm.Parameters.Add(new SqlParameter(param.ParameterName, DBNull.Value));
                }
                else
                {
                    sqlComm.Parameters.Add(param);
                }
            }
        }
        protected static string DesencriptaPasswordDeConnectionString(string connStr)
        {
            if (!deshabilita_encripcion)
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connStr);
                builder.Password = Encriptador.DecoPwd(builder.Password);
                return builder.ConnectionString;
            }
            else return connStr;
        }
    }
}
