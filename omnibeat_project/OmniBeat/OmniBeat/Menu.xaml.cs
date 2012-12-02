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
        public bool delay_flag;
        public Boolean[][] drumBeats;
        private TempoController tempoController;
        //public Vector<    
        

        public Menu()
        {
            InitializeComponent();
            isSynced = false;
            delay_flag = false;
        }

        public void sync(Boolean[][] beats, TempoController tempo)
        {
            Console.WriteLine("\n***** inside sync*****\n");
            isSynced = true;
            drumBeats = beats;
            tempoController = tempo;
        }

        private void saveButton_NewContact(object sender, NewContactEventArgs e)
        {
            // Use timers.. 
            Console.WriteLine("Save Button Pressed");
            if (isSynced && !delay_flag)
            {
                delay_flag = true;
                saveToFile("save");
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
            }

        }

    }
}
