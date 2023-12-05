using System;
using System.Linq;
using System.Text.RegularExpressions;
using AlgSort.algorithms;
using AlgSort.menu.utils;

namespace AlgSort.menu.menus
{
    public abstract class AlgorithmStringSortMenu : MenuItem
{
    private Algorithm<string> _sort;

    protected AlgorithmStringSortMenu(Algorithm<string> sort, string title, bool isSelected = false) : base(title: title,
        isSelected)
    {
        _sort = sort;
    }

    public override void Execute()
    {
        do
        {
            try
            {
                ConsoleUtil.ClearScreen();
                Console.WriteLine($"[ {Title.ToUpper()} ]\n");
                Console.CursorVisible = true;

                Console.WriteLine("Введите текст, слова которого должны быть отсортированы: ");
                string inputText = Console.ReadLine();
                if (string.IsNullOrEmpty(inputText))
                {
                    throw new ArgumentException("Введенная строка пуста!");
                }
                else if (new Regex("[0-9]", RegexOptions.Compiled | RegexOptions.IgnoreCase)
                             .Matches(inputText).Count > 0)
                {
                    throw new ArgumentException("Строка содержит числовые значения!");
                }
                else
                {
                    Console.WriteLine("Введите задержку в миллисекундах");
                    var inputDelay = Console.ReadLine();
                    if (!int.TryParse(inputDelay, out var delay))
                    {
                        throw new ArgumentException("Введенные данные содержат не допустимые значения");
                    }

                    _sort.SortDelay = delay;

                    var inputListWord = inputText.Trim().Split(new[] {' ', ',', '.', '(', ')'}).Where(x => x != "")
                        .Select(x => x.ToLower())
                        .ToArray();
                    _sort.Data = inputListWord;
                    _sort.Sort();
                    var result = _sort.Data;
                    var wordCount = AlgorithmUtil.Count(result);
                    Console.WriteLine();
                    for (int i = 0; i < result.Length; i++)
                    {
                        Console.WriteLine($"{i}) \"{result[i]}\" встретилось раз [ {wordCount[result[i]]} ] ");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.CursorVisible = false;
        } while (ConsoleUtil.Continue());
    }
}
}