using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project_Falling_rocks
{
    internal class Rock
    {
        public int X { get; set; }
        public int Y { get; set; }

        public char Symbol { get; set; }
        public char[] Representation = new char[] { '^', '@', '*', '&', '+' , '%', '$', '#', '!', '.', ';' };

        private Random random = new Random();

        public Rock (int x)
        {
            this.X = x;
            this.Y = 0;

            this.Symbol = Representation[random.Next(0, Representation.Length)];
        }


        
    }
}
