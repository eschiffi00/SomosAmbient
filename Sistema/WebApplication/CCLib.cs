using DbEntidades.Entities;
using DbEntidades.Operators;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public static class CCLib
    {
        public static void Assert(bool b, string mensaje = "")
        {
            if (mensaje == "") mensaje = "Error " + new Random().ToString();
            if (!b) throw new Exception(mensaje);
        }
        public static int GetColumnIndexByHeaderText(GridView aGridView, String ColumnText)
        {
            Assert(aGridView.HeaderRow.Cells.Count > 0, "No se encontró la columna");
            TableCell Cell;
            for (int Index = 0; Index < aGridView.HeaderRow.Cells.Count; Index++)
            {
                Cell = aGridView.HeaderRow.Cells[Index];
                if (Cell.Text.ToString() == ColumnText)
                    return Index;
            }
            return -1;
        }
        public static DataTable GetDataTableFromGridView(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable
            if (dtg.HeaderRow != null)
            {
                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static DataTable ClassListToDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public static List<T> DataTableToClassList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            var t = propertyInfo.PropertyType;
                            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                            {
                                try
                                {
                                    if (row[prop.Name] != null)
                                    {
                                        t = Nullable.GetUnderlyingType(t);
                                        propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], t), null);
                                    }
                                    else
                                        propertyInfo.SetValue(obj, null, null);
                                }
                                catch (System.ArgumentException)
                                {
                                    propertyInfo.SetValue(obj, null, null);
                                }
                            }
                            else
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }
        public static SqlDbType ConvertSystemTypeToSqlDbType(System.Type theType)
        {
            SqlParameter p1;
            System.ComponentModel.TypeConverter tc;
            p1 = new SqlParameter();
            tc = System.ComponentModel.TypeDescriptor.GetConverter(p1.DbType);
            if (tc.CanConvertFrom(theType))
                p1.DbType = (DbType)tc.ConvertFrom(theType.Name);
            else
                try
                {
                    p1.DbType = (DbType)tc.ConvertFrom(theType.Name);
                }
                catch (Exception)
                { }
            return p1.SqlDbType;
        }
        public static int GetGridviewColumnIndexByName(GridView grid, string name)
        {
            foreach (DataControlField col in grid.Columns)
            {
                if (col.HeaderText.ToLower().Trim() == name.ToLower().Trim())
                {
                    return grid.Columns.IndexOf(col);
                }
            }
            return -1;
        }
        public static string GetNumberAfterTagInString(string s, string tag, int coincidenciaNro = 1)
        {
            int count = 0;
            while (count < coincidenciaNro)
            {
                int i = s.IndexOf(tag);
                if (i < 0) return string.Empty;
                s = s.Substring(i + 1, s.Length - i - 1);
                count++;
            }
            return Regex.Match(s, @"\d+").Value;
        }
        public static string GetSiteRootUrl()
        {
            string protocol;

            if (HttpContext.Current.Request.IsSecureConnection)
                protocol = "https";
            else
                protocol = "http";

            StringBuilder uri = new StringBuilder(protocol + "://");

            string hostname = HttpContext.Current.Request.Url.Host;

            uri.Append(hostname);

            int port = HttpContext.Current.Request.Url.Port;

            if (port != 80 && port != 443)
            {
                uri.Append(":");
                uri.Append(port.ToString());
            }

            return uri.ToString();
        }
        public static string ReadAllFile(string fpath)
        {
            string strContents = "";
            StreamReader objReader;
            try
            {
                objReader = new StreamReader(fpath);
                strContents = objReader.ReadToEnd();
                objReader.Close();
                return strContents;
            }
            catch { }
            return string.Empty;
        }
        public static string GetRevisionFromFile(string fpath)
        {
            string strContents = "";
            StreamReader objReader;
            string aRevData;
            string repo;
            try
            {
                //string fpath = Server.MapPath(ResolveUrl("~/Version"));
                objReader = new StreamReader(fpath);
                strContents = objReader.ReadToEnd();
                objReader.Close();

                aRevData = strContents.Split('\r')[0];
                //repo = strContents.Split('\r')[6].Split('/').Reverse().Take(2).Last();
                repo = "SomosAmbient " + strContents.Split('\r')[6].Split('/').Last();
                //string cnn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                //SqlConnectionStringBuilder s = new SqlConnectionStringBuilder(cnn);
                repo += "(" + aRevData + ") - " ;
                repo += " build 0.1." + aRevData;
            }

            catch (Exception)
            {
                aRevData = " no disponible.";
                repo = "";
            }
            return repo;
        }
        public static DateTime GetLinkerTime()
        {
            var filePath = Assembly.GetExecutingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;

            var buffer = new byte[2048];

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                stream.Read(buffer, 0, 2048);

            var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

            var localTime = linkTimeUtc.AddHours(-3);
            return localTime;
        }
        public static byte[] GetBufferDocumento(Stream content, long len)
        {
            byte[] buffer = new byte[len];
            Stream s = content;
            s.Read(buffer, 0, buffer.Length);
            return buffer;
        }
        public static void ExportarDataTable(DataTable dt, string tableName, string pathName, string fmt = "")
        {
            string[] tipo = fmt.Split(',');
            List<string> columnas = new List<string>();
            for (int i = 0; i < dt.Columns.Count; i++) columnas.Add(dt.Columns[i].ColumnName.ToString());
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet(tableName);
            var headerRow = sheet.CreateRow(0);

            //titulos del excel
            IFont headerFont = workbook.CreateFont();
            headerFont.IsBold = true;
            //headerFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.SetFont(headerFont);
            for (int i = 0; i < columnas.Count; i++)
            {
                var cell = headerRow.CreateCell(i);
                cell.SetCellValue(columnas[i]);
                //cell.CellStyle.SetFont(headerFont);
                cell.CellStyle = cellStyle;
                //sheet.AutoSizeColumn(i);
            }
            //filas
            IFont normalFont = workbook.CreateFont();
            //normalFont.IsBold = true;
            //normalFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            IDataFormat newDataFormatDate = workbook.CreateDataFormat();
            var styleDate = workbook.CreateCellStyle();
            styleDate.DataFormat = newDataFormatDate.GetFormat("dd/MM/yyyy");
            IDataFormat newDataFormatDateTime = workbook.CreateDataFormat();
            var styleDateTime = workbook.CreateCellStyle();
            styleDateTime.DataFormat = newDataFormatDateTime.GetFormat("dd/MM/yyyy HH:mm:ss");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var rowIndex = i + 1;
                var row = sheet.CreateRow(rowIndex);
                for (int j = 0; j < columnas.Count; j++)
                {
                    var cell = row.CreateCell(j);
                    string s = dt.Rows[i][columnas[j]].ToString();
                    if (fmt != "")
                    {
                        try
                        {

                            if (tipo[j] == "int") cell.SetCellValue(Convert.ToInt32(s));
                            else if (tipo[j] == "double") cell.SetCellValue(Convert.ToDouble(s));
                            else if (tipo[j] == "date")  {cell.SetCellValue(DateTime.Parse(s)); cell.CellStyle = styleDate; }
                            else if (tipo[j] == "datetime")  {cell.SetCellValue(DateTime.Parse(s)); cell.CellStyle = styleDateTime; }
                            else cell.SetCellValue(s);
                        }
                        catch (Exception ee)
                        {
                            cell.SetCellValue(s);
                        }
                    }
                    else cell.SetCellValue(s);

                    cell.CellStyle.SetFont(normalFont);
                }
            }

            var stream = new MemoryStream();
            workbook.Write(stream);

            Guid guid = Guid.NewGuid();
            string FilePath = pathName;

            //Write to file using file stream  
            FileStream file = new FileStream(FilePath, FileMode.CreateNew, FileAccess.Write);
            stream.WriteTo(file);
            file.Close();
            stream.Close();
        }
#region mail
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        //public static string GeneraTokenVerificacion(string edificio, Cliente cliente)
        //{
        //    string cadena = edificio + ";" + cliente.ClienteId.ToString() + ";" + cliente.Email;
        //    string token = new CryptorEngine().Encrypt(cadena, true);
        //    token = CCLib.Base64Encode(token);
        //    return token;
        //}
        public static string GeneraTokenImgVerificacion(string edificio, string emailLogId)
        {
            string imgToken = new CryptorEngine().Encrypt(edificio + ";" + emailLogId, true);
            imgToken = CCLib.Base64Encode(imgToken);
            return imgToken;
        }
        //public static string GeneraTokenPortal(string edificio, Cliente cliente)
        //{
        //    string cadena = edificio + ";" + cliente.ClienteId.ToString() + ";" + cliente.Email;
        //    string token = new CryptorEngine().Encrypt(cadena, true);
        //    token = CCLib.Base64Encode(token);
        //    return token;
        //}
        //public static void EnviaMailAltaClientePortal(string edificio, Cliente cliente, string body)
        //{
        //    //string cadena = edificio + ";" + cliente.ClienteId.ToString() + ";" + cliente.Email;
        //    //string token = new CryptorEngine().Encrypt(cadena, true);
        //    //token = CCLib.Base64Encode(token);

        //    //string t = c.Decrypt(s, true);
        //    string siteRoot = CCLib.GetSiteRootUrl();

        //    //registro el envio de mail
        //    //EmailLog mLog = new EmailLog();
        //    //mLog.FeEnviado = DateTime.Now;
        //    //mLog.Desde = ParametroOperator.GetParametroString("mail.FromAdress");
        //    //mLog.Destino = cliente.Email;
        //    //mLog.Subject = "CasaCampus - Ingreso al Portal";
        //    //mLog.Body = body;
        //    //mLog.ClienteId = cliente.ClienteId;
        //    //EmailLogOperator.Save(mLog);
        //    //string imgToken = new CryptorEngine().Encrypt(edificio + ";" + mLog.EmailLogId.ToString(), true);
        //    //imgToken = CCLib.Base64Encode(imgToken);

        //    //body = body.Replace("{Token}", GeneraTokenPortal(edificio, cliente));
        //    //body = body.Replace("{SiteRoot}", siteRoot);
        //    //body = body.Replace("{imgToken}", imgToken);
        //    //body = body.Replace("{Edificio}", edificio);

        //    //mLog.Body = body;
        //    //EmailLogOperator.Save(mLog);

        //    EnviaMailGeneral(cliente.Email, cliente.RazonSocial, "CasaCampus - Ingreso al Portal", body);

        //}
        //public static void EnviaMailParaVerificarCliente(string edificio, Cliente cliente, string body)
        //{
        //    //string cadena = edificio + ";" + cliente.ClienteId.ToString() + ";" + cliente.Email;
        //    //string token = new CryptorEngine().Encrypt(cadena, true);
        //    //token = CCLib.Base64Encode(token);

        //    //string t = c.Decrypt(s, true);


        //    //registro el envio de mail
        //    //EmailLog mLog = new EmailLog();
        //    //mLog.FeEnviado = DateTime.Now;
        //    //mLog.Desde = "robot.mensajes@gmail.com";
        //    //mLog.Destino = cliente.Email;
        //    //mLog.Subject = "CasaCampus - Verificación de Registro";
        //    //mLog.Body = body;
        //    //mLog.ClienteId = cliente.ClienteId;
        //    //EmailLogOperator.Save(mLog);

        //    //string imgToken = new CryptorEngine().Encrypt(edificio + ";" + mLog.EmailLogId.ToString(), true);
        //    //imgToken = CCLib.Base64Encode(imgToken);

        //    //string token = GeneraTokenVerificacion(edificio, cliente);
        //    //string imgToken = GeneraTokenImgVerificacion(edificio, mLog.EmailLogId.ToString());
        //    //string siteRoot = CCLib.GetSiteRootUrl();
        //    //body = body.Replace("{Token}", token);
        //    //body = body.Replace("{SiteRoot}", siteRoot);
        //    //body = body.Replace("{imgToken}", imgToken);

        //    //mLog.Body = body;
        //    //EmailLogOperator.Save(mLog);

        //    EnviaMailGeneral(cliente.Email, cliente.RazonSocial, "CasaCampus - Verificación de Registro", body);

        //}
        public static void EnviaMailOlvideClaveBondi(string edificio, string email, string body)
        {
            string token = new CryptorEngine().Encrypt(edificio + ";" + email, true);
            token = CCLib.Base64Encode(token);

            //string t = c.Decrypt(s, true);
            string siteRoot = CCLib.GetSiteRootUrl();

            //registro el envio de mail
            //EmailLog mLog = new EmailLog();
            //mLog.FeEnviado = DateTime.Now;
            //mLog.Desde = ParametroOperator.GetParametroString("mail.FromAdress");
            //mLog.Destino = email;
            //mLog.Subject = "CasaCampus - Recuperación Contraseña";
            //mLog.Body = body;
            //mLog.ClienteId = 0;
            //EmailLogOperator.Save(mLog);
            //string imgToken = new CryptorEngine().Encrypt(edificio + ";" + mLog.EmailLogId.ToString(), true);
            //imgToken = CCLib.Base64Encode(imgToken);

            //body = body.Replace("{Token}", token);
            //body = body.Replace("{SiteRoot}", siteRoot);
            //body = body.Replace("{imgToken}", imgToken);

            //mLog.Body = body;
            //EmailLogOperator.Save(mLog);

            EnviaMailGeneral(email, string.Empty, "SomosAmbient - Recuperación Contraseña", body);
        }

        /*
        public static void EnviaMailGeneral(string email, string nombre, string mailSubject, string mailBody)
        {
            //PARA QUE FINALMENTE ACEPTE MAILS DE VUELTA Y ESTO FUNCIONE TUVE QUE LOGUEARME A GMAIL E IR A https://www.google.com/settings/security/lesssecureapps
            //Y PONERLE Turn on

            //var mySettings = Properties.Settings.Default;
            //var mySettings = Properties.Settings1.Default;

            MailAddress fromAddress = new MailAddress(ParametroOperator.GetParametroString("mail.FromAdress"), ParametroOperator.GetParametroString("mail.DisplayName"));
            MailAddress toAddress = new MailAddress(email, nombre);
            string subject = mailSubject;
            string body = mailBody;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = ParametroOperator.GetParametroString("smtp.Host");
            smtp.Port = ParametroOperator.GetParametroInt("smtp.Port");
            smtp.EnableSsl = ParametroOperator.GetParametroString("smtp.EnableSsl") == "true" ? true : false;

            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(ParametroOperator.GetParametroString("mail.FromAdress"), 
                                                     Properties.Settings.Default.EmailPass);
            //smtp.UseDefaultCredentials = true; //si se pone en true o en false no anda mas. Hay que anularlo


            MailMessage message = new MailMessage(fromAddress, toAddress);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            //ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            if ((ParametroOperator.GetParametroString("HabilitarEnvioMail") == "Y") || 
                (ParametroOperator.GetParametroString("HabilitarEnvioMail") == "S"))    
                smtp.Send(message);
        }
        */

        public static void EnviaMailGeneral(string email, string nombre, string mailSubject, string mailBody)
        {
            string SMTP_SERVER = "http://schemas.microsoft.com/cdo/configuration/smtpserver";
            string SMTP_SERVER_PORT = "http://schemas.microsoft.com/cdo/configuration/smtpserverport";
            string SEND_USING = "http://schemas.microsoft.com/cdo/configuration/sendusing";
            string SMTP_USE_SSL = "http://schemas.microsoft.com/cdo/configuration/smtpusessl";
            string SMTP_AUTHENTICATE = "http://schemas.microsoft.com/cdo/configuration/smtpauthenticate";
            string SEND_USERNAME = "http://schemas.microsoft.com/cdo/configuration/sendusername";
            string SEND_PASSWORD = "http://schemas.microsoft.com/cdo/configuration/sendpassword";

            System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();

            mail.Fields[SMTP_SERVER] = ParametroOperator.GetParametroString("smtp.Host");
            mail.Fields[SMTP_SERVER_PORT] = ParametroOperator.GetParametroInt("smtp.Port");
            mail.Fields[SEND_USING] = 2;
            mail.Fields[SMTP_USE_SSL] = ParametroOperator.GetParametroString("smtp.EnableSsl") == "true" ? true : false;
            mail.Fields[SMTP_AUTHENTICATE] = 1;
            mail.Fields[SEND_USERNAME] = ParametroOperator.GetParametroString("mail.FromAdress");
            //mail.Fields[SEND_PASSWORD] = Properties.Settings.Default.EmailPass;

            mail.From = ParametroOperator.GetParametroString("mail.DisplayName") + " <" +ParametroOperator.GetParametroString("mail.FromAdress") + ">";
            mail.To = nombre + " <" + email + ">";
            mail.Subject = mailSubject;
            mail.Body = mailBody;
            mail.BodyFormat = System.Web.Mail.MailFormat.Html;

            if ((ParametroOperator.GetParametroString("HabilitarEnvioMail") == "Y") ||
                (ParametroOperator.GetParametroString("HabilitarEnvioMail") == "S"))
                System.Web.Mail.SmtpMail.Send(mail);
        }
        #endregion mail
    }
    public static class StringExtension
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
        public static string ToUpperFirstLetter(this string value)
        {
            if (value.Length == 0) return value;
            else if (value.Length == 1) return char.ToUpper(value[0]).ToString();
            else return char.ToUpper(value[0]) + value.Substring(1);
        }
        public static IEnumerable<DateTime> EachCalendarDay(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1)) yield
            return date;
        }
        public static string IgnorarAcentos(this string s)
        {
            Encoding destEncoding = Encoding.GetEncoding("iso-8859-8");
            return destEncoding.GetString(Encoding.Convert(Encoding.UTF8, destEncoding, Encoding.UTF8.GetBytes(s)));
        }
    }


}