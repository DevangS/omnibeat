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
    /// Interaction logic for SoundClipsSelect.xaml
    /// </summary>
    public partial class SoundClipsSelect : UserControl
    {
        Boolean[] clipSelected = new Boolean[10];

        public SoundClipsSelect()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                clipSelected[i] = false;
            }
        }

        private void vizLayer_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void soundClipsSelectButton_NewContact(object sender, NewContactEventArgs e)
        {
            Console.WriteLine("Pressed the button" + ((Button)sender).Name.ToString());
        }

        private void SelectButton_NewContact(object sender, NewContactEventArgs e)
        {
            Console.WriteLine("PRESSED SELECT BUTTON");
            Switcher.Switch(Switcher.mainWindow.beatMaker);
        }
    }
}