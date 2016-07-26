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
    public partial class AmodNinverse : Form
    {
        public AmodNinverse()
        {
            InitializeComponent();
        }

        public static int inverse(int a , int n)
        {
            int[] a3 = { 1, 0, n };
            int[] b3 = { 0, 1, a };

            while(b3[2]!=1 && b3[2]!=0){
            int buf = a3[2] / b3[2];
            
            int[] temp = { (a3[0] - buf * b3[0]), (a3[1] - buf * b3[1]), (a3[2] - buf * b3[2]) };
             a3 = b3;
             b3 = temp;
            }

            while (b3[1] < 0)
                b3[1] += n;


            if (b3[2] == 1)
                return b3[1];
            else
                return -1;
        
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int answer = inverse((int)numericUpDown1.Value, (int)numericUpDown2.Value);

            if (answer != -1)
                numericUpDown3.Value = answer;
            else
                MessageBox.Show("inverse not exist");
        }

        private void 使用說明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string buf = "";

            buf += "這是一個給定a和n，並求出ax=1(mod n)的x的程式\n";
            buf += "如果inverse不存在，會跳出對話框，告知使用者\n";
            buf += "\n";
            buf += "作者:施智偉 SHIH-JHIH-WEI\n";
            buf += "學歷:長庚大學電子工程學系四年級甲班\n";
            buf += "科目:網路安全概論\n";
            buf += "指導老師:黎明富\n";
            buf += "完成時間:2016/6/8 13:00\n";

            MessageBox.Show(buf);
        }
    }
}
