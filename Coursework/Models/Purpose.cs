using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coursework.Models
{
	public class Purpose
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Range(typeof(DateTime), "2019-01-01T00:00:00", "3000-01-01T00:00:00")]
		public DateTime CreationTime { get; set; }
		public string Description { get; set; }

		[JsonIgnore]
		public List<Task> Tasks { get; set; }

		public Purpose()
		{
			Tasks = new List<Task>();
		}
	}
}
