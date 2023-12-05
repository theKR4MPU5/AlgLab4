using System;

namespace Algorithms.Tester.DataTypes
{
    public class Vector
    {
        private readonly int _size;
        private readonly int[] _values;
        
        public Vector(int size)
        {
            if (size < 1) size = 1;
            _size = size;
            _values = new int[_size];
        }

        public int Count => _values.Length;

        public static double[] RandomGenerate(int size)
        {
            Random rnd = new Random();
            var vector = new double[size];
            for (int i = 0; i < vector.Length; i++)
                vector[i] = rnd.Next();
            return vector;
        }
    }
}