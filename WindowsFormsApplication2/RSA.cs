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
    public partial class RSA : Form
    {
        private string input_article;
        private UInt64 n;
        private UInt64 phi_n;
        private int private_key;
        private int public_key;

        private void Rsa(int key, UInt64 n)
        {
            
            char[] buf2 = input_article.ToCharArray();
            int i;


            string buf_string = "";

            for (i = 0; i < buf2.Length; i++)
            {
                UInt64 buf3 = (UInt64)buf2[i];
                buf3 = Fast_exp_mod(buf3, key, n);
                buf2[i] = (char)buf3;
                

            }

            for (i = 0; i < buf2.Length; i++)
                buf_string += Char.ToString(buf2[i]);

            input_article = buf_string;


        
        }

        private UInt64 Fast_exp_mod(UInt64 a, int d, UInt64 n)
        {


            int digit=1;
            int temp=1;

            while (temp < d)
            {   
            
                temp = temp * 2;
    
                digit = digit + 1;
            }

            digit = digit - 1;




            UInt64[] modular = new UInt64[digit];
            UInt64[] table = new UInt64[digit];
            int i;
            modular[0]=(UInt64)a%n;
            table[0] = 1;
            for (i = 1; i < digit; i++)
            {
                modular[i] = (modular[i - 1] * modular[i-1]) % n;
                table[i] = 2 * table[i - 1];
            }

            UInt64 buf = 0;
            UInt64 answer = 1;

            for (i = digit - 1; i >= 0; i--)
            {

                if (buf + table[i] <= (UInt64)d)
                {

                    buf += table[i];
                 

                    answer = (answer * modular[i]) % n;
                
                
                }
            
            }

            return answer;
            


        }

        public RSA()
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

        private void RSA_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Encrytion");
            comboBox1.Items.Add("Decrytion");
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.Add("Public key");
            comboBox2.Items.Add("Private key");
            comboBox2.SelectedIndex = 1;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown3.Value = numericUpDown1.Value * numericUpDown2.Value;
            numericUpDown4.Value = (numericUpDown1.Value - 1) * (numericUpDown2.Value - 1);
            n = (UInt64)numericUpDown3.Value;
            phi_n = (UInt64)numericUpDown4.Value;
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            int a;

            a = AmodNinverse.inverse((int)numericUpDown5.Value, (int)phi_n);
            private_key = (int)numericUpDown5.Value;

            if (a != -1)
            {
                numericUpDown6.Value = (decimal)a;
                public_key = a;
            }
            else
                MessageBox.Show("public key not exist!\n" + "Please choose another private key!!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            if (comboBox2.SelectedIndex == 0)
                Rsa(private_key, n);
            else
                Rsa(public_key, n);


            MessageBox.Show(comboBox1.Text + " Finish!!");

        }

        private void 使用說明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string buf="";

            buf += "這是一個RSA public private key的加解密程式\n";
            buf += "使用者自己決定p和q(注意n>256)\n";
            buf += "程式有提供讀寫檔的功能\n";
            buf += "private key 可以自己輸入,程式會自動產生對應的public key\n";
            buf+=" 如果public key 不存在會跳出提示框\n";
            buf += "\n";
            buf += "作者:施智偉 SHIH-JHIH-WEI\n";
            buf += "學歷:長庚大學電子工程學系四年級甲班\n";
            buf += "科目:網路安全概論\n";
            buf += "指導老師:黎明富\n";
            buf += "完成時間:2016/6/10 2:30\n";

            MessageBox.Show(buf);
        }


    }
}
