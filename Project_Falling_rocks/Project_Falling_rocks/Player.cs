using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Falling_rocks
{
    internal class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Representation { get; set; }

        public bool HasBeenHit { get; set; } = false;

        public Player(int x, int y) 
        {
            this.X = x;
            this.Y = y;

            this.Representation = "(0)";
        }
        public void Draw()
        {
            Console.SetCursorPosition(this.X, this.Y);
            Console.Write(this.Representation);
        }
        public void Move()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                ClearKeyAvailableBuffer();

                bool isRight = pressedKey.Key == ConsoleKey.RightArrow;
                bool isLeft = pressedKey.Key == ConsoleKey.LeftArrow;

                if(isRight && IsOutOfConsoleBoundaries())
                {
                    this.X++;
                }
                else if(isLeft && this.X > 0)
                {
                    this.X--;
                }
            }
        }
        private bool IsOutOfConsoleBoundaries() => this.X + this.Representation.Length < Console.WindowWidth - 1;
        
              
    private void ClearKeyAvailableBuffer()
    {
        while (Console.KeyAvailable)
        {
            Console.ReadKey();
        }
    }
    

    }
}
