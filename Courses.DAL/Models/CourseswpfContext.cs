using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Courses.DAL.Models;

public partial class CourseswpfContext : DbContext
{
    public CourseswpfContext()
    {
    }

    public CourseswpfContext(DbContextOptions<CourseswpfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentsGroup> StudentsGroups { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\\\\\\\Users\\\\\\\\Oksana\\\\\\\\courseswpf.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.StudentId).ValueGeneratedNever();

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<StudentsGroup>(entity =>
        {
            entity.HasOne(d => d.Course).WithMany(p => p.StudentsGroups)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Teacher).WithMany(p => p.StudentsGroups).HasForeignKey(d => d.TeacherId);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.Property(e => e.TeacherId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
