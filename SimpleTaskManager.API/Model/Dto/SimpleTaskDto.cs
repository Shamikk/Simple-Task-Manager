using System;

namespace SimpleTaskManager.API.Model.Dto
{
    public class SimpleTaskDto : SimpleTaskBaseDto
    {
        public DateTime CreationDateTime { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string Status { get; set; }
        public int NumberOfUpdates { get; set; }
    }
}
