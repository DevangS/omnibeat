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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Multitouch.Framework.WPF.Controls.Window
    {
        InteractiveSpaceProvider spaceProvider;
        Boolean play = false;
        Boolean stop = false;
        private Boolean[] kick = new Boolean[16];
        private Boolean[] snare = new Boolean[16];
        private Boolean[] closedHats = new Boolean[16];
        private Boolean[] openHats = new Boolean[16];
        
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

            for (int i = 0; i < 16; i++)
            {
                kick[i] = false;
                snare[i] = false;
                closedHats[i] = false;
                openHats[i] = false;
            }

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
            }
            else
            {
                b.Background = Brushes.White;
            }
        }

        private void kickButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            int index = (int)b.Tag;
            kick[index] = !kick[index];
            if (kick[index])
            {
                b.Background = Brushes.OrangeRed;
                Console.WriteLine("Kick " + index);
            }
            else
            {
                b.Background = Brushes.White;
            }
        }

        private void snareButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            int index = (int)b.Tag;
            snare[index] = !snare[index];
            if (snare[index])
            {
                b.Background = Brushes.OrangeRed;
                Console.WriteLine("Snare " + index);
            }
            else
            {
                b.Background = Brushes.White;
            }
        }

        private void closedHatsButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            int index = (int)b.Tag;
            closedHats[index] = !closedHats[index];
            if (closedHats[index])
            {
                b.Background = Brushes.OrangeRed;
                Console.WriteLine("Closed Hats " + index);
            }
            else
            {
                b.Background = Brushes.White;
            }
        }

        private void openHatsButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            int index = (int)b.Tag;
            openHats[index] = !openHats[index];
            if (openHats[index])
            {
                b.Background = Brushes.OrangeRed;
                Console.WriteLine("Open Hats " + index);
            }
            else
            {
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

    }
}
