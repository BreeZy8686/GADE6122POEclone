using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{
    public class Position
    {
        // Fields
        private int _x;
        private int _y;

        // Constructor to initialize x and y
        public Position(int x, int y)
        {
            _x = x;
            _y = y;
        }

        // Properties
        public int X
        {
            get => _x;
            set => _x = value;
        }

        public int Y
        {
            get => _y;
            set => _y = value;
        }
    }
}
