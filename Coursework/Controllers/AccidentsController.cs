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
	public class AccidentsController: ControllerBase
	{
		private SheduleDbContext db;
		public AccidentsController()
		{
			db = new SheduleDbContext();
		}

		[HttpGet]
		public IActionResult Get()
		{
			db.Accidents.Include(t => t.Reminder).Load();
			var accidents = db.Accidents;

			return Ok(accidents);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			await db.Accidents.Where(t => t.Id == id).Include(t => t.Reminder).LoadAsync();
			var accident = await db.Accidents.FindAsync(id);

			if (accident == null)
			{
				return NotFound();
			}

			return Ok(accident);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]Accident accident)
		{
			if (accident == null) return BadRequest("Accident doesn't exist");

			try
			{
				await db.Accidents.AddAsync(accident);
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}

			await db.SaveChangesAsync();
			return CreatedAtAction("AccidentAction", accident);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody]Accident inputAccident)
		{
			var accident = await db.Accidents.FindAsync(id);
			if (accident == null) return NotFound();

			try
			{
				accident.Name = inputAccident.Name;
				accident.Description = inputAccident.Description;

				db.Accidents.Update(accident);
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
			var accident = await db.Accidents.FindAsync(id);
			if (accident == null) return NotFound();

			try
			{
				db.Accidents.Remove(accident);
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
