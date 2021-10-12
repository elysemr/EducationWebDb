using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationWebDb.Models;

namespace EducationWebDb.Models
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        public AppDbContext (DbContextOptions<AppDbContext> options) 
        : base (options)
        { }
        //what it looks like to define tables with fluent api instead of attributes, most things you can do with attributes but some things you can't
        //like making a column unique
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Major>(e =>
            {
                e.ToTable("Majors"); //table name
                e.HasKey(p => p.Id); // primary key
                e.Property(p => p.Code).HasMaxLength(15).IsRequired(true);
                e.HasIndex(p => p.Code).IsUnique(true); //makes a column unique
                e.Property(p => p.Description).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.MinSat);
            });

            builder.Entity<Student>(e =>
            {
                e.ToTable("Students");
                e.HasKey(p => p.Id);
                e.Property(p => p.Firstname).HasMaxLength(50).IsRequired(true);
                e.Property(p => p.Lastname).HasMaxLength(50).IsRequired(true);
                e.Property(p => p.SAT);
                e.Property(p => p.GPA).HasColumnType("decimal(5,2)");
                e.HasOne(p => p.Major).WithMany(p => p.Students).HasForeignKey(p => p.MajorId).OnDelete(DeleteBehavior.Restrict);
                //student will have 1 major instance attached to it, with many means a major will have many students, what virtual in major pointing to
                //withmany property is required to define FK, one to many relationship
                //ondelete overrides default of if delete major will delete all the students assoc with it
            });
      }

        public DbSet<EducationWebDb.Models.Major> Major { get; set; }

        public DbSet<EducationWebDb.Models.Student> Student { get; set; }
    }
}
