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
            if (isSynced && !ioLock && saved)
            {
                ioLock = true;
                loadFromFile(save1Location);
                BeatMaker.updateSelectedClips();
                BeatMaker.updateBeatButtons();
                BeatMaker.updateSoundClipButtons();
                BeatMaker.tempoController.updateTempo();
                saved = false;
                ioLock = false;
            }
        }

        private void saveButton_NewContact(object sender, NewContactEventArgs e)
        {
            Console.WriteLine("Save Button Pressed");
            if (isSynced && !ioLock)
            {
                ioLock = true;
                saveToFile(save1Location);
                saved = true;
                ioLock = false;
            }
        }

        private void clearButton_NewContact(object sender, NewContactEventArgs e)
        {
            Console.WriteLine("Clear Button Pressed");
            BeatMaker.clearEverything();
        }

        private void loadFromFile(String filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
          
            //read tempo
            BeatMaker.tempoController.Tempo = Convert.ToInt32(lines[0]);

            //read chosenButton
            BeatMaker.chosenButton = Convert.ToInt32(lines[1]);

            //read chosenClips
            String[] clips = lines[2].Split(' ');
            for (int i = 0; i < BeatMaker.chosenClips.Length; i++)
            {
                BeatMaker.chosenClips[i] = Convert.ToInt32(clips[i]);
            }

            //read drumbeats
            for (int i = 0; i < BeatMaker.drumBeats.GetLength(0); i++)

            {
                try
                {
                    //get all beats for this instrument
                    string[] beats = lines[i + 2 + BeatMaker.chosenClips.Length].Split(' ');
                    for (int j = 0; j < BeatMaker.drumBeats.GetLength(0); j++)
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

            int nextLine = 2 + BeatMaker.chosenClips.Length + BeatMaker.drumBeats.GetLength(0);

            //read drum pattern
            for (int note = 0; note < BeatMaker.pattern.Notes; note++)
            {
                String[] stepsArr = lines[nextLine + note].Split(' ');
                Console.Write((nextLine + note));
                Console.Write(lines[nextLine + note]);
                for (int step = 0; step < stepsArr.Length; step++)
                {
                      BeatMaker.pattern[note, step] = Convert.ToByte(stepsArr[step]);

                }
            }

            nextLine = 2 + BeatMaker.chosenClips.Length + BeatMaker.drumBeats.GetLength(0) + BeatMaker.pattern.Notes;

            //read pitch
            for (int note = 0; note < BeatMaker.pitchController.state.GetLength(0); note++)
            {
                Console.Write(lines[nextLine + note]);

                Console.Write((nextLine + note));
                for (int col = 0; col < BeatMaker.pitchController.state.GetLength(1); col++)
                {

                    Console.Write((nextLine + note + col));
                    String[] rows = lines[note + nextLine + col].Split(' ');
                    Console.Write(lines[nextLine + note + col]);
                    for (int row = 0; row < rows.Length; row++)
                    {
                        bool b = Convert.ToInt32(rows[row]) == 1 ? true : false;
                        BeatMaker.pitchController.state[note, col, row] = Convert.ToBoolean(b);
                    }
                }
            }
        }

        private void saveToFile(String filename)
        {
            using (StreamWriter file = new StreamWriter(filename))
            {
                StringBuilder line = new StringBuilder();
                //update pattern,  pitch.state
 
                //write tempo to disk
                file.WriteLine(BeatMaker.tempoController.Tempo.ToString());

                //write chosenButton
                file.WriteLine(BeatMaker.chosenButton.ToString());

                //write chosenClips to disk
                foreach (int clip in BeatMaker.chosenClips)
                    line.Append(clip).Append(" ");
                file.WriteLine(line.ToString());

                //write drumBeats to disk
                for (int i = 0; i < BeatMaker.drumBeats.GetLength(0); i++)
                {
                    line = new StringBuilder();
                    for (int j = 0; j < BeatMaker.drumBeats.GetLength(0); j++)
                    {
                        int val = BeatMaker.drumBeats[i][j] ? 1 : 0;
                        line.Append(val).Append(" ");
                    }
                    file.WriteLine(line.ToString());
                }

                //write drum pattern
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
                for (int note = 0; note < BeatMaker.pitchController.state.GetLength(0); note++)
                {
                    file.WriteLine(note.ToString());
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
