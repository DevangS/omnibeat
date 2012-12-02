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
    public partial class SoundClipsSelect : Page
    {
        public SoundClipsSelect()
        {
            InitializeComponent();
        }

        private void vizLayer_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void soundClipsSelectButton_NewContact(object sender, NewContactEventArgs e)
        {

        }
    }
}
