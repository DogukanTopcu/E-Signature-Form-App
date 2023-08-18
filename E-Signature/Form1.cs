extern alias ITextSharp;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using ITextSharp::iTextSharp.text.pdf;
using ITextSharp::iTextSharp.text.pdf.security;

namespace E_Signature
{
    public partial class Form1 : Form
    {
        private Data data;
        public Form1()
        {
            InitializeComponent();
            data = new Data();
            bckWorker.DoWork += bckWorker_doWork;
            bckWorker.RunWorkerCompleted += bckWorker_RunWorkerCompleted;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Sign_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordArea.Text))
            {
                Sign.Text = "Signing, please wait.";
                Sign.Enabled = false;
                this.data.DonglePassword = passwordArea.Text;

                bckWorker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Enter your USB-Dongle password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog upload = new OpenFileDialog())
            {
                upload.Filter = "Pdf Files|*.pdf";
                upload.Title = "Select the pdf file to be signed.";
                if (upload.ShowDialog() != DialogResult.OK)
                    return;
                string fileName = upload.FileName;
                fileLabel.Text = fileName;
                this.data.pdfContent = File.ReadAllBytes(fileName);
            }
        }



        private string checkSignature(byte[] pdfContent)
        {
            PdfReader reader = new PdfReader(pdfContent);

            AcroFields fields = reader.AcroFields;
            List<String> names = fields.GetSignatureNames();

            if (names.Count == 0)
            {
                return "The signature(s) of the relevant PDF could not be found.";
            }
            string message = string.Empty;
            for (int i = 1; i < names.Count + 1; i++)
            {
                string temp = string.Empty;
                PdfPKCS7 pkcs7 = fields.VerifySignature(names[i - 1]);
                var result = pkcs7.Verify();
                if (result)
                {
                    temp = string.Format("{0}.signature valid.", i);
                }
                else
                {
                    temp = string.Format("{0}.signature invalid.", i);
                }
                message += temp;
            }
            reader.Close();
            return message;
        }

        private void controlSignature_Click(object sender, EventArgs e)
        {
            try
            {
                var message = checkSignature(this.data.pdfContent);
                MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch {
                MessageBox.Show("A selected file was not found. Please select a file.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void bckWorker_doWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("bckWorker_doWork");
            SignatureManager signManager = new SignatureManager();
            signManager.SignPdf(this.data);
        }

        private void bckWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Sign.Enabled = true;
            Sign.Text = "Sign Document";
            if (e.Error != null)
            {
                MessageBox.Show("Error occured : " + e.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled == true)
            {
                MessageBox.Show("Operation cancelled!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Pdf was successfuly signed and saved.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
