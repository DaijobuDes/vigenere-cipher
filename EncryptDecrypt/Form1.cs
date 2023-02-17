using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptDecrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void EncryptFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Private key cannot be empty.", "Private key error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textBox1.Enabled = false;
            FileStream fs = new FileStream(EncryptFileDialog1.FileName, FileMode.OpenOrCreate);
            int[] temp = new int[fs.Length];

            for (int i = 0; i < fs.Length; i++)
            {
                temp[i] = fs.ReadByte();
            }
            fs.Close();

            String t_str = textBox1.Text;
            char[] data = t_str.ToCharArray();

            fs = new FileStream(EncryptFileDialog1.FileName, FileMode.OpenOrCreate);

            for (int i = 0, j = 0; i < fs.Length; i++, j++)
            {
                //fs.WriteByte((byte)(temp[i] + 3));
                int index = j % data.Length;
                fs.WriteByte((byte)(temp[i] + (int)data[index]));
            }
            fs.Close();
            textBox1.Enabled = true;

            MessageBox.Show("File encrypted.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void DecrpytFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Private key cannot be empty.", "Private key error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textBox1.Enabled = false;
            FileStream fs = new FileStream(DecrpytFileDialog2.FileName, FileMode.OpenOrCreate);
            int[] temp = new int[fs.Length];

            for (int i = 0; i < fs.Length; i++)
            {
                temp[i] = fs.ReadByte();
            }
            fs.Close();

            String t_str = textBox1.Text;
            char[] data = t_str.ToCharArray();

            fs = new FileStream(DecrpytFileDialog2.FileName, FileMode.OpenOrCreate);

            for (int i = 0, j = 0; i < fs.Length; i++, j++)
            {
                //fs.WriteByte((byte)(temp[i] - 3));
                int index = j % data.Length;
                fs.WriteByte((byte)(temp[i] - (int)data[index]));
            }
            fs.Close();
            textBox1.Enabled = true;

            MessageBox.Show("File decrypted.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EncryptButton(object sender, EventArgs e)
        {
            EncryptFileDialog1.ShowDialog();
        }

        private void DecryptButton(object sender, EventArgs e)
        {
            DecrpytFileDialog2.ShowDialog();
        }

        private void GeneratePrivateKey(object sender, EventArgs e)
        {
            textBox1.Text = "";
            char[] data = new char[10];
            Random random = new Random();
            
            for (int i = 0; i < 10; i++)
            {
                //textBox1.Text += random.Next('A', 'Z');
                data[i] = (char) random.Next('a', 'z');
            }
            textBox1.Text = new string(data);
        }
    }
}
