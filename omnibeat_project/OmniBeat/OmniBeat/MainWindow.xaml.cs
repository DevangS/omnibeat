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
        public BeatMaker beatMaker = new BeatMaker();
        public SoundClipsSelect clipSelector = new SoundClipsSelect();
        /*
        public static int MAX_BEATS = 8;
        public InteractiveSpaceProvider spaceProvider;
        public IWavePlayer waveOut;
        public static DrumPattern pattern;
        public DrumPatternSampleProvider patternSequencer;
        public TempoController tempoController;
        public PitchController pitchController;
        //private int tempo;
        DrumKit kit = new DrumKit();
        Boolean play = false;
        Boolean stop = false;

        private int selectedKit = 0;
        public static int chosenButton = 0;
        private Button[] instrumentButtonArr = new Button[4];
        private Button[] beatButtonArr = new Button[MAX_BEATS];
        public string[] notes = {"Kick", "Snare", "Closed Hat", "Open Hat", "Cymbal",
                                  "Everybody", "Oh Yeah", "OneMoreTime", "Shots", "Jerk", 
                                  "Kick", "Snare", "Closed Hat", "Open Hat", "Cymbal",
                                  "Everybody", "Oh Yeah", "OneMoreTime", "Shots", "Jerk" };
        private enum noteName {Kick, Snare, ClosedHat, OpenHat, Cymbal,
                                  Everybody, OhYeah, OneMoreTime, Shots, Jerk};
        public static int noteNum = 20;
        public int[] chosenClips = new int[] { (int)noteName.Kick, (int)noteName.Snare, (int)noteName.ClosedHat, (int)noteName.OpenHat }; 

        public Boolean[][] drumBeats;
        */
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public MainWindow()
        {
            InitializeComponent();

            Switcher.mainWindow = this;
            Switcher.Switch(this.beatMaker);
            /*
            Menu.sync(this);

            // *************************************************************
            // Uses the Person class in the Window code-behind
            
            //Person person = new Person();
            //this.DataContext = person;
            // *************************************************************

            MultitouchScreen.AllowNonContactEvents = true;

            spaceProvider = new InteractiveSpaceProviderDLL();
            spaceProvider.Connect();
            drumBeats = new Boolean[notes.Length*2][];
            chosenClips = new int[] {(int)noteName.Kick, (int)noteName.Snare, (int)noteName.ClosedHat, (int)noteName.OpenHat};


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
            for (int i = 8; i < notes.Length; i++)
            {
                Boolean[] newArray = new Boolean[MAX_BEATS];
                //make all values false
                for (int j = 0; j < MAX_BEATS; j++)
                {
                    newArray[j] = false;
                }
                drumBeats[i] = newArray;
            }


            pattern = new DrumPattern(notes, MAX_BEATS);
            this.tempoController = tempoCtrl;
            this.pitchController = pitchCtrl;
            this.pitchController.reloadState();


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

            updateSelectedClips();
            //this.tempo = 100;
            //Uncomment these lines to draw fingers on the projected screen
            //spaceProvider.CreateFingerTracker();
            //vizLayer.SpaceProvider = spaceProvider;
             */
        }


/*

        //call this method when the selected instruments have been changed
        //uses chosenClips. Alternately, maybe change it so it takes an array.
        //either way chosenClips has to change since i think Pitch bend needs
        //it too.

        // UPDATE NAMES OF SOUDN CLIP BUTTONS
        private void updateSelectedClips()
        {
            for (int i = 0; i < chosenClips.Length; i++)
            {
                Console.WriteLine("Changing button# " + i);
                int x = chosenClips[i];
                Button b = instrumentButtonArr[i];
                b.Tag = x.ToString();
                Console.WriteLine("from " + b.Content + " to " + notes[x]);
                b.Content = "" + notes[x];
            }
            selectedKit = chosenClips[0];
        }

        private void playClipButton_NewContact(object sender, NewContactEventArgs e)
        {
            Console.WriteLine("play clip");
            Button b = (Button)sender;
            int x = int.Parse(b.Tag.ToString());
            pattern[x+10, 0] = 127;
            PatternSequencer.playClip = true;
        }

        public static void clearClips()
        {
            Console.WriteLine("clear clip");
            pattern[10, 0] = 0;
            pattern[11, 0] = 0;
            pattern[12, 0] = 0;
            pattern[13, 0] = 0;
            pattern[14, 0] = 0;
            pattern[15, 0] = 0;
            pattern[16, 0] = 0;
            pattern[17, 0] = 0;
            pattern[18, 0] = 0;
            pattern[19, 0] = 0;
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

        private void soundClipsSelectButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            b.Background = Brushes.OrangeRed;
        }

        private void instrumentSelectButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            
            //change prev selected back to whtie 
            instrumentButtonArr[chosenButton].Background = Brushes.White;
            chosenButton = int.Parse( b.Name[16].ToString() ) ;
            selectedKit = int.Parse(b.Tag.ToString());

            //reload pitch
            this.pitchController.reloadState();

            updateSoundClipButtons();

            //reset beat buttons
            updateBeatButtons();
        }

        private void updateSoundClipButtons()
        {
            for (int i = 0; i < chosenClips.Length; i++)
            {
                if (i == chosenButton)
                {
                    instrumentButtonArr[i].Background = Brushes.OrangeRed;
                }
                else
                {
                    instrumentButtonArr[i].Background = Brushes.White;
                }
            }
        }

        private void updateBeatButtons()
        {
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
            this.pitchController.setPatternSequencer(this.patternSequencer);
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

        public void clearEverything() 
        {
            //iterate over each dimensions/first array dereference
            for (int i = 0; i <= drumBeats.Rank; i++)
            {
                //iterate over each element in the array at the ith dimension
                for (int j = 0; j < drumBeats.GetLength(i); j++)
                {
                    //set the jth beat for the ith instrument to false
                    drumBeats[i][j] = false;
                }
            }

            //reset drum pattern
            for (int i = 0; i < notes.Length; i++)
            {
                for (int j = 0; j < MAX_BEATS; j++)
                {
                    pattern[i, j] = 0;
                }
            }

            //reset tempo to 1
            tempoController.Tempo = 1;

            //recolour GUI beat buttons
            updateBeatButtons();
        }
        */
    }
}
