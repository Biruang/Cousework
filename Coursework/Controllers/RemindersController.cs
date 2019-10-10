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
	public class RemindersController: ControllerBase
	{
		private SheduleDbContext db;
		public RemindersController()
		{
			db = new SheduleDbContext();
		}

		[HttpGet]
		public IActionResult Get()
		{
			db.Reminders.Include(t => t.Task).Include(t => t.Accident).Load();
			var reminders = db.Reminders;

			return Ok(reminders);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			await db.Reminders.Where(t => t.Id == id).Include(t => t.Task).Include(t => t.Accident).LoadAsync();
			var reminder = await db.Reminders.FindAsync(id);

			if (reminder == null)
			{
				return NotFound();
			}

			return Ok(reminder);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]Reminder reminder)
		{
			if (reminder == null)
			{
				ModelState.AddModelError("", "Model doesn't exist");
			}

			try
			{
				await db.Reminders.AddAsync(reminder);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			await db.SaveChangesAsync();
			return CreatedAtAction("ReminderPost", reminder);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody]Reminder inputReminder)
		{
			var reminder = await db.Reminders.FindAsync(id);

			if (reminder == null)
			{
				return NotFound();
			}

			try
			{
				reminder.RepeatMode = inputReminder.RepeatMode;
				reminder.TriggerTime = inputReminder.TriggerTime;

				db.Reminders.Update(reminder);
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}

			await db.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var reminder = await db.Reminders.FindAsync(id);
			if (reminder == null) return NotFound();

			try
			{
				db.Reminders.Remove(reminder);
			}
			catch(Exception e)
			{
				return BadRequest(e);
			}

			await db.SaveChangesAsync();
			return NoContent();
		}
	}
}
