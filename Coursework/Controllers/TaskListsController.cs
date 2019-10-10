using Coursework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coursework.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaskListsController : ControllerBase
	{
		private SheduleDbContext db;
		public TaskListsController()
		{
			db = new SheduleDbContext();
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(db.TaskLists);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var taskList = await db.TaskLists.FindAsync(id);

			if (taskList == null)
			{
				return NotFound();
			}

			return Ok(taskList);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]TaskList taskList)
		{
			if (taskList == null)
			{
				return BadRequest("Task list doesn't exist");
			}

			try
			{
				await db.TaskLists.AddAsync(taskList);
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}

			await db.SaveChangesAsync();
			return CreatedAtAction("TaskListPost", taskList);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody]JObject input)
		{
			var taskList = await db.TaskLists.FindAsync(id);
			if(taskList == null)
			{
				return NotFound();
			}

			try
			{
				taskList.Name = input.ContainsKey("name") ? input["name"].ToString() : taskList.Name;
				taskList.Color = input.ContainsKey("color") ? input["color"].ToString() : taskList.Color;
				db.TaskLists.Update(taskList);
			}
			catch(Exception e)
			{
				return BadRequest(e);
			}

			await db.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var taskList = await db.TaskLists.FindAsync(id);
			if (taskList == null) return NotFound();
			try
			{
				db.TaskLists.Remove(taskList);
				await db.SaveChangesAsync();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
			return NoContent();
		}

		[HttpGet("{id}/tasks")]
		async public Task<IActionResult> GetTasks(int id)
		{
			await db.TaskLists.Where(t => t.Id == id).Include(t => t.TaskListTasks).ThenInclude(t => t.Task).LoadAsync();
			var taskList = await db.TaskLists.FindAsync(id);
			if (taskList == null) return NotFound();

			var tasks = taskList.TaskListTasks.Select(t => t.Task);
			JArray response = new JArray();
			foreach(var task in tasks)
			{
				response.Add(Serializer.SerializeTask(task));
			}
			return Ok(response);
		}

		[HttpPost("{id}/tasks/{taskId}")]
		async public Task<IActionResult> AddTaskInList(int id, int taskId)
		{
			if (await db.TaskLists.FindAsync(id) == null) ModelState.AddModelError("Database","Task list with this id don't exist");
			if (await db.Tasks.FindAsync(taskId) == null) ModelState.AddModelError("Database", "Task with this id don't exist");
			foreach(var taskListTask in db.TasksListTasks) {
				if ((taskListTask.TaskId == taskId) && (taskListTask.TaskListId == id))
				{
					ModelState.AddModelError("Database", "This relationship already exist");
					break;
				}
			}
			if (!ModelState.IsValid) return BadRequest(ModelState);

			await db.TasksListTasks.AddAsync(new TaskListTask
			{
				TaskId = taskId,
				TaskListId = id
			});
			db.SaveChanges();
			return NoContent();
		}

		[HttpPost("{id}/tasks")]
		public async Task<IActionResult> CreateTaskInList(int id,[FromBody]Models.Task task)
		{
			if (await db.TaskLists.FindAsync(id) == null) return NotFound("Task list with this id don't exist");
			if (task.CreationTime == null) task.CreationTime = DateTime.Now;

			await db.Tasks.AddAsync(task);
			await db.TasksListTasks.AddAsync(new TaskListTask
			{
				TaskId = task.Id,
				TaskListId = id
			});
			db.SaveChanges();

			return Created("",Serializer.SerializeTask(task));
		}

		[HttpDelete("{id}/tasks/{taskId}")]
		public async Task<IActionResult> DeleteTaskFromList(int id,int taskId)
		{
			if (await db.TaskLists.FindAsync(id) == null) return NotFound("Task list with this id don't exist");
			TaskListTask find = new TaskListTask();
			foreach (var taskListTask in db.TasksListTasks)
			{
				if ((taskListTask.TaskId == taskId) && (taskListTask.TaskListId == id))
				{
					find = taskListTask;
					break;
				}
			}
			if(find.TaskList == null) return NotFound("This relationship doesn't exist");
			db.TasksListTasks.Remove(find);
			await db.SaveChangesAsync();
			return NoContent();
		}
	}
}
