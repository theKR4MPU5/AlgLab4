using System;
using System.Collections.Generic;
using System.IO;
using AlgorhythmsLab3.Tester;
using AlgorythmsLab3.Testing.Tester;
using AlgSort.algorithms;
using AlgSort.menu;
using AlgSort.menu.menus;

namespace AlgSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new MainMenu(); //Запуск меню с работой в программе
            menu.Start();

            for (int j = 0; j < 5; j++)
            {
                int size = 0;
                switch (j)
                {
                    case 0:
                        size = 100;
                        break;
                    case 1:
                        size = 500;
                        break;
                    case 2:
                        size = 1000;
                        break;
                    case 3:
                        size = 2000;
                        break;
                    case 4:
                        size = 5000;
                        break;
                }


                ABCSort aBCSort = new ABCSort();
                TestTextSort(x => aBCSort.Sort(x), "ABCSort", 1, size);             //тест ABCSort слов
                ShakerSort shakerSort = new ShakerSort();
                TestTextSort(x => shakerSort.Sort(x), "ShakerSort", 1, size);                 //тест ShakerSort слов
            }
        }

        private static void TestTextSort(Func<List<string>, ICollection<string>> func, string name, int iterCount, int size)
        {

            
                Generate generate = new Generate();
                Generate.GenerateText(size);
                var tester = new TimeTester();

                var allText = File.ReadAllText(Directory.GetCurrentDirectory() + "\\..\\..\\TextFile1.txt").Split(' ');
                for (int i = 1; i <= allText.Length; i += 1)
                {
                    var testText = CopyTo(allText, i);

                    Console.WriteLine($"Тест алгоритма: {name} | Итерация: {i}");
                    tester.Test(() => func.Invoke(testText), iterCount, name);

                }

                tester.SaveAsExcel(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{name} - время, размер - {size}");
                tester.AllResults.Clear();
            


        }

        private static List<string> CopyTo(string[] array, int index)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < index; i++)
            {
                list.Add(array[i]);
            }

            return list;
        }
    }
}