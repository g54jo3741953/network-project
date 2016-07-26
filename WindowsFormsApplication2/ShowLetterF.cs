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
    public partial class ShowLetterF : Form
    {
        private string input_article="";
        private int[] letternumber = new int[26];
        private char[] letter = {'a','b','c','d','e','f','g','h','i','j','k','l','m','n'
                               ,'o','p','q','r','s','t','u','v','w','x','y','z'};


        private void Count_Number()
        {
            char[] buf = input_article.ToCharArray();
            int i;

            for (i = 0; i < buf.Length; i++)
            { 
                if(buf[i]>='A' && buf[i]<='Z')
                {
                int buf2=buf[i]-'A';
                letternumber[buf2] += 1;
                
                }


                else if (buf[i] >= 'a' && buf[i] <= 'z')
                {

                    int buf2 = buf[i] - 'a';
                    letternumber[buf2] += 1;
                
                }
                  
            
            
            
            }

        
        }


        public ShowLetterF()
        {
            InitializeComponent();
            chart1.Visible = false;
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



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             
            if (input_article != "")
            {
                chart1.Visible = true;
                chart1.Series["data"].Points.Clear();

               

                Count_Number();



                for (int i = 0; i < 26; i++)
                    chart1.Series["data"].Points.AddXY(Char.ToString(letter[i]), letternumber[i]);

                button1.Visible = false;
            }
            else
                MessageBox.Show("You must load file");
           

            
        }

        private void 使用說明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string buf="";

            buf+="這是一個顯示字母出現頻率的程式\n";
            buf += "讀取檔案，按下draw button即可\n";
            buf += "本程式只提供一次性的畫圖，如果想重話透過目錄重新開啟即可\n";
            buf += "\n";
            buf += "作者:施智偉 SHIH-JHIH-WEI\n";
            buf += "學歷:長庚大學電子工程學系四年級甲班\n";
            buf += "科目:網路安全概論\n";
            buf += "指導老師:黎明富\n";
            buf += "完成時間:2016/6/11 23:33\n";

            MessageBox.Show(buf);

        }
    }
}
