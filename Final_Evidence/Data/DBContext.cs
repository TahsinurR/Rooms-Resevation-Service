using Final_Evidence.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Final_Evidence.Data
{
	public class DBContext : DbContext
	{
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //singular to plural unchanged.
			base.OnModelCreating(modelBuilder);
		}
		public DBContext() : base("name=Model1")
		{

		}
		public DbSet<Rooms> Rooms { get; set; }
		public DbSet<Reservation> Reservation { get; set; }
	}

}