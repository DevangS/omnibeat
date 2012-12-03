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
        public Boolean[] clipSelected;
        public Button[] buttons;
        public int numClips;
        public List<int> clipIndices;

        public SoundClipsSelect()
        {
            InitializeComponent();
            clipSelected = new Boolean[10];
            clipIndices = new List<int>();
            buttons = new Button[10];
            buttons[0] = this.kickTrimmed;
            buttons[1] = this.snareTrimmed;
            buttons[2] = this.closedHatsTrimmed;
            buttons[3] = this.openHatsTrimmed;
            buttons[4] = this.cymbal;
            buttons[5] = this.everybody;
            buttons[6] = this.ohYeah;
            buttons[7] = this.oneMoreTime;
            buttons[8] = this.shots;
            buttons[9] = this.jerk;
            ResetSelections();
        }

        private void vizLayer_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void ResetSelections()
        {
            clipIndices.Clear();
            for (int i = 0; i < 10; i++)
            {
                clipSelected[i] = false;
            }
            foreach (Button button in buttons) {
                button.Background = Brushes.White;
            }
            numClips = 0;
        }

        private void soundClipsSelectButton_NewContact(object sender, NewContactEventArgs e)
        {
            Button b = (Button)sender;
            Console.WriteLine("Pressed the button" + b.Name.ToString());
            int index = Convert.ToInt32(b.Tag);
            if (!clipSelected[index]) {
                if (numClips == 4) return;
                clipSelected[index] = true;
                numClips++;
                b.Background = Brushes.DarkTurquoise;
                b.Foreground = Brushes.White;
            }
            else {
                if (numClips == 0) return;
                clipSelected[index] = false;
                numClips--;
                b.Background = Brushes.White;
                b.Foreground = Brushes.DarkTurquoise;
            }
        }

        private void SelectButton_NewContact(object sender, NewContactEventArgs e)
        {
            Console.WriteLine("PRESSED SELECT BUTTON");
            if (numClips != 4) return;

            for(int i = 0; i < 10; i++) {
                if (clipSelected[i]) clipIndices.Add(i);
            }

            if (clipIndices.Count != 4)
            {
                clipIndices.Clear();
                return;
            }

            Switcher.mainWindow.beatMaker.ChangeNotes(clipIndices);
            Switcher.Switch(Switcher.mainWindow.beatMaker);
            Switcher.mainWindow.beatMaker.soundClipsSelectButton.Background = Brushes.White;
            Switcher.mainWindow.beatMaker.soundClipsSelectButton.Foreground = Brushes.DarkTurquoise;
        }
    }
}
