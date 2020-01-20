using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publi4.Services
{
    public class EmailMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }

        public string NomeAdmin { get; set; }

        public string EmailAdmin { get; set; }
    }
}
