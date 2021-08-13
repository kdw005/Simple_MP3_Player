using NAudio.Wave;
using System;
using System.IO;
using Player.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Player
{
    public partial class Form1 : Form
    {
        string file;
        IWavePlayer wavePlayer;
        WaveStream mainOutputStream;
        WaveChannel32 volumeStream;
        WaveOut waveOutDevice;
        ButtonControl btc = new ButtonControl(false);
        List<string> Path = new List<string>();
        List<string> Nomes = new List<string>();
        
        

        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            waveOutDevice = new WaveOut();
            dataGridView1.DefaultCellStyle.Font = new Font("Garamond", 10, style: FontStyle.Italic);
            btc.PlayStatus = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string i in openFileDialog.FileNames)
                {
                    Path.Add(i);
                }
                string[] chs = openFileDialog.FileNames;
                string[] nomes = openFileDialog.SafeFileNames;
                dataGridView1.Columns.Add("caminho","Caminho");
                dataGridView1.Columns.Add("Nome", "Nome");
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 500;
                for(int cont = 0; cont < nomes.Length; cont++)
                {
                    dataGridView1.Rows.Add(chs[cont],nomes[cont]);
                    Path.Add(chs[cont]);
                }       
            }
        }
        private WaveStream CreateInputStream(string fileName)
        {
            WaveChannel32 inputStream;
            if (fileName.EndsWith(".mp3"))
            {
                WaveStream mp3Reader = new Mp3FileReader(fileName);
                inputStream = new WaveChannel32(mp3Reader);
            }
            else
            {
                throw new InvalidOperationException("Unsuppordet extension");
            }
            volumeStream = inputStream;
            return volumeStream;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                waveOutDevice.Init(mainOutputStream);
                btc.UpdatePlayStatus();
                if (btc.PlayStatus == false)
                {
                    waveOutDevice.Play();
                    this.btnPlay.Text = "Pause";
                    this.btnPlay.Image = System.Drawing.Image.FromFile(@"C:\Users\Eduardo\source\repos\Player\Player\Resources\1486348534-music-pause-stop-control-play_80459.ico");

                }
                else
                {
                    waveOutDevice.Pause();
                    this.btnPlay.Text = "Play";
                     this.btnPlay.Image = System.Drawing.Image.FromFile(@"C:\Users\Eduardo\source\repos\Player\Player\Resources\1486348532-music-play-pause-control-go-arrow_80458.ico");

                }
            }
            else
            {
                lbnome.Text = "Please, select a music - tip: use open button";
            }
           
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            waveOutDevice.Stop();
            btnPlay.Text = "Play";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            string file = Path[dataGridView1.SelectedRows[0].Index];

            if (File.Exists(file))
            {
                
                mainOutputStream = CreateInputStream(file);

                lbnome.Text = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[1].Value.ToString();
                


            }
            
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows[0].Index > 0)
            {
                string file = Path[dataGridView1.SelectedRows[0].Index];
                int num = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[num - 1].Selected = true;

            }
        }
    }
}
