using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace OmniBeat
{
    class PitchBox 
    {
        public Rectangle rect { get; set; }
        public String path { get; set; }
        public int row { get; set; }
        public int col { get; set; }
        public float pitch { get; set; }

        public PitchBox(Rectangle _rect, int _col, int _row)
        {
            rect = _rect;
            col = _col;
            row = _row;
        }


    }
}
