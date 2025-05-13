using System;
using System.Threading;

namespace BombRainGame
{
    public class Program
    {
        /// <summary>
        /// Блок с переменными
        /// </summary>
        const int width = 20;
        const int height = 20;
        static int playerX = width / 2;
        static int lives = 3;
        static Random random = new Random();
        static char[,] field = new char[width, height];

        /// <summary>
        /// Главный метод программы который запускается при первом старте приложения
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) 
        {
            Console.CursorVisible = false;

            while(lives > 0)
            {
                HandleInput();
                SpawnBomb();
                MoveBombs();
                CheckCollision();
                DrawField();

                Thread.Sleep(50);
            }
            Console.Clear();
            Console.WriteLine("*Игра окончена!*");
        }

        /// <summary>
        /// Метод для создания новой бомбы.
        /// С наибольшей вероятностью создем новую бомбу в случайной колонке в верхней строке
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
        /// Метод для перемещения всех бомб вниз.
        /// Этот метод сдвигает все бомбы на одну строку вниз
        /// </summary>
        static void MoveBombs()
        {
            for (int x = 0; x < width; x++)
            {
                if (field[x, height - 1] == '*')
                {
                    if(x != playerX)
                    {
                        field[x, height - 1] = ' ';
                    }
                }
            }
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
        /// Метод для проверки попадания бомбы в игрока. 
        /// Если бомба попадает в игрока отнимаем жизнь.
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
        /// Метод для отрисовки игрового поля.
        /// Этот метод рисует все игровые символьные объекты на экране.
        /// </summary>
        static void DrawField()
        {
            Console.SetCursorPosition(0,0);

            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    if(y == height - 1 && x == playerX)
                    {
                        Console.Write("@");
                    }
                    else if(field[x,y] == '*')
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                if (y == height - 1)
                {
                    Console.WriteLine("\n====================");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("--------------------");
            Console.WriteLine($"\n Жизни: {lives}");
        }

        /// <summary>
        /// Метод для обработки вводи игрока.
        /// Этот метод проверяет нажатие стрелок <- и -> и перемещает игрока по горизонтали.
        /// </summary>
        static void HandleInput()
        {
            while (Console.KeyAvailable)
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
