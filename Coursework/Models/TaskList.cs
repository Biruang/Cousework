using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Coursework.Models
{
	public class TaskList
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Color { get; set; }

		[JsonIgnore]
		public List<TaskListTask> TaskListTasks { get; set; }

		public TaskList()
		{
			TaskListTasks = new List<TaskListTask>();
		}
	}
}
