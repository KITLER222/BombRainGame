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
            Console.CursorVisible = false;

            while(lives > 0)
            {

            }
        }

        /// <summary>
        /// С шансом 1 к 5 добавляет * в случайную врехнюю ячейку поля
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
        /// Перемещает все бомбына скроку ниже (двухмерный массив)
        /// </summary>
        static void MoveBombs()
        {
            for (int y = height - 2; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    if (field[x,y] == '*')
                    {
                        field[x, y] = ' ';
                        if(y + 1 < height)
                        {
                            field[x, y + 1] = '*';
                        }
                    }
                }
            }

            for (int x = 0; x < width; x++)
            {
                if(field[x, 0] != '*')
                {
                    field[x, 0] = ' ';
                }
            }
        }
        
        /// <summary>
        /// Если бомба на позиции игррока - уменьшаем жизнь и убираем бомбу
        /// </summary>
        static void CheckCollision()
        {
            if (field[playerX, height - 1] == '*')
            {
                lives--;
                Console.Beep();
                field[playerX, height - 1] = ' ';
            }
        }

        /// <summary>
        /// Выводим текущее состояние поля и статистику
        /// </summary>
        static void DrawField()
        {
            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    if(y == height - 1 && x == playerX)
                    {
                        Console.Write("@");
                    }
                    else
                    {
                        Console.Write(field[x,y] == '*' ? "*" : ".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine($"\n Жизни: {lives}");
        }

        /// <summary>
        /// Управление игроком через <- и ->
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
