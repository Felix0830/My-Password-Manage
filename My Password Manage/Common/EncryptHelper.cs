using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManage.Common
{
    public class EncryptHelper
    {
        #region 方法一
        private SymmetricAlgorithm mCSP;
        private readonly byte[] key = null;

        public EncryptHelper() { }

        public EncryptHelper(string mykey)
        {
            mCSP = new DESCryptoServiceProvider();
            key = Encoding.UTF8.GetBytes(mykey);
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="Value">需加密的字符串</param>
        /// <returns></returns>
        public string EncryptString(string Value)
        {
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            try
            {
                byt = Encoding.UTF8.GetBytes(Value);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, mCSP.CreateEncryptor(key, key), CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);

                cs.FlushFinalBlock();
                cs.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                return Value;
            }
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="Value">要解密的字符串</param>
        /// <returns>string</returns>
        public string DecryptString(string Value)
        {
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            try
            {
                byt = Convert.FromBase64String(Value);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, mCSP.CreateDecryptor(key, key), CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);

                cs.FlushFinalBlock();
                cs.Close();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch
            {
                return Value;
            }
        }
        #endregion
    }
}
