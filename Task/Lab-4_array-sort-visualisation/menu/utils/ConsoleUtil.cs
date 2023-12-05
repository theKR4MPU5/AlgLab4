using System;

namespace AlgSort.menu.utils
{
    public static class ConsoleUtil
    {
        public static void ClearScreen()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new String(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, 0);
        }

        public static bool Continue()
        {
            do
            {
                Console.WriteLine("\nХотите выйти? [Y/N]");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Y | keyInfo.Key == ConsoleKey.N)
                {
                    return !(keyInfo.Key == ConsoleKey.Y);
                }
                else
                {
                    Console.WriteLine("Неизвестная клавиша");
                }
            } while (true);
        }

        public static void Pause()
        {
            Console.WriteLine("\nДля продолжения нажмите любую клавишу...");
            Console.ReadLine();
        }
    }
}