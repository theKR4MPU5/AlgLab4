using System;
using System.Collections.Generic;
using AlgSort.algorithms;
using AlgSort.menu.utils;

namespace AlgSort.menu.menus
{
    public class AlgorithmMenu : MenuItem
    {
        private readonly Algorithm<int> _algorithm;

        protected AlgorithmMenu(Algorithm<int> algorithm, string title, bool isSelected = false) : base(title,
            isSelected)
        {
            _algorithm = algorithm;
        }

        public override void Execute()
        {
            do
            {
                ConsoleUtil.ClearScreen();
                Console.WriteLine($"[ {Title.ToUpper()} ]");
                var values = InputValues();
                var delay = InputDelayValue();

                _algorithm.Data = values.ToArray();
                _algorithm.SortDelay = delay;
                _algorithm.Sort();
            } while (ConsoleUtil.Continue());
        }

        private List<int> InputValues()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите через пробел последовательность цифр для сортировки: ");
                    var listValues = Console.ReadLine();
                    if (string.IsNullOrEmpty(listValues))
                    {
                        throw new Exception("Введеная строка не содержит символов");
                    }

                    var splitListValues = listValues.Split(" ");
                    List<int> values = new List<int>();
                    foreach (var value in splitListValues)
                    {
                        if (!int.TryParse(value, out var intValue))
                        {
                            throw new Exception($"Введеное значение {value} не является числом");
                        }

                        values.Add(intValue);
                    }

                    return values;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private int InputDelayValue()
        {
            while (true)
            {
                Console.WriteLine("Введите задержку: ");
                try
                {
                    var delayValue = Console.ReadLine();
                    if (string.IsNullOrEmpty(delayValue))
                    {
                        throw new Exception("Введеная строка не содержит символов");
                    }

                    if (!int.TryParse(delayValue, out var intDelayValue))
                    {
                        throw new Exception($"Введеное значение {delayValue} не является числом");
                    }

                    return intDelayValue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}