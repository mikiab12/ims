using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ims.domain
{
    public static class Utils
    {
        public static string Hash(this string text)
        {
            using (var hashProvider = MD5.Create())
            {
                var strBuilder = new StringBuilder();

                foreach (var b in hashProvider.ComputeHash(Encoding.UTF8.GetBytes(text)))
                    strBuilder.Append(b.ToString("x2"));

                return strBuilder.ToString();
            }
        }
    }

    public class IMSConfiguration
    {

    }


}
