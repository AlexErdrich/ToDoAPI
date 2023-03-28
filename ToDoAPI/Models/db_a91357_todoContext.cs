using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ToDoAPI.Models
{
    public partial class db_a91357_todoContext : DbContext
    {
        public db_a91357_todoContext()
        {
        }

        public db_a91357_todoContext(DbContextOptions<db_a91357_todoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ToDo> ToDos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=sql8004.site4now.net;Database=db_a91357_todo;UID=db_a91357_todo_admin;PWD=E7CBue3H4w3ckZz;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CatDesc).HasMaxLength(100);

                entity.Property(e => e.CatName).HasMaxLength(25);
            });

            modelBuilder.Entity<ToDo>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ToDos)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_ToDos_Categories");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
