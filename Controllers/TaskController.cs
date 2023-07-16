using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskBackend.Models;

namespace TaskBackend.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly List<Models.Task> _tasks;

        public TaskController()
        {
            _tasks = new List<Models.Task>
        {

            new Models.Task
            {
                Id = 1,
                Name = "Task_1",
            Steps = new List<Step> {new Step { Name = "Step_1_1", Owner = "Marcel" },
            new Step
            {
                Name = "Step_1_2",
                Owner = "Joe",
                SubSteps = new List<Step>
                {
                    new Step { Name = "Step_1_2_1", Owner = "anonymous" },
                    new Step { Name = "Step_1_2_2", Owner = "anonymous" }
                }
            },
            new Step { Name = "Step_1_3", Owner = "Joe" }
             }
            },
    new Models.Task
    {   Id=2,
        Name = "Task_2",
        Steps = new List<Step>
        {
            new Step
            {
                Name = "Step_2_1",
                Owner = "Bob",
                SubSteps = new List<Step>
                {
                    new Step { Name = "Step_2_1_1", Owner = "Bob & Alice" },
                    new Step { Name = "Step_2_1_2", Owner = "Bob & Max" }
                }
            },
            new Step { Name = "Step_2_2", Owner = "Joe" },
            new Step { Name = "Step_2_3", Owner = "Joe" }
        }
    }, 
    new Models.Task
    {   Id=3,
        Name = "Task_3",
        Steps = new List<Step>
        {
            new Step
            {
                Name = "Step_2_1",
                Owner = "Bea",
                SubSteps = new List<Step>
                {
                    new Step { Name = "Step_2_1_1", Owner = "Bea & Maricica" },
                }
            },
            new Step { Name = "Step_2_2", Owner = "Ion" },
            new Step { Name = "Step_2_3", Owner = "Ionel" }
        }
    }
    };
        }

        [HttpGet]
        public ActionResult<List<Models.Task>> GetTasks()
        {
            return _tasks;
        }

        [HttpPost]
        public ActionResult<Models.Task> CreateTask(Models.Task task)
        {
            //incrementuing the id
            var taskId = _tasks.Count + 1;
            task.Id = taskId;

            _tasks.Add(task);

            return CreatedAtAction(nameof(GetTasks), new { id = taskId }, task);
        }

        [HttpGet("{id}")]
        public ActionResult<Models.Task> GetTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        [HttpPut("{id}")]
        public ActionResult<Models.Task> UpdateTask(int id, Models.Task task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Name = task.Name;
            existingTask.Steps = task.Steps;

            return existingTask;
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            _tasks.Remove(task);

            return NoContent();
        }

    }
}