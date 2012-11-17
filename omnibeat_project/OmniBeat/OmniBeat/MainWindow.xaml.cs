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
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace OmniBeat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Multitouch.Framework.WPF.Controls.Window
    {
        private static int MAX_BEATS = 8;
        InteractiveSpaceProvider spaceProvider;
        private IWavePlayer waveOut;
        private DrumPattern pattern;
        private DrumPatternSampleProvider patternSequencer;
        private int tempo;
        DrumKit kit = new DrumKit();
        Boolean play = false;
        Boolean stop = false;
        private static Boolean[] kick = new Boolean[MAX_BEATS];
        private static Boolean[] snare = new Boolean[MAX_BEATS];
        private static Boolean[] closedHats = new Boolean[MAX_BEATS];
        private static Boolean[] openHats = new Boolean[MAX_BEATS];

        private int selectedKit = 0;
        private Button[] instrumentButtonArr = new Button[4];
        private Button[] beatButtonArr = new Button[MAX_BEATS];

        private Boolean[][] drumBeats = { kick, snare, closedHats, openHats };
        
        public MainWindow()
        {
            InitializeComponent();

            // *************************************************************
            // Uses the Person class in the Window code-behind
            
            //Person person = new Person();
            //this.DataContext = person;
            // *************************************************************

            MultitouchScreen.AllowNonContactEvents = true;

            spaceProvider = new InteractiveSpaceProviderDLL();
            spaceProvider.Connect();

            for (int i = 0; i < MAX_BEATS; i++)
            {
                kick[i] = false;
                snare[i] = false;
                closedHats[i] = false;
                openHats[i] = false;
            }
            var notes = new string[] { "Kick", "Snare", "Closed Hats", "Open Hats" };
            this.pattern = new DrumPattern(notes, MAX_BEATS);
            
            //16 beat stuff
            //this.pattern[0, 0] = this.pattern[0, 8] = 127;
            //this.pattern[1, 4] = this.pattern[1, 12] = 127;
            //for (int n = 0; n < pattern.Steps; n++)
            //{
                //this.pattern[2, n] = 127;
            //}

            instrumentButtonArr[0] = kickSelectButton;
            instrumentButtonArr[1] = snareSelectButton;
            instrumentButtonArr[2] = closedHatsSelectButton;
            instrumentButtonArr[3] = openHatsSelectButton;

            beatButtonArr[0] = beatButton1;
            beatButtonArr[1] = beatButton2;
            beatButtonArr[2] = beatButton3;
            beatButtonArr[3] = beatButton4;
            beatButtonArr[4] = beatButton5;
            beatButtonArr[5] = beatButton6;
            beatButtonArr[6] = beatButton7;
            beatButtonArr[7] = beatButton8;


            this.tempo = 100;
            //Uncomment these lines to draw fingers on the projected screen
            //spaceProvider.CreateFingerTracker();
            //vizLayer.SpaceProvider = spaceProvider;
        }

        private void playButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            play = !play;
            if (play)
            {
                b.Background = Brushes.OrangeRed;
                Console.WriteLine("Playing");
                Play();
                stopButton.Background = Brushes.White;
                stop = false;
            }
            else
            {
                b.Background = Brushes.White;
                Stop();
            }
        }
        /*
        private void playButton_ContactRemoved(object sender, ContactEventArgs e)
        {
            Button b = (Button)sender;
            Console.Out.WriteLine("button Released:" + b.Name);
            if (b.DataContext.Equals("#"))
            {
                b.Background = Brushes.Black;
            }
            else b.Background = Brushes.White;

        }
         * */

        private void stopButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            stop = !stop;
            if (stop)
            {
                b.Background = Brushes.OrangeRed;
                Console.WriteLine("Stop");
                Stop();
                playButton.Background = Brushes.White;
                play = false;
            }
            else b.Background = Brushes.White;
        }

        private void instrumentSelectButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            
            //change prev selected back to whtie 
            instrumentButtonArr[selectedKit].Background = Brushes.White;
            selectedKit = int.Parse(b.Tag.ToString());
            b.Background = Brushes.OrangeRed;
            Console.WriteLine(b.Name);

            //reset beat buttons
            Boolean[] buttonStates = drumBeats[selectedKit];
            for (int i = 0; i < buttonStates.Length; i++)
            {
                if (buttonStates[i])
                    beatButtonArr[i].Background = Brushes.OrangeRed;
                else
                    beatButtonArr[i].Background = Brushes.White;
            }
        }

        private void beatButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            int index = int.Parse(b.Tag.ToString());
            Console.WriteLine(b.Tag.ToString());
            Boolean[] currInstrument = drumBeats[selectedKit];
            currInstrument[index] = !currInstrument[index];
            if (currInstrument[index])
            {
                pattern[selectedKit, index] = 127;
                b.Background = Brushes.OrangeRed;
                Console.WriteLine("Kick " + index);
            }
            else
            {
                pattern[selectedKit, index] = 0;
                b.Background = Brushes.White;
            }
        }

        /*private void kickButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            int index = int.Parse(b.Tag.ToString());
            Console.WriteLine(b.Tag.ToString());
            kick[index] = !kick[index];
            if (kick[index])
            {
                pattern[0, index] = 127;
                b.Background = Brushes.OrangeRed;
                Console.WriteLine("Kick " + index);
            }
            else
            {
                pattern[0, index] = 0;
                b.Background = Brushes.White;
            }
        }

        private void snareButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            Console.WriteLine(b.Tag.ToString());
            int index = int.Parse(b.Tag.ToString());
            snare[index] = !snare[index];

            if (snare[index])
            {
                pattern[1, index] = 127;
                b.Background = Brushes.OrangeRed;
                Console.WriteLine("Snare " + index);
            }
            else
            {
                pattern[1, index] = 0;
                b.Background = Brushes.White;
            }
        }

        private void closedHatsButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            int index = int.Parse(b.Tag.ToString());
            closedHats[index] = !closedHats[index];
            if (closedHats[index])
            {
                pattern[2, index] = 127;
                b.Background = Brushes.OrangeRed;
                Console.WriteLine("Closed Hats " + index);
            }
            else
            {
                pattern[2, index] = 0;
                b.Background = Brushes.White;
            }
        }

        private void openHatsButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            int index = int.Parse(b.Tag.ToString());
            openHats[index] = !openHats[index];
            if (openHats[index])
            {
                pattern[3, index] = 127;
                b.Background = Brushes.OrangeRed;
                Console.WriteLine("Open Hats " + index);
            }
            else
            {
                pattern[3, index] = 0;
                b.Background = Brushes.White;
            }
        }*/

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            spaceProvider.Close();
        }

        private void vizLayer_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Play()
        {
            if (waveOut != null)
            {
                Stop();
            }
            waveOut = new WaveOut();
            this.patternSequencer = new DrumPatternSampleProvider(pattern);
            this.patternSequencer.Tempo = tempo;
            IWaveProvider wp = new SampleToWaveProvider(patternSequencer);
            waveOut.Init(wp);
            waveOut.Play();
        }

        private void Stop()
        {
            if (waveOut != null)
            {
                this.patternSequencer = null;
                waveOut.Dispose();
                waveOut = null;
            }
        }

        public void Dispose()
        {
            Stop();
        }

        public int Tempo
        {
            get
            {
                return tempo;
            }
            set
            {
                if (tempo != value)
                {
                    this.tempo = value;
                    if (this.patternSequencer != null)
                    {
                        this.patternSequencer.Tempo = value;
                    }
                    //RaisePropertyChanged("Tempo");
                }
            }
        }

     /*   private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;*/
    }
}
