using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Algorithms.Tester.classes;
using Algorithms.Tester.interfaces;

namespace AlgorhythmsLab3.Tester
{
    public class StepTester : ITester<double>, IStepTester
    {
        public StepTester()
        {
            AllResults = new List<TestResult<double>>();
        }
        
        public TestResult<double> LastResult { get; protected set; }
        public IList<TestResult<double>> AllResults { get; protected set; }

        public void Test(Func<(double, int)> algorithm, int iterNumber, string name)
        {
            var localResults = new double[iterNumber];
            for (int i = 0; i < iterNumber; i++)
            {
                var resultIter = algorithm.Invoke();
                localResults[i] = resultIter.Item2;
            }
            var resultId = AllResults.Count(x => x.AlgorithmName == name) + 1;
            var generalResult = localResults.Min();
            var testResult = new TestResult<double>(resultId, name, generalResult, localResults);
            LastResult = testResult;
            lock (AllResults) 
            {
                AllResults.Add(testResult); 
            }
        }

        public void SaveAsExcel(string path, string name, bool emissionsEnabled = true)
        {
            path = Path.Combine(path, name + ".xlsx");
            FileInfo file = new(path);
            var groupedResults = AllResults.GroupBy(x => x.AlgorithmName);

            foreach (var group in groupedResults)
            {
                var groupAr = group.ToArray();
                // if (!emissionsEnabled) Services.DeleteEmissions(groupAr);
                SaveManager.SaveTable(file, groupAr, "Степень", "Шаги");
            }
        }
    }
}