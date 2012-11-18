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
    /// Interaction logic for TouchSlider.xaml
    /// </summary>
    public partial class TouchSlider : UserControl
    {
        public bool isSliding;

        public TouchSlider()
        {
            InitializeComponent();
            isSliding = false;
        }

        private void tempoSlider_NewContact(object sender, NewContactEventArgs e)
        {
            this.isSliding = true;
            /*
            Slider s = (Slider)sender;

            Point p = e.GetPosition(s);
            Console.WriteLine("slider loc: " + s.Value);
            Console.WriteLine("Pressed at: " + p);
             */
            //Console.WriteLine("new contact");
        }

        private void tempoSlider_ContactMoved(object sender, ContactEventArgs e)
        {
            //Console.WriteLine("moved!!");
            if (isSliding)
            {
                Slider s = (Slider)sender;
                Point p = e.GetPosition(s);

                s.Value = p.X;
            }
        }

        private void tempoSlider_ContactRemoved(object sender, ContactEventArgs e)
        {
            this.isSliding = false;
           // Console.WriteLine("removed!!");
        }


    }
}
