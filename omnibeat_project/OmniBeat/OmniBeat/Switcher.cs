using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace OmniBeat
{
    public static class Switcher
    {
        public static MainWindow mainWindow;

        public static void Switch(UserControl newView)
        {
            mainWindow.Navigate(newView);
        }

        // Do we need the second switch? public static void Switch(UserControl newView, object state){}
    }
}
