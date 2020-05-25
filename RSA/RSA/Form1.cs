using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA
{
   // [System.Runtime.InteropServices.GuidAttribute("490A5DEF-7B26-4880-8AB3-CA6C1331B218")]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string hash = "DM-PROJECT";

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(textBox1.Text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    textBox2.Text = Convert.ToBase64String(results, 0, results.Length);
                
                }
             }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] data = Convert.FromBase64String(textBox2.Text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    textBox3.Text = UTF8Encoding.UTF8.GetString(results);

                }
            }

        }
    }
}
