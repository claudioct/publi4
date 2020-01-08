using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publi4.Helpers
{
    public static class Base64Helper
    {
        public static string Encode(string txt)
        {
            return System.Net.WebUtility.UrlEncode(txt);

            //byte[] encodedBytes = System.Text.Encoding.Unicode.GetBytes(txt);
            //return Convert.ToBase64String(encodedBytes);
        }

        public static string Decode(string txt)
        {

            return System.Net.WebUtility.UrlDecode(txt);
            //byte[] decodedBytes = Convert.FromBase64String(Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(txt)));
            //return System.Text.Encoding.Unicode.GetString(decodedBytes);
        }
    }
}
