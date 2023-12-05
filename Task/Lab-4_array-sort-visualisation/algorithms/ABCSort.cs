using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AlgSort.algorithms
{
    public class ABCSort : Algorithm<string>
    {
        public ICollection<string> Sort(List<string> words, int rank = 0)
        {
            if (words.Count > 1)
            {
                Console.WriteLine($"Сравниваем слова по символу с индексом {rank}\n");
                Thread.Sleep(SortDelay);
                Dictionary<char, List<string>> squares = new Dictionary<char, List<string>> {{'@', new List<string>()}};
                foreach (var word in words)
                {
                    Console.WriteLine($"Сравниваем индекс {rank} и длину слова {word}");
                    Thread.Sleep(SortDelay);
                    if (rank < word.Length)
                    {
                        if (squares.ContainsKey(word[rank]))
                        {
                            squares[word[rank]].Add(word);
                        }
                        else
                        {
                            squares.Add(word[rank], new List<string>() {word});
                        }

                        Console.WriteLine($"\tДобавляем слова \"{word}\" в группу [ {word[rank]} ]\n");
                        Thread.Sleep(SortDelay);
                    }
                    else
                    {
                        Console.WriteLine($"Добавляем слова \"{word}\" в дефолтную группу [ @ ]\n");
                        Thread.Sleep(SortDelay);
                        squares['@'].Add(word);
                    }
                }

                if (squares['@'].Count == words.Count)
                {
                    Console.WriteLine(
                        $"Мы отсортировали все символы слов, теперь выводим итоговый список \n");
                    Thread.Sleep(SortDelay);
                    return words;
                }

                List<string> result = new List<string>();
                for (char i = '@'; i <= 'z'; i++)
                {
                    if (squares.ContainsKey(i))
                    {
                        foreach (var word in Sort(squares[i], rank + 1))
                        {
                            result.Add(word);
                        }
                    }
                }

                return result;
            }

            return words;
        }

        public override void Sort()
        {
            Sort(Data.ToList());
        }
    }
}