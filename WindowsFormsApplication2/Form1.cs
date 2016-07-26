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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CaesarCipher caeser = new CaesarCipher();
            caeser.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AmodNinverse ninverse = new AmodNinverse();
            ninverse.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NSubstitutionCipher sub = new NSubstitutionCipher();
            sub.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RC4 rc4 = new RC4();
            rc4.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RSA rsa = new RSA();
            rsa.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Prime prime = new Prime();
            prime.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowLetterF showletterf = new ShowLetterF();
            showletterf.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            HillCipher hillcipher = new HillCipher();
            hillcipher.Show();
        }
    }
}
