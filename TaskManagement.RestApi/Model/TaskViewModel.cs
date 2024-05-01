using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace TaskManagement.Model
{
    public class TaskViewModel
    {
        public List<TaskModel> Tasks { get; set; }
        public TaskViewModel()
        {
            Tasks = new List<TaskModel>();
        }

        public int TotalTask { get; set; }

        public int PageSize { get; set; }
    }
}
