using System;
namespace Project_Falling_rocks
{
    public class Rock
    {
        public int X { get; set; }
        public int Y { get; set; }

        public char Symbol { get; set; }

        private char[] Representation = new char[] { '^', '@', '*', '&', '+', '%', '$', '#', '!', '.', ';' };
        private Random Random = new Random();

        public Rock(int x)
        {
            this.X = x;
            this.Y = 0;

            this.Symbol = Representation[Random.Next(0, Representation.Length)];
        }
    }
}
