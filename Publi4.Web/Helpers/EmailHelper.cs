using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publi4.Helpers
{
    public class EmailImageUrls
    {
        public static string LogoGreenBg { get; set; }
    }

    public static class EmailHelper
    {
        public static string InvoiceHtml
        {
            get
            {
                var html = System.IO.File.ReadAllText(@".\Email\send_invoice.min.html");
                html = html.Replace("__LOGO_PATH__", EmailImageUrls.LogoGreenBg);

                return html;
            }
        }

        public static string ConfirmationHtml
        {
            get
            {
                var html = System.IO.File.ReadAllText(@".\Email\confirmation.min.html");
                html = html.Replace("__LOGO_PATH__", EmailImageUrls.LogoGreenBg);

                return html;
            }
        }

        public static string ResetPassword
        {
            get
            {
                var html = System.IO.File.ReadAllText(@".\Email\resetpassword.min.html");
                html = html.Replace("__LOGO_PATH__", EmailImageUrls.LogoGreenBg);

                return html;
            }
        }
    }
}
