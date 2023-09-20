using System;
using BlogConsoleApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BlogConsoleApp.Controller
{
	public class PostManager
	{
        public readonly BlogConsoleAppDbContext _dbContext = new();

        public PostManager(BlogConsoleAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int MaxSize
        {
            get { return 1 << 64; }
        }
        public async Task CreatePostAsync(string title, string content)
        {
            var newPost = new Post
            {
                Title = title,
                Content = content
            };

            _dbContext.Posts.Add(newPost);
            Console.WriteLine("Creating Post...");
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Post Created Successfully.");
        }
        public async Task<Post?> GetPostAsync(int PostId)
        {
            Console.WriteLine("Searching for post...");
            return await _dbContext.Posts.FindAsync(PostId);
        }

        public async Task ChangePostTitleAsync(int PostId, string NewTitle)
        {
            Post? Match = await GetPostAsync(PostId);
            if (Match is null)
            {
                Console.WriteLine("No matches for the required post");
                throw new Exception("No matches for the required post");
            }
            else
            {
                Match.Title = NewTitle;
            }
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Post title updated succesfully");
        }

        public async Task ChangePostContentAsync(int PostId, string NewContent)
        {
            Post? Match = await GetPostAsync(PostId);
            if (Match is null)
            {
                Console.WriteLine("No matches for the required post");
                throw new Exception("No matches for the required post");
            }
            else
            {
                Match.Content = NewContent;
            }
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Post Content updated succesfully");
        }

        public async Task<bool> DeletePostAsync(int PostId)
        {
            var Match = await GetPostAsync(PostId);
            if (Match is null)
            {
                Console.WriteLine("No matches found for the required post.");
                return false;
            }
            else
            {
                _dbContext.Posts.Remove(Match);
                _dbContext.SaveChanges();
                Console.WriteLine("Post removed.");
                return true;
            }
        }


        public async Task ShowAllPostsAsync()
        {
            Console.WriteLine("Fetching posts...");
            List<Post> AllPosts = await _dbContext.Posts.ToListAsync();
            Console.WriteLine("Here are all the posts in the database:");
            Console.WriteLine();
            foreach (var post in AllPosts)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine(post);
                Console.WriteLine("Comments:");
                foreach (var Comment in await new CommentManager(_dbContext).GetCommentsOnPost(post.PostId))
                {
                    Console.Write("\t|");
                    Console.WriteLine(Comment.ToStringSmall());
                }

            }
        }

        public async Task ShowAllPostsSmallAsync()
        {
            Console.WriteLine("Fetching posts...");
            List<Post> AllPosts = await _dbContext.Posts.ToListAsync();
            Console.WriteLine("Here are all the posts in the database:");
            Console.WriteLine();
            foreach (var post in AllPosts)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine(post.ToStringSmall());
            }
        }

        public async Task ShowBlog(int PostId)
        {
            var Match = await GetPostAsync(PostId);
            if (Match is null)
            {
                Console.WriteLine("No matches found for the required post.");
                throw new Exception("No matches found for the required post.");
            }
            else
            {
                Console.WriteLine(Match);
                foreach (var Comment in await new CommentManager(_dbContext).GetCommentsOnPost(Match.PostId))
                {
                    Console.Write("\t|");
                    Console.WriteLine(Comment.ToStringSmall());
                }

            }
        }
    }
}

