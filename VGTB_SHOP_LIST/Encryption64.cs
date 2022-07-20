using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace VGTB_SHOP_LIST
{
    public class Encryption64
    {
        private byte[] key = { };
        private byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
        private String _EncKey = "Pwd@TAF";

        public string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {
            string sEncKey = sEncryptionKey + _EncKey;
            byte[] inputByteArray = new byte[stringToDecrypt.Length];
            try
            {
                key = Encoding.UTF8.GetBytes(sEncKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Encrypt(string stringToEncrypt, string sEncryptionKey)
        {
            try
            {
                string sEncKey = sEncryptionKey + _EncKey;
                key = Encoding.UTF8.GetBytes(sEncKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Dictionary<string, string> EncrytURL(string stringToEncrypt, string SEncryptionKey)
        {
            string sEncKey = SEncryptionKey + _EncKey;
            string _val = Decrypt(stringToEncrypt.Replace(" ", "+"), sEncKey);
            Dictionary<string, string> _dic = new Dictionary<string, string>();
            foreach (string Str in _val.Split('&'))
            {
                _dic.Add(Str.Split('=')[0], Str.Split('=')[1]);
            }
            return _dic;
        }
    }
}