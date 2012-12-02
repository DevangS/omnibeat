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
        public Boolean[][] drumBeats;
        private TempoController tempoController;
        private String save1Location;
        

        public Menu()
        {
            InitializeComponent();
            isSynced = false;
            ioLock = false;
            save1Location = Directory.GetCurrentDirectory() + "/save1";
        }

        public void sync(Boolean[][] beats, TempoController tempo)
        {
            Console.WriteLine("\n***** inside sync*****\n");
            isSynced = true;
            drumBeats = beats;
            tempoController = tempo;
        }

        private void openButton_NewContact(object sender, NewContactEventArgs e)
        {
            Console.WriteLine("Open Button Pressed");
            if (isSynced && !ioLock && saved)
            {
                ioLock = true;
                saveToFile(save1Location);
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

            //TODO: recolour all the beat buttons to be white in the GUI
        }

        private void loadFromFile(String filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
          
            tempoController.Tempo = Convert.ToInt32(lines[0]);

            for (int i = 0; i < drumBeats.Rank; i++)
            {
                try
                {
                    //get all beats for this instrument
                    string[] beats = lines[i].Split(' ');
                    for (int j = 0; j < drumBeats.GetLength(i); j++)
                    {
                        //set the value of each beat in our application based on value in file
                        drumBeats[i][j] = Convert.ToInt32(beats[j]) != 0;
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
                //write tempo to disk
                file.WriteLine(tempoController.Tempo.ToString());

                //write drumBeats to disk
                for (int i = 0; i < drumBeats.Rank; i++)
                {
                    StringBuilder line = new StringBuilder();
                    for (int j = 0; j < drumBeats.GetLength(i); j++)
                    {
                        int val = drumBeats[i][j] ? 1 : 0;
                        line.Append(val).Append(" ");
                    }
                    file.WriteLine(line.ToString());
                }
                file.Close();
            }
            
        }

    }
}
