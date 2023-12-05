using AlgSort;

namespace AlgSort.algorithms
{
    public abstract class Algorithm<T>
    {
        public T[] Data { get; set; }
        public int SortDelay { get; set; }

        protected Algorithm(T[] data, int sortDelay)
        {
            Data = data;
            SortDelay = sortDelay;
        }

        protected Algorithm() { }

        public abstract void Sort();
    }
}