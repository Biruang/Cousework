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
	public class PurposesController : ControllerBase
	{
		private SheduleDbContext db;
		public PurposesController()
		{
			db = new SheduleDbContext();
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(db.Purposes);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var purpouse = await db.Purposes.FindAsync(id);

			if (purpouse == null)
			{
				return NotFound();
			}

			return Ok(purpouse);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]Purpose purpouse)
		{
			if (purpouse == null)
			{
				return BadRequest("Purpouse doesn't exist");
			}

			try
			{
				await db.Purposes.AddAsync(purpouse);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			await db.SaveChangesAsync();
			return CreatedAtAction("PurpousePost", purpouse);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody]JObject input)
		{
			var purpouse = await db.Purposes.FindAsync(id);
			if (purpouse == null)
			{
				return NotFound();
			}

			try
			{
				purpouse.Name = input.ContainsKey("name") ? input["name"].ToString() : purpouse.Name;
				purpouse.Description = input.ContainsKey("description") ? input["description"].ToString() : purpouse.Description;
				db.Purposes.Update(purpouse);
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
			var purpouse = await db.Purposes.FindAsync(id);
			if (purpouse == null) return NotFound();

			try
			{
				db.Purposes.Remove(purpouse);
				await db.SaveChangesAsync();
			}
			catch(Exception e)
			{
				return BadRequest(e);
			}

			return NoContent();
		}

		[HttpGet("{id}/tasks")]
		async public Task<IActionResult> GetTasks(int id)
		{
			await db.Purposes.Where(t => t.Id == id).Include(t => t.Tasks).LoadAsync();
			var purpouse = await db.Purposes.FindAsync(id);
			if (purpouse == null) return NotFound();

			JArray response = new JArray();
			foreach (var task in purpouse.Tasks)
			{
				response.Add(Serializer.SerializeTask(task));
			}
			return Ok(response);
		}

		[HttpPost("{id}/tasks/{taskId}")]
		async public Task<IActionResult> AddTaskInPurpose(int id, int taskId)
		{
			var task = await db.Tasks.FindAsync(taskId);

			if (await db.Purposes.FindAsync(id) == null) ModelState.AddModelError("Database", "Purpose with this id don't exist");
			if (task == null) ModelState.AddModelError("Database", "Task with this id don't exist");
			if (!ModelState.IsValid) return BadRequest(ModelState);
			if (task.PurposeId == taskId) ModelState.AddModelError("Database", "This relationship already exist");
			if (task.PurposeId != null) ModelState.AddModelError("Database", "This task already have purpose");
			if (!ModelState.IsValid) return BadRequest(ModelState);

			task.PurposeId = id;
			await db.SaveChangesAsync();
			return NoContent();
		}

		[HttpPost("{id}/tasks")]
		public async Task<IActionResult> CreatePurposeInList(int id, [FromBody]Models.Task task)
		{
			if (await db.Purposes.FindAsync(id) == null) return NotFound("Purpose with this id don't exist");
			if (task.CreationTime == null) task.CreationTime = DateTime.Now;

			task.PurposeId = id;
			await db.Tasks.AddAsync(task);
			db.SaveChanges();

			return Created("", Serializer.SerializeTask(task));
		}

		[HttpDelete("{id}/tasks/{taskId}")]
		public async Task<IActionResult> DeleteTaskFromPurpose(int id, int taskId)
		{
			var task = await db.Tasks.FindAsync(taskId);
			if (await db.Purposes.FindAsync(id) == null) ModelState.AddModelError("Database", "Purpose with this id don't exist");
			if (task == null) ModelState.AddModelError("Database", "Task with this id don't exist");
			if (!ModelState.IsValid) return BadRequest(ModelState);
			if (task.PurposeId != id) return BadRequest("Task doesn't have relationship with this purpose");

			task.PurposeId = null;
			await db.SaveChangesAsync();
			return NoContent();
		}
	}
}
