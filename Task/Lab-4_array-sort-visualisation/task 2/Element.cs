using System;

namespace AlgSort.task_2
{
    internal readonly struct Element : IComparable
    {
        public object Value { get; }
        private TableWorker.ColumnType Type { get; }

        public Element(string value, TableWorker.ColumnType type)
        {
            Type = type;
            switch (type)
            {
                case TableWorker.ColumnType.Integer:
                    Value = int.Parse(value);
                    break;
                case TableWorker.ColumnType.String:
                    Value = value;
                    break;
                default:
                    throw new Exception("Тип не реализован");
            }
        }

        public int CompareTo(object? obj)
        {
            if (obj is not Element otherElement) throw new Exception("Нельзя сравнить с другими типами");
            if (Type != otherElement.Type)
                throw new Exception("Попытка сравнить элементы, хрaнящие разные типы данных");

            var value1 = (IComparable) Value;
            var value2 = (IComparable) otherElement.Value;

            return value1.CompareTo(value2);
        }
    }
}