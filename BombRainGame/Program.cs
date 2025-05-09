﻿using System;
using System.Threading;

namespace BombRainGame
{
    public class Program
    {
        /// <summary>
        /// Блок с переменными
        /// </summary>
        const int width = 20; //ширина игрового поля (колличество символов по горизонтали)
        const int height = 20; //длина игрового поля (колличество строк по вертикали)
        static int playerX = width / 2; //начальная позиция игрока по оси x (по центру по горизонтали)
        static int lives = 3; //количестов жизней у игрока. Когда жизни закончатся игра завершится
        static Random random = new Random(); //генератор случайных чисел для появления бомб
        //игровое поле представленное двумерным массивом символов
        static char[,] field = new char[width, height]; //каждый элемент может быть - '.' - пустым или '*' - бомба

        /// <summary>
        /// Главный метод программы который запускается при первом старте приложения
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) 
        {
            Console.CursorVisible = false; //скрываем мигающий курсор для крассивого отображения игры

            //главный игровой цикл повторяется до тех пор пока у игрока есть жизни
            while(lives > 0)
            {
                HandleInput(); //обработка нажатий клавиш для перемещения игрока
                SpawnBomb(); //случайное появление новой бомбы в верхней строке
                MoveBombs(); //перемещаем все бомбы на одну строку вниз
                CheckCollision(); //проверям не столкнулся ли игрок с бомбой
                DrawField(); //отображаем все игровое поле на экране

                Thread.Sleep(200); //делаем небольшую паузу чтобы скорость перемещение игровых объектов не была слишком быстрой
            }
            Console.Clear(); //когда жизни закончились очищаем игровой экран
            Console.WriteLine("*Игра окончена!*"); //выводим сообщение об окончании игры
        }

        /// <summary>
        /// Метод для создания новой бомбы.
        /// С наибольшей вероятностью создем новую бомбу в случайной колонке в верхней строке
        /// </summary>
        static void SpawnBomb()
        {
            if (random.Next(0, 5) == 0) //с вероятностью 1 к 5 (20%) появится новая бомба
            {
                int x = random.Next(0, width); //выбираем случайную горизонтальную позицию для новой бомбы
                field[x, 0] = '*'; //помещвем бомбу в верхнюю строку поля
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
            for (int y = height - 2; y >= 0; y--) //проходим по всем строкам снизу вверх (иначе мы затрем падующие бомбы)
            {
                for (int x = 0; x < width; x++)
                {
                    if (field[x,y] == '*') //если в ячейке уже находится бомба 
                    {
                        field[x, y] = ' '; //убираем бомбу с текущего места
                        if(y + 1 < height) //если ниже еще есть место на поле
                        {
                            field[x, y + 1] = '*'; //перемищваем бомбу вниз на одну строку
                        }
                    }
                }
            }

            for (int x = 0; x < width; x++) //после перещмещения очищаем верхнюю строку от старой бомбы
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
            if (field[playerX, height - 1] == '*') //проверяем стоит ли бомба на позиции игрока
            {
                lives--; //уменьшаем количество жизней на 1
                Console.Beep(); //издаем звуковой сигнал при действии
                field[playerX, height - 1] = ' '; //убираем бомбу с позиции игрока
            }
        }

        /// <summary>
        /// Метод для отрисовки игрового поля.
        /// Этот метод рисует все игровые символьные объекты на экране.
        /// </summary>
        static void DrawField()
        {
            Console.SetCursorPosition(0,0); //очищаем экран перед новой отрисовкой

            for (int y = 0; y < height; y++) //проходим по всем строкам игрового поля
            {
                for(int x = 0; x < width; x++) //проходим по всем колонкам игрового поля
                {
                    if(y == height - 1 && x == playerX) //если эта позиция игрока (последняя строка и последний столбец игрока)
                    {
                        Console.Write("@"); //отображаем игрока символом '@'
                    }
                    else if(field[x,y] == '*')
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write(" "); //инече отображаем либо бомбу '*' либо пустоту '.'
                    }
                }
                if (y == height - 1)
                {
                    Console.WriteLine("\n====================");
                }
                else
                {
                    Console.WriteLine(); //после каждой строки переходим на новую строку
                }
            }
            Console.WriteLine("--------------------");
            Console.WriteLine($"\n Жизни: {lives}"); //после поля выводим колличество оставшихся жизней
        }

        /// <summary>
        /// Метод для обработки вводи игрока.
        /// Этот метод проверяет нажатие стрелок <- и -> и перемещает игрока по горизонтали.
        /// </summary>
        static void HandleInput()
        {
            while (Console.KeyAvailable) //проверяем была ли нажата какая либо клавиша
            {
                ConsoleKey key = Console.ReadKey(true).Key; //стчитываем нажатую клавишу (без отображения символа на экране)

                if (key == ConsoleKey.LeftArrow && playerX > 0) //если нажата стрелка влево и игрок не стоит у левого края 
                {
                    playerX--; //перемещаем игрока влево

                }
                else if (key == ConsoleKey.RightArrow && playerX < width - 1) //если нажата стрелка вправо и игрок не стоит у правого края
                {
                    playerX++; //то мы перемещаем игрока вправо
                }
            }
        }
    }
}
