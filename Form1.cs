using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NAudio.Wave;
using NAudio.CoreAudioApi;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {

        private IWavePlayer waveOutDevice;
        private WaveMixerStream32 mixer;
        private string audioSourcePath = "C:\\Users\\H Peng\\Documents\\Visual Studio 2010\\Projects\\WindowsFormsApplication1\\WindowsFormsApplication1\\pianoSamples\\";
        //private string[] fileNames = { "middleC.wav", "middleD.wav", "middleE.wav", "middleF.wav", "middleG.wav", "middleA.wav", "middleB.wav" };
        private string[] fileNames = { "C5", "D5", "E5", "F5", "G5", "A5", "B5", "C6" };
        private Key[] keyArray = new Key[8];
        


        public Form1()
        {
            InitializeComponent();
            //Setup the Mixer
            mixer = new WaveMixerStream32();
            mixer.AutoStop = false;


            //initialize keys
            for (int i = 0; i < keyArray.Length; i++)
            {
                keyArray[i] = new Key(audioSourcePath + fileNames[i] + ".wav");
                mixer.AddInputStream(keyArray[i].channelStream);
            }

            this.KeyDown += new KeyEventHandler(form_KeyDown);
            this.KeyUp += new KeyEventHandler(form_KeyUp);

            if (waveOutDevice == null)
            {
                waveOutDevice = new WaveOut();
                waveOutDevice.Init(mixer);
                mixer.Position = mixer.Length;
                waveOutDevice.Play();
            }




        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    statusC.Text = "ON";
                    keyArray[0].setPlayState(true);
                    break;
                case Keys.S:
                    statusD.Text = "ON";
                    keyArray[1].setPlayState(true);
                    break;
                case Keys.D:
                    statusE.Text = "ON";
                    keyArray[2].setPlayState(true);
                    break;
                case Keys.F:
                    statusF.Text = "ON";
                    keyArray[3].setPlayState(true);
                    break;
                case Keys.G:
                    statusG.Text = "ON";
                    keyArray[4].setPlayState(true);
                    break;
                case Keys.H:
                    statusA.Text = "ON";
                    keyArray[5].setPlayState(true);
                    break;
                case Keys.J:
                    statusB.Text = "ON";
                    keyArray[6].setPlayState(true);
                    break;
                case Keys.K:
                    statusC6.Text = "ON";
                    keyArray[7].setPlayState(true);
                    break;
                default:
                    break;
            }

        }

        private void form_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    statusC.Text = "OFF";
                    keyArray[0].setPlayState(false);
                    break;
                case Keys.S:
                    statusD.Text = "OFF";
                    keyArray[1].setPlayState(false);
                    break;
                case Keys.D:
                    statusE.Text = "OFF";
                    keyArray[2].setPlayState(false);
                    break;
                case Keys.F:
                    statusF.Text = "OFF";
                    keyArray[3].setPlayState(false);
                    break;
                case Keys.G:
                    statusG.Text = "OFF";
                    keyArray[4].setPlayState(false);
                    break;
                case Keys.H:
                    statusA.Text = "OFF";
                    keyArray[5].setPlayState(false);
                    break;
                case Keys.J:
                    statusB.Text = "OFF";
                    keyArray[6].setPlayState(false);
                    break;
                case Keys.K:
                    statusC6.Text = "OFF";
                    keyArray[7].setPlayState(false);
                    break;
                default:
                    break;
            }

        }

    }

}
