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
    public partial class HillCipher : Form
    {

        private string input_article = "";
        private char[] table;
        private int[,] key=new int[2,2];

        public void CreateTable()
        {
            table = new char[52];
            int i;

            for (i = 0; i < 52; i++)
            {
                if (i < 26)
                    table[i] = (char)('A' + i);
                else
                    table[i] = (char)('a' + (i - 26));

            }

        



        }

        public int mod(int m, int n)
        {
            if (m > 0)
                return m % n;
            else
            {
                while (m < 0)
                    m += n;
                return m % n;

            }

        }


        private bool prime_check(int n)
        {
            if (n == 2)
                return true;

            int i = 2;

            while (i * i < n)
            {
                if (n % i == 0)
                    return false;
                i += 1;
                      
            
            }

            return true;
        
        }

        private int find_index(char a)
        {
            int i;
            for (i = 0; i < table.Length; i++)
                if (table[i] == a)
                    return i;

            return -1;
        
        }

        private void Hillcipher(int[,] key2,bool en)
        {
            if (en == false)
            {
                int temp;
                temp = key2[0, 0];
                key2[0, 0] = key2[1, 1];
                key2[1, 1] = temp;
                key2[0, 1] = -key2[0, 1];
                key2[1, 0] = -key2[1, 0];

            
            }

            char[] buf = input_article.ToCharArray();

            int i,count=0;
            int first_index=0;
            int first_char_index=0;
            int second_char_index=0;
  
            

                

            string buf_string = "";

            for (i = 0; i < buf.Length; i++)
            {
                if ((buf[i] >= 'A' && buf[i] <= 'Z') || (buf[i] >= 'a' && buf[i] <= 'z'))
                {
                    count += 1;

                    if (count == 1)
                    {
                        first_index = i;
                        first_char_index = find_index(buf[i]);
                    

                    }
                    else if (count == 2)
                    {
                        second_char_index = find_index(buf[i]);

                        

                        int new_first = mod((key2[0, 0] * first_char_index + key2[0, 1] * second_char_index) , 52);
                        int new_second = mod((key2[1, 0] * first_char_index + key2[1, 1] * second_char_index) , 52);
                       

                        buf[first_index] = table[new_first];
                        buf[i] = table[new_second];

                        count = 0;



                    }
                }
            
            }

            for (i = 0; i < buf.Length; i++)
                buf_string += Char.ToString(buf[i]);

            input_article = buf_string;

           

        
        
        }

        public HillCipher()
        {
            InitializeComponent();
             CreateTable();
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

        private void HillCipher_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Encrytion");
            comboBox1.Items.Add("Decrytion");
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                button2.Visible = true;
            else
                button2.Visible = false;

            
                
                  
         }

        private void button2_Click(object sender, EventArgs e)
        {
            Random r = new Random();

            int first = r.Next(100);

            int second = r.Next(100);

            while (prime_check((first * second)-1))
                second = r.Next(100);


            int factor = r.Next((first * second)-3) + 1;

            while((first*second-1)%factor!=0)
                factor = r.Next((first * second) - 4) + 2;


            string buf = "";
            string show_buf = "";

            show_buf += first.ToString() + " " + factor.ToString() + "\r\n";
            show_buf += (((first * second) - 1) / factor).ToString() + " " + second.ToString() + "\r\n";

            buf += first.ToString() + " " + factor.ToString() + " "
                + (((first * second) - 1) / factor).ToString() + " " + second.ToString();

            key[0, 0] = first;
            key[0, 1] = factor;
            key[1,0]=(first * second - 1) / factor;
            key[1, 1] = second;


           DialogResult a = MessageBox.Show(show_buf + "\n" + "是否要將此KEY儲存下來", "KEY",
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

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files(*.txt)|*.txt";
            

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);

                string buf="";
               
                int i, j;

                while (sr.Peek() != -1)  
                    buf += sr.ReadLine();



            
                sr.Close();

                string[] ss = buf.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


                for (i = 0; i < 2; i++)
                    for (j = 0; j < 2; j++)
                        key[i, j] = Convert.ToInt32(ss[2 * i + j]);



                        MessageBox.Show(buf);



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                Hillcipher(key, true);
            else
                Hillcipher(key, false);

            MessageBox.Show(comboBox1.Text + " finish");
        }

        private void 使用說明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string buf="";

            buf += "這是一個Hill Cipher(2*2)的加解密器\n";
            buf += "程式有提供讀寫檔，和自動產生key\n";
            buf += "\n";
            buf += "作者:施智偉 SHIH-JHIH-WEI\n";
            buf += "學歷:長庚大學電子工程學系四年級甲班\n";
            buf += "科目:網路安全概論\n";
            buf += "指導老師:黎明富\n";
            buf += "完成時間:2016/6/12 1:30\n";

            MessageBox.Show(buf);
        }
    }
}
