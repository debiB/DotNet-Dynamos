using System;
using BlogWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Data
{
    public class BlogWebApiDbContext : DbContext
    {
        public BlogWebApiDbContext(DbContextOptions<BlogWebApiDbContext> options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comment { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasIndex(e => e.PostId, "IX_Comment_PostId");
                entity.HasOne(c => c.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(x => x.PostId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Comment_Post");
            });
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

        }
    }
}

