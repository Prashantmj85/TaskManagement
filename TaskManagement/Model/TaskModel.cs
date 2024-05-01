using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Model
{
    public class TaskModel
    {
        // Properties
        public int TaskId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }

    }
}
