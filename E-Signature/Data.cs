using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Signature
{
    [Serializable]
    public class Data
    {
        public string DonglePassword { get; set; }
        public byte[] pdfContent { get; set; }
    }
}
