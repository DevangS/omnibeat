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
        public bool drumBeats_isSet;
        public bool delay_flag;
        public Boolean[][] drumBeats;
        //public Vector<    
        

        public Menu()
        {
            InitializeComponent();
            drumBeats_isSet = false;
            delay_flag = false;
        }

        public void referenceDrumBeats(Boolean[][] beats)
        {
            Console.WriteLine("\n***** referencing the drum beats*****\n");
            drumBeats_isSet = true;
            drumBeats = beats;
        }

        private void saveButton_NewContact(object sender, NewContactEventArgs e)
        {
            // Use timers.. 
            Console.WriteLine("Save Button Pressed");
            if (drumBeats_isSet && !delay_flag)
            {
                delay_flag = true;
                
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
    }
}
