using Coursework.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coursework.Controllers
{
	static public class Serializer
	{
		static public JObject SerializeTask(Models.Task task)
		{
			JArray reminders = new JArray();
			foreach (Reminder reminder in task.Reminders)
			{
				reminders.Add(new JObject
				{
					["id"] = reminder.Id,
					["repeatMode"] = reminder.RepeatMode,
					["triggerTime"] = reminder.TriggerTime
				});
			}
			JArray taskLists = new JArray();
			var lists = task.TaskListTasks.Select(t => t.TaskList).ToList();
			foreach (var list in lists)
			{
				taskLists.Add(new JObject
				{
					["id"] = list.Id,
					["name"] = list.Name,
					["color"] = list.Color
				});
			}
			JObject response = new JObject
			{
				["id"] = task.Id,
				["name"] = task.Name,
				["description"] = task.Description,
				["completed"] = task.Completed,
				["creationTime"] = task.CreationTime,
				["purpouse"] = (task.Purpose == null) ? null : new JObject
				{
					["id"] = task.Purpose.Id,
					["name"] = task.Purpose.Name
				},
				["reminders"] = reminders,
				["taskLists"] = taskLists
			};
			return response;
		}
	}
}
