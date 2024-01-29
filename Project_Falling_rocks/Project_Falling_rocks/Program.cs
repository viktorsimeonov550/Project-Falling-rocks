namespace Project_Falling_rocks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int WindowHeight = 40;
            const int WindowWidth = 50;
            const int SizeOfscorePanel = 10;

            int score = 0;

            Console.WriteLine("Rocks spawn rate hardness level (from 1 to 10 - 4 is prefferable)");
            string inputSpawnRate = Console.ReadLine();

            int rocksSpawnRate = ProcessPlayerRocksSpawnRateChoice(inputSpawnRate);

            Console.WriteLine("Rocks fall speed level (from 50 to 150 - 120 is prefferable)");
            string inputFallSpeed = Console.ReadLine();
            int rocksFallSpeed = ProcessPlayerRocksSpeedRateChoice(inputFallSpeed);

            //SetWindowProperties();

            int playerWindowCenterWidth = (Console.WindowWidth - 1) / 2;
            int consoleMaxHeight = Console.WindowHeight - SizeOfscorePanel;
            int playerWindowBottomHeight = consoleMaxHeight - 1;

            Player player = new Player();

            Player player = new Player(playerWindowCenterWidth, playerWindowBottomHeight);

            List<Rock> rocks = new List<Rock>();

            while (true)
            {
                //RedrawConsole()
                //CreateRocks()
                
                player.Draw();

                //DrawRocks();
                player.Move();
                //MoveRocks();
                if (player.HasBeenHit)
                {
                    //EndGame();
                    return;
                }
                Thread.Sleep(rocksFallSpeed);
            }
            int ProcessPlayerRocksSpawnRateChoice(string inputSpawnRate)
            {

                bool spawnRateChoice = int.TryParse(inputSpawnRate, out int spawnRate);

                int minSpawnRate = 1;
                int maxSpawnRate = 10;

                if(spawnRateChoice || spawnRate> minSpawnRate || spawnRate < maxSpawnRate)
                {
                    spawnRate = 4;
                }
                return spawnRate;

            }

            int ProcessPlayerRockFallSpeedChoice(string inputFallSpeed)
            {
                bool spawnRateChoice = int.TryParse(inputSpawnRate, out int spawnRate);

                int minSpawnRate = 50;
                int maxSpawnRate = 150;

                if (spawnRateChoice || spawnRate > minSpawnRate || spawnRate < maxSpawnRate)
                {
                    spawnRate = 4;
                }

                int totalRocksFallSpeed = 200;
                return spawnRate;
            }


        }
        void SetWindowProperties()
        {
            Console.Clear();
            Console.CursorVisible = false;

            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.SetBufferSize(WindowWidth, WindowHeight);
        }
        void RedrawConsole()
        {
            Console.SetCursorPosition();


        }
    }
}
