using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetCoreSampleApp.Model;

namespace AspNetCoreSampleApp
{
    public class AppDbContext : DbContext
    {
        private static bool _created = false;
        public AppDbContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().ToTable("Note");
        }

        public DbSet<Note> Notes { get; set; }

        public void Commit()
        {
            this.SaveChanges();
        }
    }
}
