using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using TAPWin;


namespace TapPiano
{
    public partial class Form1 : Form
    {
        private const int TOTAL_WAVES = 31;
        private const int NUM_OF_NOTES = 5;
        private const double BASE_FREQ = 440;
        private const uint SAMPLE_RATE = 16;
        private IWave[] waves;
        public Form1()
        {
            InitializeComponent();
            IntializedSoundProfile();
            IntializedTap();
        }

        private void IntializedTap()
        {
            TAPManager.Instance.OnTapped += this.OnTapped;

        }

        private void OnTapped(string identifier, int tapcode)
        {
            this.log(identifier + " tapped " + tapcode.ToString());
            IWave wave = waves[tapcode];
            short[] data = wave.generateData();

            SoundPlayer player;

        }

        private void IntializedSoundProfile()
        {
            //I didn't want to calculate the total cominations for maximum of choose(5,i)
            // as it requires to implement at least backtracking algorithm that by using DFS finds the combinations 
            // doing it like I did is a little bit less readable but it is easy to digest
            //initialize the sound profile for all fingures and acords 
            waves = new IWave[TOTAL_WAVES];
            //base (lowest) note
            double freq = BASE_FREQ;
            double step = Math.Pow(2, 1.0 / 5.0);
            int index = 1;
            //a single note
            for (int i = 0; i < NUM_OF_NOTES; i++)
            {

                //TODO fix period length to lower then sec (maybe nano or mil

                waves[index] = new SineGenerator(freq, SAMPLE_RATE, (UInt16)(1.0 / BASE_FREQ));
                index = index << 1;
                freq *= step;
            }
            //acords of two 
            for (int i = 0; i < NUM_OF_NOTES - 1; i++)
            {
                for (int j = i + 1; j < NUM_OF_NOTES; j++)
                {
                    index = 1 << i + 1 << j;
                    waves[index] = new CompoundWave(new IWave[] { waves[1 << i], waves[1 << j] });
                }
            }
            //acords of three
            for (int i1 = 0; i1 < NUM_OF_NOTES - 2; i1++)
            {
                for (int i2 = i1 + 1; i2 < NUM_OF_NOTES - 1; i2++)
                {
                    for (int i3 = i2 + 1; i3 < NUM_OF_NOTES; i3++)
                    {
                        index = 1 << i1 + 1 << i2 + 1 << i3;
                        waves[index] = new CompoundWave(new IWave[] { waves[1 << i1], waves[1 << i2], waves[1 << i3] });
                    }
                }
            }
            //acords of four
            //simply five of those
            int[][] indicies = new int[][] {
                new int[] { 0, 1, 2, 3 },
                new int[] { 0, 1, 2, 4 },
                new int[] { 0, 1, 3, 4 },
                new int[] { 0, 2, 3, 4 },
                new int[] { 1, 2, 3, 4 }
            };
            foreach (int[] comb in indicies)
            {
                index = 0;
                foreach (int ind in comb)
                    index |= (1 << ind);
                waves[index] = new CompoundWave(new IWave[] { waves[comb[0]], waves[comb[1]], waves[comb[2]], waves[comb[3]], waves[comb[4]] });
            }

            //acord of five
            index = 31;
            waves[index] = new CompoundWave(new IWave[] { waves[1], waves[2], waves[4], waves[8], waves[16] });


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
