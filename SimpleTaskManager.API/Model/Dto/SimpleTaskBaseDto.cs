using System.ComponentModel.DataAnnotations;

namespace SimpleTaskManager.API.Model.Dto
{
    public class SimpleTaskBaseDto
    {
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        public string CreatedBy { get; set; }
    }
}
