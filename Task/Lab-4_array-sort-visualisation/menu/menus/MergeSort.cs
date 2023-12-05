using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlgSort.menu.utils;
using AlgSort.task_2;

namespace AlgSort.menu.menus
{
    public abstract class MergeSort : MenuItem
    {
        private SortType _sortType;

        protected MergeSort(SortType sortType, string title, bool isSelected = false) : base(title, isSelected)
        {
            _sortType = sortType;
        }

        public override void Execute()
        {
            do
            {
                ConsoleUtil.ClearScreen();
                try
                {
                    var tablesName = GetListTableNames();
                    Console.WriteLine($"Введите имя файла для сортировки\nДоступные таблицы [ {tablesName} ]");
                    var nameFile = Console.ReadLine();
                    if (string.IsNullOrEmpty(nameFile) || !CheckFileExist(nameFile))
                    {
                        throw new Exception("Файла с таким именем не найдено");
                    }
                    else
                    {
                        nameFile = $"..\\..\\..\\files\\{nameFile}.csv";
                    }

                    var header = GetHeader(nameFile);
                    Console.WriteLine(
                        $"Введите название столбца для сортировки\nСтолбцы, доступные в выбранной таблице: [ {header} ]");
                    var attributeName = Console.ReadLine();
                    if (string.IsNullOrEmpty(attributeName) || !CheckColumnExist(attributeName, header))
                    {
                        throw new Exception("Столбца с таким именем не существует в заданной таблице");
                    }

                    Console.WriteLine("Введите порядок сортировки: 1 - по возрастанию, 2 - по убыванию");
                    var ascendingChoice = Console.ReadLine();
                    if (string.IsNullOrEmpty(ascendingChoice) ||
                        (!ascendingChoice.Equals("1") && !ascendingChoice.Equals("2")))
                    {
                        throw new Exception("Введнная строка пуста или содержит недопустимое значение");
                    }

                    bool ascending = ascendingChoice.Equals("1");

                    Console.WriteLine("Введите длительность задержки в миллисекундах: ");
                    var inputTime = Console.ReadLine();
                    if (string.IsNullOrEmpty(inputTime) || !int.TryParse(inputTime, out var time))
                    {
                        throw new Exception("Введнная строка пуста или содержит недопустимое значение");
                    }

                    TableWorker worker = new TableWorker(nameFile, time);
                    worker.GetSortedTable(outputPath: $"{nameFile}_sorted.csv", ascending: ascending,
                        attribute: attributeName, sortType: _sortType);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (ConsoleUtil.Continue());
        }

        private bool CheckFileExist(string fileName)
        {
            if (fileName.Contains(".csv"))
            {
                fileName = fileName.Replace(".csv", "");
            }

            return File.Exists($"..\\..\\..\\files\\{fileName}.csv");
        }

        private string GetHeader(string fileName)
        {
            var header = File.ReadAllLines(fileName)[0];
            header = header.Replace(";", " | ");
            return header;
        }

        private bool CheckColumnExist(string columnName, string header) => header.Contains(columnName);

        private string GetListTableNames()
        {
            List<string> names = new List<string>();
            var files = Directory.GetFiles("..\\..\\..\\files").Where(x => x.Contains(".csv"));
            foreach (var name in files)
            {
                var nameWithoutExtension = name.Replace(".csv", "").Replace("..\\..\\..\\files\\", "");
                names.Add(nameWithoutExtension);
            }

            return string.Join(" | ", names);
        }
    }
}