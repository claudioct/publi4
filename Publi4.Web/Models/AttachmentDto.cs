using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publi4.Models
{
    public class AttachmentDto
    {
        public string ContentType { get; internal set; }
        public byte[] Content { get; internal set; }
        public string FileName { get; internal set; }
    }
}
