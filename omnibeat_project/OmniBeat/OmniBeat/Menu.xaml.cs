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
            for (int i = 0; i < BeatMaker.chosenClips.Length; i++)
            {
                BeatMaker.chosenClips[i] = Convert.ToInt32(lines[i + 2]);
            }

            //read drumbeats
            for (int i = 0; i < BeatMaker.drumBeats.Rank; i++)

            {
                try
                {
                    //get all beats for this instrument
                    string[] beats = lines[i].Split(' ');
                    for (int j = 0; j < BeatMaker.drumBeats.GetLength(i); j++)
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
                for (int i = 0; i < BeatMaker.drumBeats.Rank; i++)
                {
                    line = new StringBuilder();
                    for (int j = 0; j < BeatMaker.drumBeats.GetLength(i); j++)
                    {
                        int val = BeatMaker.drumBeats[i][j] ? 1 : 0;
                        line.Append(val).Append(" ");
                    }
                    file.WriteLine(line.ToString());
                }
                file.Close();
            }
        }
    }
}
