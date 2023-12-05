using System;

namespace Algorithms.Tester.DataTypes
{
    public class Matrix
    {
        private readonly double[,] _values;

        private int RowLength { get; }
        private int ColumnLength { get; }

        public Matrix(int rowLength, int columnLength)
        {
            if (rowLength < 1) rowLength = 1;
            if (columnLength < 1) columnLength = 1;
            
            RowLength = rowLength;
            ColumnLength = columnLength;
            _values = new double[rowLength, columnLength];
        }

        public static double[,] RandomGenerate(int row, int column)
        {
            if (row < 1) row = 1;
            if (column < 1) column = 1;
            
            var rnd = new Random();
            double[,] matrix = new double[row, column];
            
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = rnd.Next(0, int.MaxValue);
                }
            }

            return matrix;
        }
    }
}