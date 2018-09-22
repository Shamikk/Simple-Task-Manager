using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleTaskManager.API.Model.BLL.External
{
    public static class TheBestTaskNameCheckerInTheWorld
    {
        public static HashSet<string> Names { get; private set; }
        public static void Seed()
        {
            Names = new HashSet<string>();

            for(int i = 0; i < 100; i++)
            {
                Names.Add($"Task {i}");
            }
        }

        public static bool Check(string name)
        {
            var randomWaitTime = 5 + new Random().Next(0, 5);
            Thread.Sleep(randomWaitTime * 1000);
            return Names.Any(x => x == name);
        }

        public static void SyncNames(IEnumerable<string> taskNames)
        {
            if (Names == null)
                Names = new HashSet<string>();

            foreach(var taskName in taskNames)
            {
                var existingName = Names.SingleOrDefault(x => x == taskName);
                if(existingName == null)
                {
                    Names.Add(taskName);
                }
            }
        }
    }
}
