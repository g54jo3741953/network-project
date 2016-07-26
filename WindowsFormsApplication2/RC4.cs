using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class RC4 : Form
    {
        private string input_article;
        private string key="";
        private int[] S = new int[256];
        private int[] T = new int[256];

        private void GenerateS()
        {
            char[] buf_string = key.ToCharArray();
            int[] key_int = new int[buf_string.Length];
            int i,j;
            int temp;

            for (i = 0; i < key_int.Length; i++)
                key_int[i] = (int)buf_string[i];

                for (i = 0; i < S.Length; i++)
                {
                    S[i] = i;
                    T[i] = key_int[i % key_int.Length];
                }

            for (i = 0; i < S.Length; i++)
            {
                j = 0;
                j = (j + S[i] + T[i]) % 256;
                temp = S[i];
                S[i] = S[j];
                S[j] = temp;
            }
            
        
        }

        private void Rc4()
        {
            char[] buf = input_article.ToCharArray();
            int i, j,k;

            int temp;
            for (i = 0; i < buf.Length; i++)
            {   j = 0;
                k = (i + 1) % 256;
                j = (j + S[k]) % 256;
                temp =S[k];
                S[k] = S[j];
                S[j] = temp;

                k = (S[k] + S[j]) % 256;


                buf[i] = (char)((int)buf[i] ^ S[k]);
            
            
            
            }

            string buf_string = "";
            for (i = 0; i < buf.Length; i++)
                buf_string += Char.ToString(buf[i]);

            input_article = buf_string;
            
        
        
        }

        public RC4()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "ALL files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                input_article = sr.ReadToEnd();
                MessageBox.Show("成功讀檔");
                sr.Close();


            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files(*.txt)|*.txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {


                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                sw.Write(input_article);

                sw.Close();

                MessageBox.Show("存檔完成");



            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {

                GenerateS();
                Rc4();

                MessageBox.Show(comboBox1.Text + "完成");

            }
            else
                MessageBox.Show("You must enter key");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            key = textBox1.Text;
        }

        private void RC4_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Encrytion");
            comboBox1.Items.Add("Decrytion");
            comboBox1.SelectedIndex = 0;
        }
    }
}
