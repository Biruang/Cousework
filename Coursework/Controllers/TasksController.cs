using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Coursework.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
		private SheduleDbContext db;
		public TasksController()
		{
			db = new SheduleDbContext();
		}

		[HttpGet]
		public IActionResult Get()
		{
			db.Tasks
				.Include(t=>t.Reminders)
				.Include(t=>t.Purpose)
				.Include(t=>t.TaskListTasks).ThenInclude(tl=>tl.TaskList)
				.Load();
			var tasks = db.Tasks;
			JArray response = new JArray();
			foreach(Models.Task task in tasks)
			{
				response.Add(Serializer.SerializeTask(task));
			}
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			db.Tasks
				.Include(t => t.Reminders)
				.Include(t => t.Purpose)
				.Include(t => t.TaskListTasks).ThenInclude(tl => tl.TaskList)
				.Load();
			var task = await db.Tasks.FindAsync(id);
			if (task == null)
			{
				return NotFound();
			}
			return Ok(Serializer.SerializeTask(task));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]Models.Task task)
		{
			if (task.CreationTime == null)
			{
				task.CreationTime = DateTime.Now;
			}
			try
			{
				await db.Tasks.AddAsync(task);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			await db.SaveChangesAsync();
			return CreatedAtAction("TaskCreated",task);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody]JObject input)
		{
			var task = await db.Tasks.FindAsync(id);
			if (task == null)
			{
				return NotFound();
			}
			JObject t = new JObject();
			try
			{
				task.Name = input.ContainsKey("name") ? input["name"].ToString() : task.Name;
				task.Description = input.ContainsKey("description") ? input["description"].ToString() : task.Description;
				task.Completed = input.ContainsKey("completed") ? input["completed"].Value<bool>() : task.Completed;
				db.Tasks.Update(task);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			await db.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var task = await db.Tasks.FindAsync(id);
			if (task == null) return NotFound();

			try
			{
				db.Tasks.Remove(task);
				await db.SaveChangesAsync();
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}

			return NoContent();
		}
    }
}