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
    public partial class NSubstitutionCipher : Form
    {
        private int[] table_4bit;
        private int[] table_8bit;
        private int[] table_16bit;
        private string input_article;
        private char[] IV = {'a','b'};
        private int ctr = 10;

        private void table_generator(int[] table)
        {
            int len = table.Length;
            Random r = new Random();
            int i;

            for (i = 0; i < len; i++)
                table[i] = i;

            for (i = 0; i < len; i++)
            {
               int num=  r.Next(len);
               int buf = table[i];
                   table[i]=table[num];
                   table[num] = buf;
            
            }
         }



   

        
        private void SubstitutionECB(int[] table,int bit)
        {
            char[] buf = input_article.ToCharArray();
            int i;
            char[] buf2;

            if (buf.Length % 2 != 0)
            {
                buf2 = new char[buf.Length + 1];

                for (i = 0; i < buf.Length; i++)
                    buf2[i] = buf[i];
                buf2[i] = ' ';
            
            }
            else
               buf2 = buf;

            string buf_string = "";

            

            if (bit == 4)
            {

                for (i = 0; i < buf2.Length; i++)
                {
                    
                    int buf3 = (int)buf2[i];
                    int right_4 = buf3 % 16;
                    int left_4 = (buf3 - right_4) / 16;
                   
                    buf3 = table[right_4] + table[left_4] * 16;

                    buf2[i] = (char)buf3;
                }

               
            
            
            }
            else if (bit == 8)
            {
                for (i = 0; i < buf2.Length; i++)
                {
                    int buf3 = (int)buf2[i];
                    buf2[i] = (char)table[buf3];

                }

            }
            else
            {
                for (i = 0; i < buf2.Length; i = i + 2)
                {

                    int right_8 = (int)buf2[i];
                    int left_8 = (int)buf2[i+1];

                    int buf3 = left_8 * 256 + right_8;

                    buf3 = table[buf3];

                    left_8 = buf3 % 256;
                    right_8 = (buf3 - left_8) / 256;

                    buf2[i] = (char)left_8;
                    buf2[i + 1] = (char)right_8;
                
                
                
                }

            
            
            }

            for (i = 0; i < buf2.Length; i++)
                buf_string += Char.ToString(buf2[i]);

            input_article = buf_string;
            

        
        }
        
        private void SubstitutionCBCen(int[] table, int bit)
        {
            char[] buf = input_article.ToCharArray();
            int i;
            char[] buf2;

            if (buf.Length % 2 != 0)
            {
                buf2 = new char[buf.Length + 1];

                for (i = 0; i < buf.Length; i++)
                    buf2[i] = buf[i];
                buf2[i] = ' ';

            }
            else
                buf2 = buf;

            string buf_string = "";
            int IV_buf;



            if (bit == 4)
            {
                IV_buf = (int)IV[0];
                IV_buf = IV_buf % 16;
               


                for (i = 0; i < buf2.Length; i++)
                {
                    
                    int buf3 = (int)buf2[i];
                    int right_4 = buf3 % 16;
                    int left_4 = (buf3 - right_4) / 16;

        
                    right_4 = right_4 ^ IV_buf;
                  
                    IV_buf = table[right_4];
                    left_4 = left_4 ^ IV_buf;
                    IV_buf = table[left_4];
                    

                    buf3 = table[right_4] + table[left_4] * 16;

                    buf2[i] = (char)buf3;
                }




            }
            else if (bit == 8)
            {
                IV_buf = (int)IV[0];

                for (i = 0; i < buf2.Length; i++)
                {
                    int buf3 = (int)buf2[i];

                    buf3 = buf3 ^ IV_buf;
                    buf2[i] = (char)table[buf3];
                    IV_buf = table[buf3];

                }

            }
            else
            {
                IV_buf = (int)IV[0] + 256 * (int)IV[1];


                for (i = 0; i < buf2.Length; i = i + 2)
                {

                    int right_8 = (int)buf2[i];
                    int left_8 = (int)buf2[i + 1];

                    int buf3 = left_8 * 256 + right_8;

                    buf3 = buf3 ^ IV_buf;

                    buf3 = table[buf3];

                    IV_buf = buf3;

                    left_8 = buf3 % 256;
                    right_8 = (buf3 - left_8) / 256;

                    buf2[i] = (char)left_8;
                    buf2[i + 1] = (char)right_8;



                }



            }

            for (i = 0; i < buf2.Length; i++)
                buf_string += Char.ToString(buf2[i]);

            input_article = buf_string;
            
        
        
        
        }

        private void SubstitutionCBCde(int[] table, int bit)
        {
            char[] buf = input_article.ToCharArray();
            int i;
            char[] buf2;

            if (buf.Length % 2 != 0)
            {
                buf2 = new char[buf.Length + 1];

                for (i = 0; i < buf.Length; i++)
                    buf2[i] = buf[i];
                buf2[i] = ' ';

            }
            else
                buf2 = buf;

            string buf_string = "";
            int IV_buf;


            if (bit == 4)
            {
                IV_buf = (int)IV[0];
                IV_buf = IV_buf % 16;

                for (i = 0; i < buf2.Length; i++)
                {

                    int buf3 = (int)buf2[i];
                    int right_4 = buf3 % 16;
                    int left_4 = (buf3 - right_4) / 16;
                    int temp = right_4;
             

                    right_4 = table[temp] ^ IV_buf;
                  
                    IV_buf = temp;
                    temp=left_4;
                    left_4 = table[temp] ^ IV_buf;
                    
                    IV_buf = temp;

                    buf3 = right_4 + left_4 * 16;

                    buf2[i] = (char)buf3;
                }




            }
            else if (bit == 8)
            {
                IV_buf = (int)IV[0];

                for (i = 0; i < buf2.Length; i++)
                {
                    int buf3 = (int)buf2[i];
                    buf2[i] = (char)(table[buf3] ^ IV_buf);
                    IV_buf = buf3;

                    

                }

            }
            else
            {
                IV_buf = (int)IV[0] + 256 * (int)IV[1];

                for (i = 0; i < buf2.Length; i = i + 2)
                {

                    int right_8 = (int)buf2[i];
                    int left_8 = (int)buf2[i + 1];

                    int buf3 = left_8 * 256 + right_8;
                    int temp = buf3;

                    buf3 = table[buf3];
                    buf3 = buf3 ^ IV_buf;
                    IV_buf = temp;

                    left_8 = buf3 % 256;
                    right_8 = (buf3 - left_8) / 256;

                    buf2[i] = (char)left_8;
                    buf2[i + 1] = (char)right_8;



                }



            }

            for (i = 0; i < buf2.Length; i++)
                buf_string += Char.ToString(buf2[i]);

            input_article = buf_string;
            
        
        
        }
       
        private void SubstitutionCFBen(int[] table, int bit)
        {

            char[] buf = input_article.ToCharArray();
            int i;
            char[] buf2;

            if (buf.Length % 2 != 0)
            {
                buf2 = new char[buf.Length + 1];

                for (i = 0; i < buf.Length; i++)
                    buf2[i] = buf[i];
                buf2[i] = ' ';

            }
            else
                buf2 = buf;

            string buf_string = "";
            int IV_buf;



            if (bit == 4)
            {
                IV_buf = (int)IV[0];
                IV_buf = IV_buf % 16;



                for (i = 0; i < buf2.Length; i++)
                {

                    int buf3 = (int)buf2[i];
                    int right_4 = buf3 % 16;
                    int left_4 = (buf3 - right_4) / 16;

                    IV_buf = table[IV_buf];
                    right_4 = right_4 ^ IV_buf;
                    IV_buf = right_4;
                    IV_buf = table[IV_buf];
                    left_4 = left_4 ^ IV_buf;
                    IV_buf = left_4;
                    

                    buf3 = right_4 + left_4 * 16;

                    buf2[i] = (char)buf3;
                }




            }
            else if (bit == 8)
            {
                IV_buf = (int)IV[0];

                for (i = 0; i < buf2.Length; i++)
                {
                    int buf3 = (int)buf2[i];

                    IV_buf = table[IV_buf];
                    buf3 = buf3 ^ IV_buf;
                    buf2[i] = (char)buf3;
                    IV_buf = buf3;

                }

            }
            else
            {
                IV_buf = (int)IV[0] + 256 * (int)IV[1];


                for (i = 0; i < buf2.Length; i = i + 2)
                {

                    int right_8 = (int)buf2[i];
                    int left_8 = (int)buf2[i + 1];

                    int buf3 = left_8 * 256 + right_8;

                    IV_buf = table[IV_buf];

                    buf3 = buf3^IV_buf;

                    IV_buf = buf3;

                    left_8 = buf3 % 256;
                    right_8 = (buf3 - left_8) / 256;

                    buf2[i] = (char)left_8;
                    buf2[i + 1] = (char)right_8;



                }



            }

            for (i = 0; i < buf2.Length; i++)
                buf_string += Char.ToString(buf2[i]);

            input_article = buf_string;
            
        
        
        
        
        
        
        }

        private void SubstitutionCFBde(int[] table, int bit)
        {
            char[] buf = input_article.ToCharArray();
            int i;
            char[] buf2;

            if (buf.Length % 2 != 0)
            {
                buf2 = new char[buf.Length + 1];

                for (i = 0; i < buf.Length; i++)
                    buf2[i] = buf[i];
                buf2[i] = ' ';

            }
            else
                buf2 = buf;

            string buf_string = "";
            int IV_buf;



            if (bit == 4)
            {
                IV_buf = (int)IV[0];
                IV_buf = IV_buf % 16;



                for (i = 0; i < buf2.Length; i++)
                {

                    int buf3 = (int)buf2[i];
                    int right_4 = buf3 % 16;
                    int left_4 = (buf3 - right_4) / 16;
                    int temp;

                    IV_buf = table[IV_buf];
                    temp = right_4;
                    right_4 = temp ^ IV_buf;
                    IV_buf = temp;
                    IV_buf = table[IV_buf];
                    temp = left_4;
                    left_4 = temp ^ IV_buf;
                    IV_buf = temp;


                    buf3 = right_4 + left_4 * 16;

                    buf2[i] = (char)buf3;
                }




            }
            else if (bit == 8)
            {
                IV_buf = (int)IV[0];

                for (i = 0; i < buf2.Length; i++)
                {
                    int buf3 = (int)buf2[i];
                    int temp = buf3;

                    IV_buf = table[IV_buf];
                    buf3 = temp ^ IV_buf;
                    buf2[i] = (char)buf3;
                    IV_buf = temp;

                }

            }
            else
            {
                IV_buf = (int)IV[0] + 256 * (int)IV[1];


                for (i = 0; i < buf2.Length; i = i + 2)
                {

                    int right_8 = (int)buf2[i];
                    int left_8 = (int)buf2[i + 1];

                    int buf3 = left_8 * 256 + right_8;
                    int temp = buf3;

                    IV_buf = table[IV_buf];

                    buf3 = temp ^ IV_buf;

                    IV_buf = temp;

                    left_8 = buf3 % 256;
                    right_8 = (buf3 - left_8) / 256;

                    buf2[i] = (char)left_8;
                    buf2[i + 1] = (char)right_8;



                }



            }

            for (i = 0; i < buf2.Length; i++)
                buf_string += Char.ToString(buf2[i]);

            input_article = buf_string;        
        
        }

        private void SubstitutionOFB(int[] table, int bit)
        {
           
            char[] buf = input_article.ToCharArray();
            int i;
            char[] buf2;

            if (buf.Length % 2 != 0)
            {
                buf2 = new char[buf.Length + 1];

                for (i = 0; i < buf.Length; i++)
                    buf2[i] = buf[i];
                buf2[i] = ' ';

            }
            else
                buf2 = buf;

            string buf_string = "";
            int IV_buf;



            if (bit == 4)
            {
                IV_buf = (int)IV[0];
                IV_buf = IV_buf % 16;



                for (i = 0; i < buf2.Length; i++)
                {

                    int buf3 = (int)buf2[i];
                    int right_4 = buf3 % 16;
                    int left_4 = (buf3 - right_4) / 16;

                    IV_buf = table[IV_buf];
                    right_4 = right_4 ^ IV_buf;

                    IV_buf = table[IV_buf];
                    left_4 = left_4 ^ IV_buf;



                    buf3 = right_4 + left_4 * 16;

                    buf2[i] = (char)buf3;
                }




            }
            else if (bit == 8)
            {
                IV_buf = (int)IV[0];

                for (i = 0; i < buf2.Length; i++)
                {
                    int buf3 = (int)buf2[i];

                    IV_buf = table[IV_buf];
                    buf3 = buf3 ^ IV_buf;
                    buf2[i] = (char)buf3;


                }

            }
            else
            {
                IV_buf = (int)IV[0] + 256 * (int)IV[1];


                for (i = 0; i < buf2.Length; i = i + 2)
                {

                    int right_8 = (int)buf2[i];
                    int left_8 = (int)buf2[i + 1];

                    int buf3 = left_8 * 256 + right_8;

                    IV_buf = table[IV_buf];

                    buf3 = buf3 ^ IV_buf;



                    left_8 = buf3 % 256;
                    right_8 = (buf3 - left_8) / 256;

                    buf2[i] = (char)left_8;
                    buf2[i + 1] = (char)right_8;



                }



            }

            for (i = 0; i < buf2.Length; i++)
                buf_string += Char.ToString(buf2[i]);

            input_article = buf_string;
            
        
        
        
        
        }

        private void SubstitutionCTR(int[] table, int bit)
        {
            char[] buf = input_article.ToCharArray();
            int i;
            char[] buf2;

            if (buf.Length % 2 != 0)
            {
                buf2 = new char[buf.Length + 1];

                for (i = 0; i < buf.Length; i++)
                    buf2[i] = buf[i];
                buf2[i] = ' ';

            }
            else
                buf2 = buf;

            string buf_string = "";

            ctr = 10;



            if (bit == 4)
            {
                ctr = ctr % 16;


                for (i = 0; i < buf2.Length; i++)
                {

                    int buf3 = (int)buf2[i];
                    int right_4 = buf3 % 16;
                    int left_4 = (buf3 - right_4) / 16;

                    right_4 = table[ctr] ^ right_4;

                    ctr = (ctr + 1) % 16;

                    left_4 = table[ctr] ^ left_4;
                   


                    buf3 = right_4 + left_4 * 16;

                    buf2[i] = (char)buf3;
                }




            }
            else if (bit == 8)
            {
                ctr = ctr % 256;
                for (i = 0; i < buf2.Length; i++)
                {
                    int buf3 = (int)buf2[i];

                    buf3 = table[ctr] ^ buf3;

                    buf2[i] = (char)buf3;

                    ctr = (ctr + 1) % 256;
                }

            }
            else
            {
                ctr = ctr % 65536;
                for (i = 0; i < buf2.Length; i = i + 2)
                {

                    int right_8 = (int)buf2[i];
                    int left_8 = (int)buf2[i + 1];

                    int buf3 = left_8 * 256 + right_8;

                    buf3 = table[ctr]^buf3;

                    left_8 = buf3 % 256;
                    right_8 = (buf3 - left_8) / 256;

                    buf2[i] = (char)left_8;
                    buf2[i + 1] = (char)right_8;

                    ctr = (ctr + 1) % 65536;



                }



            }

            for (i = 0; i < buf2.Length; i++)
                buf_string += Char.ToString(buf2[i]);

            input_article = buf_string;
            
        
        }


        private void table_de(int[] table)
        {
            int[] buf_table = new int[table.Length];
            int i;

            for (i = 0; i < table.Length; i++)
                buf_table[table[i]] = i;

            for (i = 0; i < table.Length; i++)
                table[i] = buf_table[i];
        
        }

        public NSubstitutionCipher()
        {
            InitializeComponent();
        }

        private void NSubstitutionCipher_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("4");
            comboBox1.Items.Add("8");
            comboBox1.Items.Add("16");
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.Add("ECB");
            comboBox2.Items.Add("CBC");
            comboBox2.Items.Add("CFB");
            comboBox2.Items.Add("OFB");
            comboBox2.Items.Add("CTR");
            comboBox2.SelectedIndex = 0;
            comboBox3.Items.Add("Encytion");
            comboBox3.Items.Add("Decrytion");
            comboBox3.SelectedIndex = 0;


        }

        

        private void button1_Click(object sender, EventArgs e)
        {

            string buf = "";
            if (comboBox1.Text == "4")
            {
                table_4bit = new int[16];
                table_generator(table_4bit);
               
                int i;
                for (i = 0; i < table_4bit.Length; i++)
                {
                    buf += table_4bit[i].ToString();
                    buf += " ";
                
                }
               
            
            }
            else if (comboBox1.Text == "8")
            {

                table_8bit = new int[256];

                table_generator(table_8bit);

                
                int i;
                for (i = 0; i < table_8bit.Length; i++)
                {
                    buf += table_8bit[i].ToString();
                    buf += " ";

                }

               
            }

            else
            {
                table_16bit = new int[65536];

                table_generator(table_16bit);

                

                int i;
                for (i = 0; i < table_16bit.Length; i++)
                {
                    buf += table_16bit[i].ToString();
                    buf += " ";

                }
             
            
            }
            DialogResult a=DialogResult.OK;
            if(comboBox1.Text!="16")
            a = MessageBox.Show(buf + "\n" + "是否要將此KEY儲存下來", "KEY",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        

            if (a == DialogResult.OK)
            {
                saveFileDialog1.Filter = "txt files(*.txt)|*.txt";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);

                    sw.Write(buf);
                    sw.Close();

                }


            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "4")
            {
                table_4bit = new int[16];
                int i;

                openFileDialog1.Filter = "txt files(*.txt)|*.txt";
                
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);

                    string buf="";

                    while (sr.Peek() != -1)
                    buf += sr.ReadLine();

                    sr.Close();

                    string[] ss = buf.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


                    for (i = 0; i < ss.Length; i++)
                    {

                        table_4bit[i] = Convert.ToInt32(ss[i]);
                    
                    }

                    buf = "";

                    for (i = 0; i < table_4bit.Length; i++)
                    {
                        buf += table_4bit[i].ToString();
                        buf += " ";
                    
                    }

                    MessageBox.Show(buf);
                    
                   
                
                }


                
            }
            else if (comboBox1.Text == "8")
            {
                table_8bit = new int[256];

                int i;

                openFileDialog1.Filter = "txt files(*.txt)|*.txt";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);

                    string buf = "";

                    while (sr.Peek() != -1)
                        buf += sr.ReadLine();

                    sr.Close();

                    string[] ss = buf.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


                    for (i = 0; i < ss.Length; i++)
                    {

                        table_8bit[i] = Convert.ToInt32(ss[i]);

                    }

                    buf = "";

                    for (i = 0; i < table_8bit.Length; i++)
                    {
                        buf += table_8bit[i].ToString();
                        buf += " ";

                    }

                    MessageBox.Show(buf);



                }
            }
            else
            {

                table_16bit = new int[65536];

                int i;

                openFileDialog1.Filter = "txt files(*.txt)|*.txt";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);

                    string buf = "";

                    while (sr.Peek() != -1)
                        buf += sr.ReadLine();

                    sr.Close();

                    string[] ss = buf.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


                    for (i = 0; i < ss.Length; i++)
                    {

                        table_16bit[i] = Convert.ToInt32(ss[i]);

                    }

                    MessageBox.Show("Key Load Finish");




                }


            }
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

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                switch (comboBox1.SelectedIndex) 
                { 
                    case 0:
                        switch (comboBox2.SelectedIndex) { 
                            case 0:
                                SubstitutionECB(table_4bit, 4);
                                break;
                            case 1:
                             SubstitutionCBCen(table_4bit, 4);
                             break;
                            case 2:
                             SubstitutionCFBen(table_4bit, 4);
                             break;
                            case 3:
                             SubstitutionOFB(table_4bit, 4);
                             break;
                            case 4:
                             SubstitutionCTR(table_4bit, 4);
                             break;
                         }
               
                        break;
                    case 1:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                SubstitutionECB(table_8bit, 8);
                                break;
                            case 1:
                                SubstitutionCBCen(table_8bit, 8);
                                break;
                            case 2:
                                SubstitutionCFBen(table_8bit, 8);
                                break;
                            case 3:
                                SubstitutionOFB(table_8bit, 8);
                                break;
                            case 4:
                                SubstitutionCTR(table_8bit, 8);
                                break;
                        }
                        break;
                    case 2:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                SubstitutionECB(table_16bit, 16);
                                break;
                            case 1:
                                SubstitutionCBCen(table_16bit, 16);
                                break;
                            case 2:
                                SubstitutionCFBen(table_16bit, 16);
                                break;
                            case 3:
                                SubstitutionOFB(table_16bit, 16);
                                break;
                            case 4:
                                SubstitutionCTR(table_16bit, 16);
                                break;
                        }
                        break;

                }
               MessageBox.Show("加密完成");


         }
                
                        
            else 
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        switch (comboBox2.SelectedIndex)
                        { 
                            case 0:
                            table_de(table_4bit);
                            SubstitutionECB(table_4bit, 4);
                            break;

                            case 1:
                            table_de(table_4bit);
                            SubstitutionCBCde(table_4bit, 4);
                             break;
                            case 2:
                             SubstitutionCFBde(table_4bit, 4);
                             break;
                            case 3:
                             SubstitutionOFB(table_4bit, 4);
                             break;
                            case 4:
                             SubstitutionCTR(table_4bit, 4);
                             break;
                         }
                        break;
                    case 1:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                table_de(table_8bit);
                                SubstitutionECB(table_8bit, 8);
                                break;

                            case 1:
                                table_de(table_8bit);
                                SubstitutionCBCde(table_8bit, 8);
                                break;
                            case 2:
                                SubstitutionCFBde(table_8bit, 8);
                                break;
                            case 3:
                                SubstitutionOFB(table_8bit, 8);
                                break;
                            case 4:
                                SubstitutionCTR(table_8bit, 8);
                                break;
                        }
                        break;
                    case 2:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                table_de(table_16bit);
                                SubstitutionECB(table_16bit, 16);
                                break;

                            case 1:
                                table_de(table_16bit);
                                SubstitutionCBCde(table_16bit, 16);
                                break;
                            case 2:
                                SubstitutionCFBde(table_16bit, 16);
                                break;
                            case 3:
                                SubstitutionOFB(table_16bit, 16);
                                break;
                            case 4:
                                SubstitutionCTR(table_16bit, 16);
                                break;
                        }
                        break;

                }
                MessageBox.Show("解密完成");
            
            }
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                button1.Visible = true;
                button2.Visible = true;

            }
            else
            {
                button1.Visible = false;
                button2.Visible = true;
            
            }
        }

        private void 使用說明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string buf = "";

            buf += "這是一個n-bit Substitution的加解密程式\n";
            buf += "n=4,8,16bit\n";
            buf += "block mode 有ECB,CBC,CFB,OFB,CTR\n";
            buf += "本程式不提供使用者輸入initial vector and counter\n";
            buf += "程式有提供讀取文件的功能\n";
            buf += "當你在Encrytin下，你可以選擇自己讀入key值,或是由本程式自行產生key\n";
            buf += "當你選擇n=16 且產生key的時候，須要等候一小段時間\n";
            buf += "而在讀取key的時候，在n=4,8會顯示讀取的key,在n=16則顯示已經讀取\n";
            buf += "\n";
            buf += "作者:施智偉 SHIH-JHIH-WEI\n";
            buf += "學歷:長庚大學電子工程學系四年級甲班\n";
            buf += "科目:網路安全概論\n";
            buf += "指導老師:黎明富\n";
            buf += "完成時間:2016/6/9 19:50\n";

            MessageBox.Show(buf);
        }
    }
}
