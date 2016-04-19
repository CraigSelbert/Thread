using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Threading.Helper;

namespace Threading.Console
{
    class Program
    {
        private static List<int> threadIds;

        static void Main(string[] args)
        {
            var listOfNumbers = Enumerable.Range(1000, 100).ToList();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            stopWatch.Restart();
            threadIds = new List<int>();
            TaskManager.Process(listOfNumbers, Process, ThreadingOption.Single);
            System.Console.WriteLine($"It took {stopWatch.Elapsed.Seconds} seconds to process {listOfNumbers.Count} integers using threads {string.Join(",", threadIds)} ");

            stopWatch.Restart();
            threadIds = new List<int>();
            TaskManager.Process(listOfNumbers, Process, ThreadingOption.Limited);
            System.Console.WriteLine($"It took {stopWatch.Elapsed.Seconds} seconds to process {listOfNumbers.Count} integers using threads {string.Join(",", threadIds)} ");

            stopWatch.Restart();
            threadIds = new List<int>();
            TaskManager.Process(listOfNumbers, Process, ThreadingOption.Full);
            System.Console.WriteLine($"It took {stopWatch.Elapsed.Seconds} seconds to process {listOfNumbers.Count} integers using threads {string.Join(",", threadIds)} ");

            System.Console.ReadLine();
        }

        private static void Process(int x)
        {
            if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                threadIds.Add(Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(100);
        }
    }
}
