using System;

namespace Algorithms.Tester.interfaces
{
    public interface ITimeTester
    {
        public void Test(Action algorithm, int iterNumber, string name);
    }
}