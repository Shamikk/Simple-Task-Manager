using SimpleTaskManager.API.Model.BLL.External;
using SimpleTaskManager.Core.BLL;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SimpleTaskManager.API.Model.BLL
{
    public class ValidatorService : IValidatorService
    {
        private static HashSet<SimpleTask> _simpleTasksQueue;

        public ValidatorService()
        {
            if (TheBestTaskNameCheckerInTheWorld.Names == null || TheBestTaskNameCheckerInTheWorld.Names.Count == 0)
                TheBestTaskNameCheckerInTheWorld.Seed();

            if (_simpleTasksQueue == null)
                _simpleTasksQueue = new HashSet<SimpleTask>();
        }
        public bool Validate(SimpleTask task)
        {
            bool isValid = false;
            bool isCheckerListLocked = true;
            do
            {
                if (Monitor.TryEnter(TheBestTaskNameCheckerInTheWorld.Names, 0))
                {
                    isCheckerListLocked = true;
                    try
                    {
                        lock (TheBestTaskNameCheckerInTheWorld.Names)
                        {
                            isValid = !TheBestTaskNameCheckerInTheWorld.Check(task.Name);
                            if (isValid)
                                TheBestTaskNameCheckerInTheWorld.Names.Add(task.Name);
                        }
                    }
                    finally
                    {
                        Monitor.Exit(TheBestTaskNameCheckerInTheWorld.Names);
                        isCheckerListLocked = false;
                    }
                }
            } while (isCheckerListLocked);

            return isValid;
        }
    }
}
