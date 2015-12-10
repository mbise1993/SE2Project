using System;
using System.Text;
using System.Security.Cryptography;

namespace HopeHouse.Common.Helpers
{
    public class PasswordEncrypt
    {
        /// <summary>
        /// Convert cleartext password to md5 encrypted password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static String EncryptPassword ( String password )
        {
            byte[] textToBytes; // byte array to store the bytes returned after the conversion
            MD5 algorithm = MD5.Create ( ); // md5 algorithm object created to encrypt strings

            textToBytes = algorithm.ComputeHash ( Encoding.UTF8.GetBytes ( password ) ); // password encrypted

            return BitConverter.ToString ( textToBytes ).Replace ( "-", "" );
        }
    }
}
