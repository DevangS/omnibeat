using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OmniBeat
{
    class Person
    {
        string name = "Nadir";

        // The attribute is the block title "Name" and is 
        // used as the binding value
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
