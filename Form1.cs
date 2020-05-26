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
    
    public partial class Form1 : Form
    {
        bool RSAencrypt = false;
        bool MD5encrypt = false;

        public Form1()
        {
            InitializeComponent();
        }
        string hash = "DM-PROJECT";
       // RSA obj = new RSA();

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
            MD5encrypt = true;
            RSAencrypt = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MD5encrypt == true)
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
            else
            {
                MessageBox.Show("Please decrypt using the same method");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }        

        private void button3_Click(object sender, EventArgs e)
        {

            textBox2.Text = encryption.RSACrypto.Encrypt(textBox1.Text);
            RSAencrypt = true;
            MD5encrypt = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}