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
        public DrumPatternSampleProvider patternSequencer;
        public TempoController tempoController;
        //private int tempo;
        DrumKit kit = new DrumKit();
        Boolean play = false;
        Boolean stop = false;

        private int selectedKit = 0;
        private int chosenButton = 0;
        private int[] chosenClips = { 0, 1, 2, 3 };
        private Button[] instrumentButtonArr = new Button[4];
        private Button[] beatButtonArr = new Button[MAX_BEATS];
        private string[] notes = {"Kick", "Snare", "Closed Hat", "Open Hat", "Cymbal",
                                  "Everybody", "Oh Yeah", "OneMoreTime", "Scratch", "Jerk" };

        private Boolean[][] drumBeats;
        
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
            drumBeats = new Boolean[notes.Length][];

            //create an boolean array for each sample
            for (int i = 0; i < notes.Length; i++)
            {
                Boolean[] newArray = new Boolean[MAX_BEATS];
                //make all values false
                for (int j = 0; j < MAX_BEATS; j++)
                {
                    newArray[j] = false;
                }
                drumBeats[i] = newArray;
            }



            this.pattern = new DrumPattern(notes, MAX_BEATS);
            this.tempoController = tempoCtrl;
            //16 beat stuff
            //this.pattern[0, 0] = this.pattern[0, 8] = 127;
            //this.pattern[1, 4] = this.pattern[1, 12] = 127;
            //for (int n = 0; n < pattern.Steps; n++)
            //{
                //this.pattern[2, n] = 127;
            //}

            instrumentButtonArr[0] = clipSelectButton0;
            instrumentButtonArr[1] = clipSelectButton1;
            instrumentButtonArr[2] = clipSelectButton2;
            instrumentButtonArr[3] = clipSelectButton3;

            beatButtonArr[0] = beatButton1;
            beatButtonArr[1] = beatButton2;
            beatButtonArr[2] = beatButton3;
            beatButtonArr[3] = beatButton4;
            beatButtonArr[4] = beatButton5;
            beatButtonArr[5] = beatButton6;
            beatButtonArr[6] = beatButton7;
            beatButtonArr[7] = beatButton8;


            //this.tempo = 100;
            //Uncomment these lines to draw fingers on the projected screen
            //spaceProvider.CreateFingerTracker();
            //vizLayer.SpaceProvider = spaceProvider;
        }

        //call this method when the selected instruments have been changed
        private void updateSelectedClips()
        {
            for (int i = 0; i < chosenClips.Length; i++)
            {
                int x = chosenClips[i];
                Button b = instrumentButtonArr[i];
                b.Tag = x.ToString();
                b.Name = notes[i];
            }
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
                //stopButton.Background = Brushes.White;
                stop = false;
            }
            else
            {
                b.Background = Brushes.White;
                Stop();
            }
        }

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
            instrumentButtonArr[chosenButton].Background = Brushes.White;
            chosenButton = int.Parse( b.Name[16].ToString() ) ;
            selectedKit = int.Parse(b.Tag.ToString());
   
            b.Background = Brushes.OrangeRed;
            Console.WriteLine(b.Name);
            Console.WriteLine(b.Tag + " " +selectedKit);

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
                Console.WriteLine("button " + index);
            }
            else
            {
                pattern[selectedKit, index] = 0;
                b.Background = Brushes.White;
            }
        }

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
            //this.patternSequencer.Tempo = tempo;
            this.tempoController.setPatternSequencer(ref this.patternSequencer);
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

        
    }
}
