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
using System.IO;
using TAPWin;
using System.Reflection;


namespace TapPiano
{
    public partial class Form1 : Form
    {
        private static SoundPlayer player;
        private const int TOTAL_WAVES = 31;
        private const int NUM_OF_NOTES = 5;
        private const double BASE_FREQ = 440;
        private const double alternate_freq = 128;
        private IWave[] waves;
        public Form1()
        {
            InitializeComponent();
            IntializedSoundProfile(BASE_FREQ);
            IntializedTap();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void IntializedTap()
        {
            TAPManager.Instance.OnTapped += this.OnTapped;
            TAPManager.Instance.OnTapConnected += this.OnTapConnected;
            TAPManager.Instance.SetDefaultInputMode(TAPInputMode.Controller(), true);
            TAPManager.Instance.Start();
        }

        private void OnTapConnected(string identifier, string name, int fw)
        {
            this.log(identifier + " connected. (" + name + ", fw " + fw.ToString() + ")");
        }

        private void OnTapped(string identifier, int tapcode)
        {
            this.log(identifier + " tapped " + tapcode.ToString());
            IWave wave = waves[tapcode];
            //found the correct wave to use 
            //now we need to create the .WAV file in memory and enter it to memorystream
            short[] data = wave.generateData();
            WaveHeader header = new WaveHeader();
            FormatChunk format = new FormatChunk();
            DataChunk data_chunk = new DataChunk();
            List<Byte> tempBytes = new List<byte>();
            data_chunk.AddSampleData(data, data);
            header.FileLength += format.Length() + data_chunk.Length();
            tempBytes.AddRange(header.GetBytes());
            tempBytes.AddRange(format.GetBytes());
            tempBytes.AddRange(data_chunk.GetBytes());
            Byte[] bytes = tempBytes.ToArray();
            // add it to a new player and start playing
            player.Dispose();
            player.Stream = (new MemoryStream(bytes));

            player.PlayLooping();


        }

        private void IntializedSoundProfile(double base_f)
        {
            //I didn't want to calculate the total cominations for maximum of choose(5,i)
            // as it requires to implement at least backtracking algorithm that by using
            // DFS finds the combinations 
            // doing it like I did is a little bit less readable but it is easy to digest
            //initialize the sound profile for all fingures and acords 
            //base (lowest) note
            double freq = base_f;
            double step = Math.Pow(2, 1.0 / 5.0);
            //a single note
            IWave[] notes = new IWave[NUM_OF_NOTES];
            for (int i = 0; i < NUM_OF_NOTES; i++)
            {
                notes[i] = new SineGenerator(freq, (ulong)(10E6));
                freq *= step;
            }
            createAcords(notes);
            player = new SoundPlayer();



        }

        private void createAcords(IWave[] notes) 
        {
            int index;
            waves = new IWave[TOTAL_WAVES+1];
            for (int i = 0; i < NUM_OF_NOTES; i++)
            {
                waves[1 << i] = notes[i];
            }
            //acords of two 
            for (int i = 0; i < NUM_OF_NOTES - 1; i++)
            {
                for (int j = i + 1; j < NUM_OF_NOTES; j++)
                {
                    index = (1 << i) | (1 << j);
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
                        index = (1 << i1) | (1 << i2) | (1 << i3);
                        waves[index] = new CompoundWave(new IWave[] { waves[1 << i1],
                            waves[1 << i2], waves[1 << i3] });
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
                waves[index] = new CompoundWave(new IWave[] { waves[1 << comb[0]],
                    waves[1 << comb[1]], waves[1 << comb[2]], waves[1 << comb[3]] });
            }

            //acord of five
            index = 31;
            waves[index] = new CompoundWave(new IWave[] { waves[1], waves[2], waves[4],
                waves[8], waves[16] });

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.log("start sound");
            OnTapped("test", 1);

        }

        private void log(string line)
        {

            this.Invoke((MethodInvoker)delegate
            {
                Logger.AppendText(line + Environment.NewLine);
            });
        }



        private void stopSound_click(object sender, EventArgs e)
        {
            player.Stop();
        }


        private void KeyDown_handle(object sender, KeyEventArgs e)
        {
            //this.log(e.KeyCode.ToString());
            if (e.KeyCode == Keys.S)
            {
                this.log("stopped sound using S key.");

                try { player.Stop(); }
                catch { }
            }
            else if (e.KeyCode == Keys.D1)
            {
                this.OnTapped("keyboard tap ", 1);
            }
            else if (e.KeyCode == Keys.D2)
            {
                this.OnTapped("keyboard tap ", 2);
            }
            else if (e.KeyCode == Keys.D3)
            {
                this.OnTapped("keyboard tap ", 4);
            }
            else if (e.KeyCode == Keys.D4)
            {
                this.OnTapped("keyboard tap ", 8);
            }
            else if (e.KeyCode == Keys.D5)
            {
                this.OnTapped("keyboard tap ", 16);
            }
            else if (e.KeyCode == Keys.Z)
            {
                this.OnTapped("keyboard tap ", 3);
            }
            else if (e.KeyCode == Keys.X)
            {
                this.OnTapped("keyboard tap ", 5);
            }
            else if (e.KeyCode == Keys.A)
            {
                
                int tmp = (this.comboBox1.SelectedIndex - 1);
                this.comboBox1.SelectedIndex = tmp == -1 ?
                    this.comboBox1.Items.Count - 1 :
                    tmp;
            }
            else if (e.KeyCode == Keys.D)
            {
                this.comboBox1.SelectedIndex = (this.comboBox1.SelectedIndex + 1) % this.comboBox1.Items.Count;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntializedSoundProfile(double.Parse(comboBox1.SelectedItem.ToString()));
            this.log("changed sound profile");
        }

        private void myCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Logger_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            short[] data = (new SineGenerator(BASE_FREQ, (ulong)120E6)).generateData();
            WaveHeader header = new WaveHeader();
            FormatChunk format = new FormatChunk();
            DataChunk data_chunk = new DataChunk();
            List<Byte> tempBytes = new List<byte>();
            data_chunk.AddSampleData(data, data);
            header.FileLength += format.Length() + data_chunk.Length();
            tempBytes.AddRange(header.GetBytes());
            tempBytes.AddRange(format.GetBytes());
            tempBytes.AddRange(data_chunk.GetBytes());
            Byte[] bytes = tempBytes.ToArray();
            // add it to a new player and start playing
            player = new SoundPlayer(new MemoryStream(bytes));
            player.PlayLooping();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
