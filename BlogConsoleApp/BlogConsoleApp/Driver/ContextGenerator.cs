using System;
using BlogConsoleApp.Controller;
using BlogConsoleApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BlogConsoleApp.Driver
{
	public partial class Driver
	{
        public static BlogConsoleAppDbContext ContextGenerator()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogConsoleAppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=BlogWebsiteDb;Username=postgres;Password=sd32qer");
            BlogConsoleAppDbContext dbContext = new BlogConsoleAppDbContext(optionsBuilder.Options);
            return dbContext;
        }
        public static PostManager CreatePostManager()
        {

            return new PostManager(ContextGenerator());
        }

        public static CommentManager CreateCommentManager()
        {

            return new CommentManager(ContextGenerator());
        }
    }
}

