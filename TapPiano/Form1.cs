using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TAPWin;
using System.Media;

namespace TapPiano
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.log("start sound");
            SoundPlayer player = new SoundPlayer(@"E:\Open-source\TapPiano\TapPiano\sounds/C_s.wav");
            player.Play();

        }

        private void log(string line)
        {

            this.Invoke((MethodInvoker)delegate
            {
                textBox1.AppendText(line + Environment.NewLine);
            });
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
