using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Models
{
	public class SheduleDbContext : DbContext
	{
		public SheduleDbContext(): base()
		{
			Database.EnsureCreated();
		}

		public DbSet<Task> Tasks { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Purpose> Purposes { get; set; }
		public DbSet<TaskList> TaskLists { get; set; }
		public DbSet<Reminder> Reminders { get; set; }
		public DbSet<Accident> Accidents { get; set; }
		public DbSet<TaskListTask> TasksListTasks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Task>().HasData(
				new Task { Id = 1, Name = "testTask", CreationTime = DateTime.Now, Completed = false });
			modelBuilder.Entity<User>().HasData(
				new User { Login = "admin", Password = Convert.ToBase64String(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes("admin"))) });
			modelBuilder.Entity<Purpose>().HasData(
				new Purpose { Id = 1, Name = "testPurpose", CreationTime = DateTime.Now });
			modelBuilder.Entity<TaskList>().HasData(
				new TaskList { Id = 1, Name = "testTaskList", Color = "red" });
			modelBuilder.Entity<Reminder>().HasData(
				new Reminder { Id = 1, CreationTime = DateTime.Now, RepeatMode = 1, TriggerTime = (DateTime.Now.AddHours(1)), AccidentId = 1 });
			modelBuilder.Entity<Accident>().HasData(
				new Accident { Id = 1, Name = "testEvent", CreationTime = DateTime.Now });
			modelBuilder.Entity<TaskListTask>().HasData(
				new TaskListTask { Id = 1, TaskId = 1, TaskListId = 1 });
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SheduleDb;Trusted_Connection=True;");
		}
	}
}
