using System;
using System.IO;

namespace AlgSort
{
    public class Generate
    {


        public static void GenerateText(int size)
        {
            // Генерация случайных слов и запись в файл
            Random random = new Random();
            string[] words = new string[size];
            string filePath = Directory.GetCurrentDirectory() + "\\..\\..\\TextFile1.txt";

            for (int i = 0; i < size; i++)
            {
                words[i] = GenerateRandomWord();
            }

            File.WriteAllText(filePath, string.Join(" ", words));
            Console.WriteLine($"Сгенерирован и записан файл '{filePath}' с {size} словами.");
        }

        private static string GenerateRandomWord()
        {
            // Ваш код для генерации случайного слова
            // Например, можно использовать случайные буквы или словарь слов
            // В данном примере, просто используем буквы от 'a' до 'z' длиной от 3 до 8 символов
            Random random = new Random();
            int length = random.Next(3, 9);

            char[] letters = new char[length];
            for (int i = 0; i < length; i++)
            {
                letters[i] = (char)('a' + random.Next(26));
            }

            return new string(letters);
        }
    }
}
