using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coursework.Models
{
	public class Task
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		public bool Completed { get; set; }
		[Range(typeof(DateTime),"2019-01-01T00:00:00", "3000-01-01T00:00:00")]
		public DateTime? CreationTime { get; set; }

		public int? PurposeId { get; set; }
		public Purpose Purpose { get; set; }

		public List<Reminder> Reminders { get; set; }
		public List<TaskListTask> TaskListTasks { get; set; }

		public Task()
		{
			TaskListTasks = new List<TaskListTask>();
			Reminders = new List<Reminder>();
		}
	}
}
