using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleTaskManager.API.Model.BLL
{
    public class SimpleTask
    {
        public SimpleTask()
        {
            NumberOfUpdates = 1;
        }
        /// <summary>
        /// Name has to be unique. It's the primary key.
        /// </summary>
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }
        public Status Status { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdateBy { get; set; }
        public int NumberOfUpdates { get; set; }

        public void Update(SimpleTask task)
        {
            if (task == null)
                throw new ArgumentNullException("Task is null.");

            Name = task.Name;
            Status = task.Status;
            CreationDateTime = task.CreationDateTime;
            LastUpdateDateTime = task.LastUpdateDateTime;
            CreatedBy = task.CreatedBy;
            LastUpdateBy = task.LastUpdateBy;
            NumberOfUpdates = task.NumberOfUpdates;
        }

        public void Patch(SimpleTask task)
        {
            if (task == null)
                throw new ArgumentNullException("Task is null.");

            Name = task.Name;
            Status = task.Status;
            LastUpdateDateTime = task.LastUpdateDateTime;
            LastUpdateBy = task.LastUpdateBy;
            NumberOfUpdates = task.NumberOfUpdates;
        }
    }

}
