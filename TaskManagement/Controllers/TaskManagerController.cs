using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Model;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskManagerController : ControllerBase
    {
        private static List<TaskModel> tasks = new List<TaskModel>
        {
            new TaskModel { TaskId = 1, Title = "Task 1", Description = "Description 1", DueDate = DateTime.Today.AddDays(7), IsCompleted = false },
            new TaskModel { TaskId = 2, Title = "Task 2", Description = "Description 2", DueDate = DateTime.Today.AddDays(14), IsCompleted = false }
        };


        private readonly ILogger<TaskManagerController> _logger;

        public TaskManagerController(ILogger<TaskManagerController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create(TaskModel taskModel)
        {
            if (taskModel == null)
            {
                return BadRequest();
            }
            else if (ModelState.IsValid)
            {
                tasks.Add(taskModel);
                return new ObjectResult(taskModel) { StatusCode = StatusCodes.Status201Created };
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    
   
        [HttpGet(Name = "GetTasks")]
        public IActionResult Get()
        {
            TaskViewModel taskVM = new TaskViewModel
            {
                Tasks = tasks,
                TotalTask = tasks.Count,
                PageSize = 10
            };
            return Ok(taskVM); ;
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
    }
}
