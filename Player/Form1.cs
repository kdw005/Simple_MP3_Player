using NAudio.Wave;
using System;
using System.IO;
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
        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            waveOutDevice = new WaveOut();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    file = openFileDialog.FileName;
                    mainOutputStream = CreateInputStream(openFileDialog.FileName);
                    lbnome.Text = openFileDialog.SafeFileName;
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
            waveOutDevice.Init(mainOutputStream);
            waveOutDevice.Play();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            waveOutDevice.Stop();
        }
    }
}
