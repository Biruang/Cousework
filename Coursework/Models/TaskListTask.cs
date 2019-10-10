using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coursework.Models
{
	public class TaskListTask
	{
		public int Id { get; set; }

		public int TaskId { get; set; }
		public Task Task { get; set; }

		public int TaskListId { get; set; }
		public TaskList TaskList { get; set; }
	}
}
