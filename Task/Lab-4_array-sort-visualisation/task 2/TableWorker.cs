using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace AlgSort.task_2
{
    class TableWorker
    {
        public enum ColumnType
        {
            Integer,
            String
        }

        private const char SeparatingSign = ';';
        private readonly string _filePath;
        private readonly int _columnCount;
        private int RowCount { get; set; }
        public readonly string[] Attributes;
        private readonly ColumnType[] _types;
        private readonly int _delayTime;

        public TableWorker(string path, int delayTime)
        {
            _delayTime = delayTime;
            _filePath = path;
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);

                _columnCount = file.ReadLine()!.Split(";").Length;
                RowCount = File.ReadAllLines(path).Count() - 1;
                Attributes = File.ReadLines(path).First().Split(";");
                _types = ParseToType(path);
                file.Close();
            }
            else
            {
                throw new Exception("Файл не найден");
            }
        }

        private TableWorker(string path, TableWorker cloneableTable)
        {
            _filePath = path;
            _columnCount = cloneableTable._columnCount;
            Attributes = cloneableTable.Attributes;
            _types = cloneableTable._types;

            using StreamWriter newFile = new StreamWriter(path);
            using StreamReader cloneableFile = new StreamReader(cloneableTable._filePath);

            newFile.WriteLine(ParseLine(cloneableFile.ReadLine())[0] + SeparatingSign + "0" + SeparatingSign);

            newFile.WriteLine(cloneableFile.ReadLine());
            newFile.WriteLine(cloneableFile.ReadLine());
        }

        string[] ParseLine(string line)
        {
            return line.Split(SeparatingSign);
        }

        private ColumnType[] ParseToType(string path)
        {
            var line = File.ReadLines(path).Skip(1).First().Split(";");
            ColumnType[] output = new ColumnType[_columnCount];
            for (var i = 0; i < line.Length; i++)
            {
                if (int.TryParse(line[i], out _))
                {
                    output[i] = ColumnType.Integer;
                }
                else
                {
                    output[i] = ColumnType.String;
                }
            }

            return output;
        }

        private static Element[] ParseToElements(string[] words, ColumnType[] types)
        {
            Element[] output = new Element[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                output[i] = new Element(words[i], types[i]);
            }

            return output;
        }

        private void DirOperation()
        {
            var dirInfo = new DirectoryInfo("temp");
            if (dirInfo.Exists)
            {
                foreach (var file in dirInfo.GetFiles())
                {
                    file.Delete();
                }
            }
            else
            {
                Directory.CreateDirectory("temp");
            }
        }

        public void GetSortedTable(string outputPath, bool ascending, string attribute, SortType sortType)
        {
            int attributeNum = -1;
            for (int i = 0; i < _columnCount; i++)
            {
                if (Attributes[i] == attribute)
                    attributeNum = i;
            }

            if (attributeNum == -1)
                throw new Exception("Неверное имя атрибута");

            switch (sortType)
            {
                case SortType.Direct:
                    DirectSort(outputPath, ascending, attributeNum, 0);
                    break;
                case SortType.Natural:
                    NaturalSort(outputPath, attributeNum, ascending);
                    break;
                case SortType.Multipath:
                    MultiPathSort(outputPath, attributeNum, ascending);
                    break;
                default:
                    throw new Exception("Нереализованный тип сортировки");
            }
        }

        private void MultiPathSort(string outputPath, int attributeNum, bool ascending)
        {
            string directoryPath = "temp";

            Console.WriteLine("[ Многопутевая сортировка ]");

            SplitIntoTablesNaturally(directoryPath, attributeNum, ascending);
            MergeSortedTables(outputPath, Directory.GetFiles(directoryPath).ToList(), attributeNum, ascending);
        }

        private void DirectSort(string outputPath, bool ascending, int attributeNum, int depth)
        {
            Console.WriteLine("[ Прямая сортировка ]");
            
            DirOperation();
            SubSortDirectly(outputPath, ascending, attributeNum, 0);
        }

        private void NaturalSort(string outputPath, int attributeNum, bool ascending)
        {
            string directoryPath = "temp";
            
            Console.WriteLine("[ Натуральная сортировка ]");

            SplitIntoTablesNaturally(directoryPath, attributeNum, ascending);

            string newDirectoryPath = directoryPath;
            int j = 0;
            do
            {
                string[] files = Directory.GetFiles(newDirectoryPath);
                newDirectoryPath = directoryPath + @"\temp_" + j;
                Directory.CreateDirectory(newDirectoryPath);

                for (int i = 1; i < files.Length; i += 2)
                {
                    MergeSortedTables(newDirectoryPath + @"\temp_" + i / 2 + ".txt",
                        new() {files[i - 1], files[i]}, attributeNum, ascending);
                }

                if (files.Length % 2 == 1)
                {
                    Console.WriteLine("Оставшийся файл не с чем слить, копируем его");
                    File.Copy(files[^1], newDirectoryPath + @"\temp_" + files.Length / 2 + ".txt");
                    Thread.Sleep(_delayTime);
                }

                j++;
            } while (Directory.GetFiles(newDirectoryPath).Length > 1);

            File.Copy(Directory.GetFiles(newDirectoryPath)[0], outputPath, true);
        }

        private void SubSortDirectly(string outputPath, bool ascending, int columnNum, int depth)
        {
            if (RowCount > 1)
            {
                Console.WriteLine($"В файле {outputPath} еще есть данные");
                Console.WriteLine();
                Thread.Sleep(_delayTime);
                
                string path1 = @"temp\temp_" + depth + "_1" + ".txt";
                string path2 = @"temp\temp_" + depth + "_2" + ".txt";
                Console.WriteLine($"Создаем 2 файла:{path1} и {path2}");
                Thread.Sleep(_delayTime);
                Console.WriteLine("Заполняем файлы делением корневого файла");
                SplitIntoTwoTableDirectly(path1, path2);
                PrintFile(path1);
                PrintFile(path2);
                Thread.Sleep(_delayTime);
                TableWorker table1 = new TableWorker(path1, _delayTime);
                TableWorker table2 = new TableWorker(path2, _delayTime);
                table1.SubSortDirectly(path1, ascending, columnNum, depth + 1);
                table2.SubSortDirectly(path2, ascending, columnNum, depth + 1);
                MergeSortedTables(outputPath, new List<string>() {path1, path2}, columnNum, ascending);
                PrintFile(outputPath);
            }
        }

        private void PrintFile(string path)
        {
            Console.WriteLine();

            Console.WriteLine($"[ Содержимое файла {path} ]");
            foreach (var line in File.ReadAllLines(path))
            {
                Console.WriteLine(line);
            }

            Console.WriteLine();
        }

        private void SplitIntoTwoTableDirectly(string outputPath1, string outputPath2)
        {
            Console.WriteLine($"Разбиваем информацию на два файла");
            Console.WriteLine();
            Thread.Sleep(_delayTime);
            if (!File.Exists(outputPath1))
            {
                File.Delete(outputPath1);
            }

            if (!File.Exists(outputPath2))
            {
                File.Delete(outputPath2);
            }

            TableWorker table1 = new TableWorker(outputPath1, this);
            TableWorker table2 = new TableWorker(outputPath2, this);

            using StreamWriter file1 = new StreamWriter(outputPath1);
            using StreamWriter file2 = new StreamWriter(outputPath2);
            using StreamReader originFile = new StreamReader(_filePath);

            string header = originFile.ReadLine();
            file1.WriteLine(header);
            file2.WriteLine(header);
            int j = 0;
            while (!originFile.EndOfStream)
            {
                if (j % 2 == 0)
                {
                    var data = originFile.ReadLine();
                    Console.WriteLine($"Записываем в первый файл {data}");
                    Thread.Sleep(_delayTime);
                    file1.WriteLine(data);
                }
                else
                {
                    var data = originFile.ReadLine();
                    Console.WriteLine($"Записываем во второй файл {data}");
                    Thread.Sleep(_delayTime);
                    file2.WriteLine(data);
                }

                j++;
            }

            table1.RowCount = RowCount / 2 + RowCount % 2;
            table2.RowCount = RowCount / 2;
        }

        private void SplitIntoTablesNaturally(string outputDirectoryPath, int checkedColumnNum, bool ascending)
        {
            int dir = ascending ? 1 : -1;
            List<TableWorker> tables = new List<TableWorker>();

            using StreamReader originFile = new StreamReader(_filePath);

            var attributes = originFile.ReadLine();

            if (Directory.Exists(outputDirectoryPath))
                Directory.Delete(outputDirectoryPath, true);
            Directory.CreateDirectory(outputDirectoryPath);

            int j = 0;
            string pastLine = originFile.ReadLine();
            Element[] pastElements = ParseToElements(ParseLine(pastLine), _types);
            while (pastLine != null)
            {
                string path = outputDirectoryPath + @"\temp_" + j + ".txt";
                Console.WriteLine($"Создаем файл {path} и вставляем в него идущие подряд отсортированные элементы");
                Thread.Sleep(_delayTime);
                tables.Add(new TableWorker(path, this));
                using (var currentFile = new StreamWriter(path))
                {
                    int rowCount = 0;

                    currentFile.WriteLine(attributes);

                    while (true)
                    {
                        Console.WriteLine($"Запись [ {pastLine} ] в файл {path}");
                        Thread.Sleep(_delayTime);
                        currentFile.WriteLine(pastLine);
                        rowCount++;

                        if (!originFile.EndOfStream)
                        {
                            string line = originFile.ReadLine();
                            Element[] elements = ParseToElements(ParseLine(line), tables[j]._types);
                            if (pastElements[checkedColumnNum].CompareTo(elements[checkedColumnNum]) == dir)
                            {
                                pastLine = line;
                                pastElements = elements;
                                break;
                            }

                            pastLine = line;
                            pastElements = elements;
                        }
                        else
                        {
                            pastLine = null;
                            break;
                        }
                    }


                    tables[j].RowCount = rowCount;
                }

                PrintFile(path);
                j++;
            }
        }

        private void MergeSortedTables(string outputPath, List<string> inputPath, int columnNum, bool ascending)
        {
            Console.WriteLine("Сливаем файлы:");
            foreach (var data in inputPath)
            {
                Console.WriteLine(data);
            }

            Console.WriteLine($"В файл {outputPath}");
            Thread.Sleep(_delayTime);
            
            int dir = ascending ? 1 : -1;
            TableWorker[] tables = new TableWorker[inputPath.Count];
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i] = new TableWorker(inputPath[i], _delayTime);
            }

            StreamReader[] files = new StreamReader[inputPath.Count];

            for (int i = 0; i < inputPath.Count; i++)
            {
                files[i] = new StreamReader(inputPath[i]);
            }

            using (StreamWriter outputFile = new StreamWriter(outputPath))
            {
                outputFile.WriteLine(files[0].ReadLine());

                for (int i = 1; i < inputPath.Count; i++)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        files[i].ReadLine();
                    }
                }

                string[] lines = new string[inputPath.Count];
                for (int i = 0; i < inputPath.Count; i++)
                {
                    lines[i] = files[i].ReadLine();
                }

                Element[][] elements = new Element[inputPath.Count][];
                for (int i = 0; i < inputPath.Count; i++)
                {
                    elements[i] = ParseToElements(ParseLine(lines[i]), _types);
                }

                do
                {
                    int j = GetMinOrMaxElementNum(elements, inputPath, columnNum, dir);

                    if (j != -1)
                    {
                        Console.WriteLine($"Запись [ {lines[j]} ] в файл {outputPath}");
                        Thread.Sleep(_delayTime);
                        outputFile.WriteLine(lines[j]);
                        if (!files[j].EndOfStream)
                        {
                            lines[j] = files[j].ReadLine();
                            elements[j] = ParseToElements(ParseLine(lines[j]), _types);
                        }
                        else
                        {
                            lines[j] = null;
                            elements[j] = null!;
                        }
                    }
                    else
                    {
                        break;
                    }
                } while (true);

                foreach (var file in files)
                    file.Close();
            }

            PrintFile(outputPath);
        }

        int GetMinOrMaxElementNum(Element[][] element, List<string> inputPath, int columnNum, int dir)
        {
            Element[] currentLine = null;
            int output = -1;
            for (int i = 0; i < inputPath.Count; i++)
            {
                if (element[i] != null && (currentLine == null ||
                                           currentLine[columnNum].CompareTo(element[i][columnNum]) == dir))
                {
                    currentLine = element[i];
                    output = i;
                }
            }

            return output;
        }

        public void GetFilteredTable(TableWorker table, string outputPath, Condition condition)
        {
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            using StreamWriter outputFile = new StreamWriter(outputPath);
            using StreamReader inputFile = new StreamReader(table._filePath);
            outputFile.WriteLine(inputFile.ReadLine());

            while (!inputFile.EndOfStream)
            {
                string line = inputFile.ReadLine();
                Element[] elements = ParseToElements(ParseLine(line), table._types);
                if (condition.Satisfies(elements))
                {
                    outputFile.WriteLine(line);
                }
            }
        }
    }
}