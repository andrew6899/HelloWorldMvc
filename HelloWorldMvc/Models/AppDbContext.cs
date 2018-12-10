using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldMvc.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //default AppDbContext
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //use this area to override the default model properties
            //useful for inserting default values like the current date
            //into DateTime columns.
        }

        public DbSet<Message> Messages { get; set; }
    }
}
