﻿namespace Algorithms.Tester.classes
{
    public record TestResult<TResult>(int ID, string AlgorithmName, TResult Result, TResult[] LocalResults);
}
