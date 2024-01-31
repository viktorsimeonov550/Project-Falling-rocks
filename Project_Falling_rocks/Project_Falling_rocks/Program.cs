namespace falling_rocks;
class Program
{
    static void Main(string[] args)
    {
        const int WindowHeight = 40;
        const int WindowWidth = 50;
        const int SizeOfScorePanel = 10;

        int score = 0;

        Console.WriteLine($"Rocks spawn rate hardness level (from 1 to 10 - 4 is prefferable)");

        string inputSpawnRate = Console.ReadLine();
        int rocksSpawnRate = ProcessPlayerRocksSpawnRateChoice(inputSpawnRate);

        Console.WriteLine("Rocks fall speed lever(from 50 to 150 - 120 is prefferable)");
        string inputFallSpeed = Console.ReadLine();
        int rocksFallSpeed = ProcessPlayerRockFallSpeedChoice(inputFallSpeed);

        SetWindowProperties();

        int playerWindowCenterWidth = (Console.WindowWidth - 1) / 2;
        int consoleMaxHeight = Console.WindowHeight - SizeOfScorePanel;

        int playerWindowBottomHeight = consoleMaxHeight - 1;


        Player player = new Player(playerWindowCenterWidth, playerWindowBottomHeight);

        List<Rock> rocks = new List<Rock>();

        while (true)
        {
            RedrawConsole();

            CreateRocks();

            player.Draw();
            DrawRocks();

            player.Move();
            MoveRocks();

            if (player.HasBeenHit)
            {
                EndGame();
                return;
            }

            Thread.Sleep(rocksFallSpeed);
        }

        int ProcessPlayerRocksSpawnRateChoice(string inputSpawnRate)
        {
            bool spawnRateChoice = int.TryParse(inputSpawnRate, out int spawnRate);

            int minSpawnRate = 1;
            int maxSpawnRate = 10;

            if (spawnRateChoice || spawnRate > minSpawnRate || spawnRate < maxSpawnRate)
            {
                spawnRate = 4;
            }
            return spawnRate;
        }

        int ProcessPlayerRockFallSpeedChoice(string inputFallSpeed)
        {
            bool FallSpeedChoice = int.TryParse(inputFallSpeed, out int FallSpeed);

            int minFallSpeed = 50;
            int maxFallSpeed = 150;

            if (FallSpeedChoice || FallSpeed > minFallSpeed || FallSpeed < maxFallSpeed)
            {
                FallSpeed = 120;
            }
            return FallSpeed;

            int totalRocksFallSpeed = 200;
            return totalRocksFallSpeed = FallSpeed;

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
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < WindowWidth - SizeOfScorePanel; i++)
            {
                Console.Write(new string(' ', WindowWidth));
            }

            DrawScorePanel();
        }

        void DrawScorePanel()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.WriteLine("=");
            }

            Console.WriteLine($"Your score is {score}");
            Console.Write($"Rocks spawn rate: {rocksSpawnRate}    Rocks Fall Speed: {rocksFallSpeed}");

        }

        void CreateRocks()
        {
            int ConsoleMaxWidth = Console.WindowWidth - 1;

            for (int width = 1; width < ConsoleMaxWidth; width++)
            {
                if (ShouldGenerateRocks())
                {
                    Rock newRock = new Rock(width);
                    rocks.Add(newRock);
                }
            }
        }

        bool ShouldGenerateRocks()
        {
            Random random = new Random();

            int spawnMaxRateValue = 100;

            return random.Next(0, 101) >= spawnMaxRateValue - rocksSpawnRate;
        }

        void DrawRocks()
        {
            foreach (Rock rock in rocks)
            {
                Console.SetCursorPosition(rock.X, rock.Y);
                Console.Write(rock.Symbol);

            }
        }

        void MoveRocks()
        {
            List<Rock> rocksToRemove = new List<Rock>();
            foreach (Rock rock in rocks)
            {
                rock.Y++;
                if (rock.Y == consoleMaxHeight)
                {
                    rocksToRemove.Add(rock);
                    score++;
                }
                if (ThereIsCollision(rock, player))
                {
                    player.HasBeenHit = true;
                }
            }

            RemoveRocks(rocksToRemove, rocks);


        }

        bool ThereIsCollision(Rock rock, Player player)
        {
            if (RockAndPlayerAreOnSameWidth(rock, player) && RockAndPlayerAreOnSameHeight(rock, player))
            {
                return true;
            }
            return false;
        }

        bool RockAndPlayerAreOnSameWidth(Rock rock, Player player) => rock.X == player.X || rock.X == player.X + 1 || rock.X == player.X + 2;
        bool RockAndPlayerAreOnSameHeight(Rock rock, Player player) => rock.Y == player.Y + 1;

        void RemoveRocks(List<Rock> rocksToRemove, List<Rock> rocks)
        {
            foreach (var rock in rocksToRemove)
            {
                rocks.Remove(rock);
            }

        }

        void EndGame()
        {
            int gameOverTextRow = 33;
            Console.SetCursorPosition(0, gameOverTextRow);
            Console.Write("GAME OVER!!!");
        }

    }
}