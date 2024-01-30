using Project_Falling_rocks;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;


namespace FallingRocks
{
    public class FallingRocks
    {
        const int windowHeight = 40;
        const int windowWidth = 50;
        const int sizeOfScorePanel = 10;
        static int score = 0;
        static string inputSpawnRate;
        static string inputFallSpeed;
        static List<Rock> rocks = new List<Rock>();
        static Player player;
        static List<Rock> rocksToRemove = new List<Rock>(); 

        static int playerWindowCenterWidth = (Console.WindowWidth - 1) / 2;
        static int consoleMaxHeight = Console.WindowHeight - sizeOfScorePanel;
        static int playerWindowBottomHeight = consoleMaxHeight - 1;
        static int rocksSpawnRate;
        static void Main(string[] args)
        {
            Console.WriteLine("Rocks spawn rate hardness level (from 1 to 10 - 4 is prefferable)");
            inputSpawnRate = Console.ReadLine();

            rocksSpawnRate = ProcessPlayerRocksSpawnRateChoice(inputSpawnRate);

            Console.WriteLine("Rocks fall speed level (from 50 to 150 - 120 is prefferable)");
            inputFallSpeed = Console.ReadLine();

            int rocksFallSpeed = ProcessPlayerRockFallSpeedChoice(inputFallSpeed);

            SetWindowProperties();

            player = new Player(playerWindowCenterWidth, playerWindowBottomHeight);

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
        }

        static int ProcessPlayerRocksSpawnRateChoice(string inputSpawnRate)
        {
            bool playerInputSpawnRate = int.TryParse(inputSpawnRate, out int spawnRate);

            int minSpawnRate = 1;
            int maxSpawnRate = 10;

            if (playerInputSpawnRate || spawnRate < maxSpawnRate || spawnRate > minSpawnRate)
            {
                spawnRate = 4;
            }

            return spawnRate;
        }

        static int ProcessPlayerRockFallSpeedChoice(string inputFallSpeed)
        {
            bool playerInputRockFallSpeed = int.TryParse(inputFallSpeed, out int fallSpeed);

            int minFallSpeed = 50;
            int maxFallSpeed = 150;

            if (playerInputRockFallSpeed || fallSpeed < maxFallSpeed || fallSpeed > minFallSpeed)
            {
                fallSpeed = 120;
            }
            int totalRockFallSpeed = 200;

            return totalRockFallSpeed + fallSpeed; 
        }

        static void SetWindowProperties()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetWindowSize(windowWidth, windowHeight);
            Console.SetBufferSize(windowWidth, windowHeight);
        }

        static void CreateRocks()
        {
            int consoleMaxWidth = Console.WindowWidth - 1;

            for (int width = 1; width < consoleMaxWidth; width++)
            {
                if (ShouldGenerateRock())
                {
                    Rock currentWidth = new Rock(width);
                    rocks.Add(currentWidth);
                }
            }
        }

        static bool ShouldGenerateRock()
        {
            Random random = new Random();
            int spawnRateMaxValue = 100;
            return random.Next(0, 101) >= spawnRateMaxValue - rocksSpawnRate;
        }
        static void RedrawConsole()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < windowWidth - sizeOfScorePanel; i++)
            {
                Console.Write(new String(' ', windowWidth));
            }

            DrawScorePanel();
        }

        static void DrawScorePanel()
        {
            for (int width = 0; width < Console.WindowWidth; width++)
            {
                Console.Write('=');
            }

            Console.WriteLine($"Your score is: {score}");
            Console.WriteLine($"Rocks spawn rate: {ProcessPlayerRocksSpawnRateChoice(inputSpawnRate)}   Rocks fall speed: {ProcessPlayerRockFallSpeedChoice(inputFallSpeed)}");

        }

        static void DrawRocks()
        {
            foreach (Rock rock in rocks)
            {
                Console.SetCursorPosition(rock.X, rock.Y);
                Console.Write(rock.Symbol);
            }
        }

        static void MoveRocks()
        {
            List<Rock> rocksToMove = new List<Rock>();


            foreach (Rock rock in rocks)
            {
                rock.Y++;

                if (rock.Y == consoleMaxHeight)
                {
                    rocksToMove.Add(rock);
                    score++;
                }

                if (ThereIsCollision(rock, player))
                {
                    player.HasBeenHit = true;
                }
            }

            RemoveRocks(rocksToRemove, rocks);
        }


        static bool ThereIsCollision(Rock rock, Player player)
        {
            if (RockAndPlayerAreOnSameWidth(rock, player) && RockAndPlayerAreOnSameHeight(rock, player))
            {
                return true;
            }
            return false;
        }

        static bool RockAndPlayerAreOnSameWidth(Rock rock, Player player)
        => rock.X == player.X || rock.X == player.X + 1 || rock.X == player.X + 2;

        static bool RockAndPlayerAreOnSameHeight(Rock rock, Player player)
        => rock.Y == player.Y + 1;

        static void RemoveRocks(List<Rock> rocksToRemove, List<Rock> rocks)
        {
            foreach (var rock in rocksToRemove)
            {
                rocks.Remove(rock);
            }
        }

        static void EndGame()
        {
            int GameOverTextRow = 33;
            Console.SetCursorPosition(0, GameOverTextRow);
            Console.Write("GAME OVER!!!");
        }
    }
}