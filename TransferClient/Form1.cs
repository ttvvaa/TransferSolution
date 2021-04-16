using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransferClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    label1.Text = $"Uploading {dlg.FileName}";

                    bool res = FileUpload(dlg.FileName);

                    if (res)
                    {
                        label1.Text = $"Upload {dlg.FileName} succesful.";
                    }
                    else
                    {
                        label1.Text = $"Upload {dlg.FileName} error!";
                    }
                }
            }
        }

        private static bool FileUpload(string filePath)
        {
            var client = new TransferClient.TransferServiceReference.TransferServiceClient();

            var fileName = Path.GetFileName(filePath);

            long length = new FileInfo(filePath).Length;
            
            // TODO кто его диспозит?
            FileStream stream = new FileStream(filePath, FileMode.Open);

            client.UploadDocumentStorage(fileName, length, stream);

            return true;
        }
    }
}
