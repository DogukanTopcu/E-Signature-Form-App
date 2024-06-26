﻿extern alias ITextSharp;
extern alias ma3Bouncy;

using System;
using System.Collections.Generic;
using System.IO;

using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Pkcs;
using ITextSharp::iTextSharp.text;
using ITextSharp::iTextSharp.text.pdf;
using ITextSharp::iTextSharp.text.pdf.security;
using tr.gov.tubitak.uekae.esya.api.common.util;
using System.Windows.Forms;
using Org.BouncyCastle.Ocsp;


namespace E_Signature
{
    public class PdfSigner
    {
        static public ICrlClient crl;
        static public List<ICrlClient> crlList;
        static public OcspClientBouncyCastle ocsp;
        private static System.Object lockSign = new System.Object();
        private static System.Object lockToken = new System.Object();

        public PdfSigner()
        {
            LicenseUtil.setLicenseXml(new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lisans.xml"), FileMode.Open, FileAccess.Read));
        }

        private X509Certificate2[] generateCertificateChain(X509Certificate2 signingCertificate)
        {
            X509Chain Xchain = new X509Chain();
            Xchain.ChainPolicy.ExtraStore.Add(signingCertificate);
            Xchain.Build(signingCertificate); // Whole chain!
            X509Certificate2[] chain = new X509Certificate2[Xchain.ChainElements.Count];
            int index = 0;
            foreach (X509ChainElement element in Xchain.ChainElements)
            {
                chain[index++] = element.Certificate;
            }
            return chain;
        }

        private static ICollection<Org.BouncyCastle.X509.X509Certificate> chainToBouncyCastle(X509Certificate2[] chain)
        {
            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();

            ICollection<Org.BouncyCastle.X509.X509Certificate> Bouncychain = new List<Org.BouncyCastle.X509.X509Certificate>();
            int index = 0;
            foreach (var item in chain)
            {
                Bouncychain.Add(cp.ReadCertificate(item.RawData));
            }
            return Bouncychain;

        }

        public byte[] SignPDF(Data request, byte[] PDFContent)
        {
            X509Certificate2 signingCertificate;
            IExternalSignature externalSignature;

            // Problem here!!!
            this.SelectSignature(request, out signingCertificate, out externalSignature);
            X509Certificate2[] chain = generateCertificateChain(signingCertificate);
            ICollection<Org.BouncyCastle.X509.X509Certificate> Bouncychain = chainToBouncyCastle(chain);
            ocsp = new OcspClientBouncyCastle();
            crl = new ITextSharp.iTextSharp.text.pdf.security.CrlClientOnline(Bouncychain);
            PdfReader pdfReader = new PdfReader(PDFContent);
            MemoryStream stream = new MemoryStream();
            PdfStamper pdfStamper = PdfStamper.CreateSignature(pdfReader, stream, '\0', "", true);
            PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;

            crlList = new List<ICrlClient>();
            crlList.Add(crl);

            lock (lockSign)
            {
                MakeSignature.SignDetached(signatureAppearance, externalSignature, Bouncychain, crlList, ocsp, null, 0, CryptoStandard.CMS);
            }
            return stream.ToArray();
        }

        private void SelectSignature(Data request, out X509Certificate2 CERTIFICATE, out IExternalSignature externalSignature)
        {
            try
            {
                // Problem here!!!
                SmartCardManager smartCardManager = SmartCardManager.getInstance();
                var smartCardCertificate = smartCardManager.getSignatureCertificate(false, false);
                var signer = smartCardManager.getSigner(request.DonglePassword, smartCardCertificate);
                CERTIFICATE = smartCardCertificate.asX509Certificate2();
                externalSignature = new SmartCardSignature(signer, CERTIFICATE, "SHA-256");

            }
            catch (Exception e)
            {
                CERTIFICATE = null;
                externalSignature = null;
                MessageBox.Show("Certificate is not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
