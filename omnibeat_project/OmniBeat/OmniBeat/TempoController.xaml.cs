using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Multitouch.Framework.WPF.Input;
using InteractiveSpace.SDK;
using InteractiveSpace.SDK.DLL;

namespace OmniBeat
{
    /// <summary>
    /// Interaction logic for TouchSlider.xaml
    /// </summary>
    public partial class TempoController : UserControl
    {
        private static int MAX_TEMPO = 8;
        public bool isSliding;
        int tempo;
        int incDec;
        private Rectangle[] tempoArr = new Rectangle[MAX_TEMPO];
        private DrumPatternSampleProvider patternSequencer;
        private int[] value = { 33, 50, 66, 80, 100, 125, 150, 175, 200 };

        public TempoController()
        {
            InitializeComponent();
            this.tempo = 4;

            tempoArr[0] = tempo1;
            tempoArr[1] = tempo2;
            tempoArr[2] = tempo3;
            tempoArr[3] = tempo4;
            tempoArr[4] = tempo5;
            tempoArr[5] = tempo6;
            tempoArr[6] = tempo7;
            tempoArr[7] = tempo8;
        }

        public void setPatternSequencer(ref DrumPatternSampleProvider pSequencer)
        {
            this.patternSequencer = pSequencer;
        }

        private void tempoButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;

            if (tempo > 0  && tempo < MAX_TEMPO)
            {
                incDec = int.Parse(b.Tag.ToString());

                if (incDec == 0)
                {
                    tempo--;
                }
                else if (incDec == 1)
                {
                    tempo++;
                }

                updateTempo();
            }
        }

        public void updateTempo()
        {
            if (this.patternSequencer != null)
            {
                this.patternSequencer.Tempo = value[tempo];
            }
            colorTempo();
        }

        private void colorTempo()
        {
            int counter = tempo;
            for (int i = 0; i < tempoArr.Length; i++) 
            {
                if (counter >= 0)
                {
                    tempoArr[i].Fill = Brushes.Turquoise;
                    counter--;
                }
                else 
                {
                    tempoArr[i].Fill = Brushes.White;
                }
            }
        }

        public int Tempo
        {
            get
            {
                return value[tempo];
            }
            set
            {
                if (tempo != value)
                {
                    this.value[tempo] = value;
                    if (this.patternSequencer != null)
                    {
                        this.patternSequencer.Tempo = value;
                    }
                }
            }
        }
    }
}
