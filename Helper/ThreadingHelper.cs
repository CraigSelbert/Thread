using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Threading.Helper
{
    public static class TaskManager
    {
        public static void Process<T>(IEnumerable<T> list, Action<T> action, ThreadingOption threadingOption = ThreadingOption.Full, int maxDegreeOfParallelism = 10, int delayBetweenCalls = 0)
        {
            TaskScheduler taskScheduler;

            switch (threadingOption)
            {
                case ThreadingOption.Single:
                    taskScheduler = new LimitedConcurrencyLevelTaskScheduler(1, delayBetweenCalls);
                    break;
                case ThreadingOption.Limited:
                    taskScheduler = new LimitedConcurrencyLevelTaskScheduler(maxDegreeOfParallelism, delayBetweenCalls);
                    break;
                default:
                    taskScheduler = new LimitedConcurrencyLevelTaskScheduler(int.MaxValue, delayBetweenCalls);
                    break;
            }

            var taskFactory = new TaskFactory(taskScheduler);

            Task.WaitAll(list.Select(t => taskFactory.StartNew(() => { action(t); })).ToArray());
        }
    }
}