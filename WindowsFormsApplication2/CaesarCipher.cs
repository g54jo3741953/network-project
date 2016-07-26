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
    public partial class CaesarCipher : Form
    {
        private string input_article="";
        private char[] table;

        public  void CreateTable()
        {
            table = new char[54];
            int i;

            for (i = 0; i < 52; i++)
            {
                if (i < 26)
                    table[i] = (char)('A' + i);
                else
                    table[i] = (char)('a' + (i - 26));

            }

            table[52] = ',';
            table[53] = '.';

        
        
        }

        public int Index_Return(char input)
        {
            int i;
            for (i = 0; i < table.Length; i++)
                if (table[i] == input)
                    return i;

            return -1;
               
            
            
        
        }

        public int mod (int m, int n)
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

        private void Caesaren(int n)
        {
            char[] buf = input_article.ToCharArray();
            int i;

            for (i = 0; i < buf.Length; i++)
            {
                if (buf[i] >= 'a' && buf[i] <= 'z')
                {
                    int buf2 = buf[i] - 'a'+n;

                    buf[i] = (char)('a' + mod(buf2,26));
                    
                    buf2='A'-'a';
                    buf[i] = (char)(buf[i] + buf2);
                   
                
                 }

                else if (buf[i] >= 'A' && buf[i] <= 'Z')
                {

                    int buf5 = 'a' - 'A';
                    buf[i] = (char)(buf[i] + buf5);
                
                }
            
              
            
           }


            string buf3="";

            for (i = 0; i < buf.Length; i++)
                buf3 += Char.ToString(buf[i]);

            input_article = buf3;
           
            

           
        
        }

        private void Caesarde(int n)
        {
            char[] buf = input_article.ToCharArray();
            int i;

            for (i = 0; i < buf.Length; i++)
            {
                if (buf[i] >= 'A' && buf[i] <= 'Z')
                {
                    int buf2 = buf[i] - 'A' + n;

                    buf[i] = (char)('A' + mod(buf2, 26));

                    buf2 = 'a' - 'A';
                    buf[i] = (char)(buf[i] + buf2);


                }
                
                 else if (buf[i] >= 'a' && buf[i] <= 'z')
                {

                    int buf5 = 'A' - 'a';
                    buf[i] = (char)(buf[i] + buf5);
                
                }



            }


            string buf3 = "";

            for (i = 0; i < buf.Length; i++)
                buf3 += Char.ToString(buf[i]);

            input_article = buf3;





        }

        private void CaesarFix(int n)
        {
            char[] buf = input_article.ToCharArray();

            int i;

            for (i = 0; i < buf.Length; i++)
            {
                int index = Index_Return(buf[i]);

                if (index != -1)
                {
                    index = index + n;

                    buf[i] = table[mod(index, 54)];
                
                 }
            
            
            }

            string buf2 = "";

            for (i = 0; i < buf.Length; i++)
                buf2 += Char.ToString(buf[i]);

            input_article = buf2;
           


        
        
        }


        public CaesarCipher()
        {
            InitializeComponent();
            CreateTable();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                input_article = sr.ReadToEnd();
                MessageBox.Show("成功讀檔");
                textBox1.ReadOnly = true;

                sr.Close();
            
            
            }

        }

        private void CaesarCipher_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("encrytion");
            comboBox1.Items.Add("decrytion");
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.Add("general mode");
            comboBox2.Items.Add("fix mode");
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (input_article != "")
                MessageBox.Show(input_article);
            else
                MessageBox.Show("There is no string");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            input_article = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text == "encrytion" && comboBox2.Text == "general mode")
                Caesaren((int)numericUpDown1.Value);
            else if (comboBox1.Text == "decrytion" && comboBox2.Text == "general mode")
                Caesarde(-(int)numericUpDown1.Value);
            else if (comboBox1.Text == "encrytion" && comboBox2.Text == "fix mode")
                CaesarFix((int)numericUpDown1.Value);
            else
                CaesarFix(-(int)numericUpDown1.Value);
                
            
            
            MessageBox.Show(comboBox2.Text+":"+comboBox1.Text + "完成");
            
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

        private void 使用說明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string buf = "";

            buf += "這是一個Caesar 的加解密器\n";
            buf += "這邊提供兩種mode:\n";
            buf += "mode 1:general mode\n";
            buf += "將小寫做出位移並轉成大寫，而原本大寫則直接轉成小寫\n";
            buf += "mode 2:fix mode\n";
            buf += "將大寫小寫+','+'.'寫為一個table，來做位移\n";
            buf += "本程式有提供讀寫功能，如果想測試程式，可以透過右邊的textbox\n";
            buf += "當你mode和加解密選擇完成，按下start即可完成\n";
            buf += "而show string則是顯示你將要存取的檔案的string\n";
            buf += "\n";
            buf += "作者:施智偉 SHIH-JHIH-WEI\n";
            buf += "學歷:長庚大學電子工程學系四年級甲班\n";
            buf += "科目:網路安全概論\n";
            buf += "指導老師:黎明富\n";
            buf += "完成時間:2016/6/8 00:38\n";

            MessageBox.Show(buf);

        }
    }
}
