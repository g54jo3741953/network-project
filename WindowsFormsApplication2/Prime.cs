using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Prime : Form
    {
        string buf = "";

        public Prime()
        {
            InitializeComponent();
           
        }

        private bool check_prime(UInt64 input)
        {
            buf = "";

            UInt64 i=2;
            buf += input.ToString() + " = " + "1 * " + input.ToString()+"\r\n";
            if (input == 2)
                return true;

            string buf2 = "";
            while(i*i<=input)
            {
                if (input % i == 0)
                    buf2 += input.ToString() + " = " + i.ToString() + " * " + (input / i).ToString() + "\r\n";

                i++;
            }
            buf = buf+buf2;

            if (buf2 == "")
                return true;
            else
                return false;
        
        
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (check_prime((UInt64)numericUpDown1.Value))
                buf = numericUpDown1.Value.ToString() + " is prime"+"\r\n"+ buf;
            else
                buf = numericUpDown1.Value.ToString() + " is not prime" + "\r\n" + buf;

            textBox1.Text = buf;
        }

        private void 使用說明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string buf2="";
            buf2 +="這是一個確認輸入的數是否為質數的程式\n";
            buf2 += "並顯示其所有分解的組合\n";
            buf2 += "\n";
            buf2 += "作者:施智偉 SHIH-JHIH-WEI\n";
            buf2 += "學歷:長庚大學電子工程學系四年級甲班\n";
            buf2 += "科目:網路安全概論\n";
            buf2 += "指導老師:黎明富\n";
            buf2 += "完成時間:2016/6/10 21:46\n";

            MessageBox.Show(buf2);
        }
    }
}
