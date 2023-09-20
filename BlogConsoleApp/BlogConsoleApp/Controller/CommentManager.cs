using System;
using BlogConsoleApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BlogConsoleApp.Controller
{
	public class CommentManager
	{
        private readonly BlogConsoleAppDbContext _dbContext;

        public int MaxSize
        {
            get { return 1 << 64; }
        }

        public CommentManager(BlogConsoleAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateCommentAsync(int PostId, string Text)
        {
            var newComment = new Comment
            {
                PostId = PostId,
                Text = Text
            };

            try
            {
                _dbContext.Comments.Add(newComment);

                Console.WriteLine("Creating Comment...");
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Comment Created Successfully.");
        }
        public async Task<Comment?> GetCommentAsync(int CommentId)
        {
            Console.WriteLine("Searching for Comment ...");
            return await _dbContext.Comments.FindAsync(CommentId);
        }

        public async Task ChangeCommentTextAsync(int CommetId, string NewText)
        {
            Comment? Match = await GetCommentAsync(CommetId);
            if (Match is null)
            {
                Console.WriteLine("No matches for the required Comment");
                throw new Exception("No matches for the required Comment");
            }
            else
            {
                Match.Text = NewText;
            }
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Comment text updated succesfully");
        }

        public async Task<bool> DeleteCommentAsync(int CommentId)
        {
            var Match = await GetCommentAsync(CommentId);
            if (Match is null)
            {
                Console.WriteLine("No matches found for the required cpmment.");
                return false;
            }
            else
            {
                _dbContext.Comments.Remove(Match);
                _dbContext.SaveChanges();
                Console.WriteLine("Comment removed.");
                return true;
            }
        }

        public async Task<List<Comment>> GetCommentsOnPost(int PostId)
        {
            List<Comment> CommentsOnPost = await _dbContext.Comments.Where(c => c.PostId == PostId).ToListAsync();
            return CommentsOnPost;
        }
    }
}

