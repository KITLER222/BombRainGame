namespace BombRainGame
{
    public class Program
    {
        const int width = 20; //ширина игрового поля
        const int height = 20; //длина игрового поля
        static int playerX = width / 2; //начальная позиция игрока (по центру)
        static int lives = 3; //количестов жизней
        static Random random = new Random(); //генератор случайных чисел
        static char[,] field = new char[width, height]; //поле символов: '.' - пусто, '*' - бомба

        static void Main(string[] args)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        static void SpawnBomb()
        {
            if (random.Next(0, 5) == 0)
            {
                int x = random.Next(0, width);
                field[x, 0] = '*';
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static void MoveBombs()
        {

        }
        
        /// <summary>
        /// 
        /// </summary>
        static void CheckCollision()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        static void DrawField()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        static void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.LeftArrow && playerX > 0)
                {
                    playerX--;

                }
                else if (key == ConsoleKey.RightArrow && playerX < width - 1)
                {
                    playerX++;
                }
            }
        }
    }
}
