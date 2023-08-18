using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Signature
{
    public class SignatureManager
    {
        private PdfSigner _signer;
        public SignatureManager()
        {
            _signer = new PdfSigner();
        }

        public void SignPdf(Data data) 
        {
            try
            {
                var pdfContentWithSign = _signer.SignPDF(data, data.pdfContent);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while signing the file.");
            }
        }
    }
}
