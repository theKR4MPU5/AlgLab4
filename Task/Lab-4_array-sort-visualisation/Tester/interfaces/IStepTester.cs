using System;

namespace Algorithms.Tester.interfaces
{
    public interface IStepTester
    {
        public void Test(Func<(double, int)> algorithm, int iterNumber, string name);
    }
}