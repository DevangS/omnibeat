using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public bool isSynced;
        public bool ioLock;
        public bool saved;
        public bool cleared;
        private String save1Location;
        private BeatMaker BeatMaker;

        public Menu()
        {
            InitializeComponent();
            isSynced = false;
            ioLock = false;
            save1Location = Directory.GetCurrentDirectory() + "/save1";
        }

        public void sync(BeatMaker BeatMaker)
        {
            Console.WriteLine("\n***** inside sync*****\n");
            isSynced = true;
            this.BeatMaker = BeatMaker;
        }

        private void openButton_NewContact(object sender, NewContactEventArgs e)
        {
            Console.WriteLine("Open Button Pressed");
            Button b = (Button)sender;
            if (isSynced && !ioLock && saved)
            {
                if (BeatMaker.play)
                    BeatMaker.Stop();
                
                ioLock = true;
                loadFromFile(save1Location);
                BeatMaker.selectedKit = 0;
                BeatMaker.updateBeatButtons();
                BeatMaker.updateSoundClipButtons();
                BeatMaker.tempoController.updateTempo();
                b.Background = Brushes.DarkTurquoise;
                b.Foreground = Brushes.White;
                saved = false;
                ioLock = false;

                BeatMaker.Play();
            }
        }

        private void saveButton_NewContact(object sender, NewContactEventArgs e)
        {
            Console.WriteLine("Save Button Pressed");
            Button b = (Button)sender;
            if (isSynced && !ioLock)
            {
                ioLock = true;
                if (BeatMaker.play)
                    BeatMaker.Stop();
                saveToFile(save1Location);
                b.Background = Brushes.DarkTurquoise;
                b.Foreground = Brushes.White;
                saved = true;
                ioLock = false;

                BeatMaker.Play();
            }
        }

        private void clearButton_NewContact(object sender, NewContactEventArgs e)
        {
            Console.WriteLine("Clear Button Pressed");
            Button b = (Button)sender;
            b.Background = Brushes.DarkTurquoise;
            b.Foreground = Brushes.White;
            BeatMaker.clearEverything();
        }

        private void loadFromFile(String filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
          
            //read tempo
            int nextLine = Array.IndexOf(lines, "tempo") + 1;
            BeatMaker.tempoController.Tempo = Convert.ToInt32(lines[nextLine]);

            //read chosenButton
            nextLine = Array.IndexOf(lines, "chosenButton") + 1;
            BeatMaker.chosenButton = Convert.ToInt32(lines[nextLine]);

            //read chosenClips
            nextLine = Array.IndexOf(lines, "chosenClips") + 1;
            String[] clips = lines[nextLine].Split(' ');
            for (int i = 0; i < BeatMaker.chosenClips.Length; i++)
            {
                BeatMaker.chosenClips[i] = Convert.ToInt32(clips[i]);
            }

            //read drumbeats
            nextLine = Array.IndexOf(lines, "drumBeats") + 1;
            for (int i = 0; i < BeatMaker.drumBeats.GetLength(0); i++)
            {
                try
                {
                    //get all beats for this instrument
                    string[] beats = lines[i + nextLine].Split(' ');
                    for (int j = 0; j < BeatMaker.MAX_BEATS; j++)
                    {
                        //set the value of each beat in our application based on value in file
                        BeatMaker.drumBeats[i][j] = Convert.ToInt32(beats[j]) != 0;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Your save file did not have enough beats! " + e.Message);
                }
            }

            //read drum pattern
            nextLine = Array.IndexOf(lines, "drumPattern") + 1;
            for (int note = 0; note < BeatMaker.pattern.Notes; note++)
            {
                String[] stepsArr = lines[nextLine + note].Split(' ');
                for (int step = 0; step < BeatMaker.pattern.Steps; step++)
                {
                      BeatMaker.pattern[note, step] = Convert.ToByte(stepsArr[step]);
                }
            }

            //read pitch
            nextLine = Array.IndexOf(lines, "pitch") + 1;
            for (int note = 0; note < BeatMaker.pitchController.state.GetLength(0); note++)
            {
                for (int col = 0; col < BeatMaker.pitchController.state.GetLength(1); col++)
                {
                    String[] rows = lines[(note*col) + nextLine].Split(' ');
                    for (int row = 0; row < BeatMaker.pitchController.state.GetLength(2); row++)
                    {
                        bool b = Convert.ToInt32(rows[row]) == 1 ? true : false;
                        BeatMaker.pitchController.state[note, col, row] = Convert.ToBoolean(b);
                    }
                }
            }
        }

        private void button_ContactRemoved(object sender, ContactEventArgs e)
        {
            Button b = (Button)sender;
            b.Background = Brushes.White;
            b.Foreground = Brushes.DarkTurquoise;
        }

        private void saveToFile(String filename)
        {
            using (StreamWriter file = new StreamWriter(filename))
            {
                StringBuilder line = new StringBuilder();
                //update pattern,  pitch.state
 
                //write tempo to disk
                file.WriteLine("tempo");
                file.WriteLine(BeatMaker.tempoController.Tempo.ToString());

                //write chosenButton
                file.WriteLine("chosenButton");
                file.WriteLine(BeatMaker.chosenButton.ToString());

                //write chosenClips to disk
                file.WriteLine("chosenClips");
                foreach (int clip in BeatMaker.chosenClips)
                    line.Append(clip).Append(" ");
                file.WriteLine(line.ToString());

                //write drumBeats to disk
                file.WriteLine("drumBeats");
                for (int i = 0; i < BeatMaker.drumBeats.GetLength(0); i++)
                {
                    line = new StringBuilder();
                    for (int j = 0; j < BeatMaker.MAX_BEATS; j++)
                    {
                        int val = BeatMaker.drumBeats[i][j] ? 1 : 0;
                        line.Append(val).Append(" ");
                    }
                    file.WriteLine(line.ToString());
                }

                //write drum pattern
                file.WriteLine("drumPattern");
                for (int note = 0; note < BeatMaker.pattern.Notes; note++)
                {
                    line = new StringBuilder();
                    for (int step = 0; step < BeatMaker.pattern.Steps; step++)
                    {
                        int val = (int) BeatMaker.pattern[note, step];
                        line.Append(val).Append(" ");
                    }
                    file.WriteLine(line.ToString());
                }

                //write pitch
                file.WriteLine("pitch");
                for (int note = 0; note < BeatMaker.pitchController.state.GetLength(0); note++)
                {
                    for (int col = 0; col < BeatMaker.pitchController.state.GetLength(1); col++)
                    {
                        line = new StringBuilder();
                        for (int row = 0; row < BeatMaker.pitchController.state.GetLength(2); row++)
                        {
                            int val = BeatMaker.pitchController.state[note, col, row] ? 1 : 0;
                            line = line.Append(val.ToString()).Append(' ');
                        }
                        file.WriteLine(line.ToString());
                    }
                }
            }
        }
    }
}
