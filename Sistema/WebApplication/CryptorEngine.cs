using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication
{
    public class CryptorEngine
    {
        private string SecurityKey = "Charles Deluxe";
        /// <summary>
        /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <param name="useHashing">use hashing? send to for extra secirity</param>
        /// <returns></returns>
        public string Encrypt(string toEncrypt, bool useHashing)
        {
            MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            //log.LogToList(toEncrypt, method.ReflectedType.Name + "." + method.Name, "Encrypt", DateTime.Now);

            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //// Get the key from config file
            //string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            string key = SecurityKey;
            //System.Windows.Forms.MessageBox.Show(key);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public string Decrypt(string cipherString, bool useHashing)
        {
            MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            //log.LogToList(cipherString, method.ReflectedType.Name + "." + method.Name, "Decrypt", DateTime.Now);

            string resultado = string.Empty;

            try
            {
                cipherString = cipherString.Replace(" ", "+");

                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                ////Get your key from config file to open the lock!
                //string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
                string key = SecurityKey;

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                resultado = UTF8Encoding.UTF8.GetString(resultArray);
                tdes.Clear();
            }
            catch (Exception)
            {
                resultado = String.Empty;
                //Logger.Instance.LogToList(ex, method.ReflectedType.Name + "." + method.Name);
            }
            return resultado;
        }
    }
}