using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using BlogConsoleApp.Controller;
using BlogConsoleApp.Model;

namespace BlogConsoleAppTest
{
	public class CommentManagerTest
	{
        private DbContextOptions<BlogConsoleAppDbContext> GetInMemoryDbContextOptions()
        {
            return new DbContextOptionsBuilder<BlogConsoleAppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task CreateCommentAsync_ValidInput_CreatesComment()
        {
            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var commentManager = new CommentManager(dbContext);

                await commentManager.CreateCommentAsync(1, "Test Comment");
                var createdComment = await dbContext.Comments.FirstOrDefaultAsync();
                Assert.NotNull(createdComment);
                Assert.Equal(1, createdComment.PostId);
                Assert.Equal("Test Comment", createdComment.Text);
            }
        }

        [Fact]
        public async Task GetCommentAsync_ExistingCommentId_ReturnsComment()
        {
            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var commentManager = new CommentManager(dbContext);

                var comment = new Comment { PostId = 1, Text = "Test Comment" };
                dbContext.Comments.Add(comment);
                await dbContext.SaveChangesAsync();

                var result = await commentManager.GetCommentAsync(comment.CommentId);

                Assert.NotNull(result);
                Assert.Equal(1, result.PostId);
                Assert.Equal("Test Comment", result.Text);
            }
        }

        [Fact]
        public async Task ChangeCommentTextAsync_ExistingCommentId_ChangesText()
        {
            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var commentManager = new CommentManager(dbContext);

                var comment = new Comment { PostId = 1, Text = "Old Comment" };
                dbContext.Comments.Add(comment);
                await dbContext.SaveChangesAsync();

                await commentManager.ChangeCommentTextAsync(comment.CommentId, "New Comment");

                var updatedComment = await dbContext.Comments.FindAsync(comment.CommentId);
                Assert.NotNull(updatedComment);
                Assert.Equal("New Comment", updatedComment.Text);
            }
        }

        [Fact]
        public async Task ChangeCommentTextAsync_NonExistingCommentId_ThrowsException()
        {
            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var commentManager = new CommentManager(dbContext);

                await Assert.ThrowsAsync<Exception>(async () =>
                {
                    await commentManager.ChangeCommentTextAsync(-1000, "New Comment");
                });
            }
        }

        [Fact]
        public async Task DeleteCommentAsync_ExistingCommentId_DeletesComment()
        {
            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var commentManager = new CommentManager(dbContext);

                var comment = new Comment { PostId = 1, Text = "Test Comment" };
                dbContext.Comments.Add(comment);
                await dbContext.SaveChangesAsync();

                var result = await commentManager.DeleteCommentAsync(comment.CommentId);

                Assert.True(result);
                var deletedComment = await dbContext.Comments.FindAsync(comment.CommentId);
                Assert.Null(deletedComment);
            }
        }

        [Fact]
        public async Task DeleteCommentAsync_NonExistingCommentId_ReturnsFalse()
        {
            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var commentManager = new CommentManager(dbContext);

                var result = await commentManager.DeleteCommentAsync(-1000);

                Assert.False(result);
            }
        }

        [Fact]
        public async Task GetCommentsOnPost_ExistingPostId_ReturnsComments()
        {
            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var commentManager = new CommentManager(dbContext);

                var post = new Post { Title = "Test Post", Content = "Test Content" };
                dbContext.Posts.Add(post);
                await dbContext.SaveChangesAsync();

                var comment1 = new Comment { PostId = post.PostId, Text = "Comment 1" };
                var comment2 = new Comment { PostId = post.PostId, Text = "Comment 2" };
                dbContext.Comments.Add(comment1);
                dbContext.Comments.Add(comment2);
                await dbContext.SaveChangesAsync();

                var comments = await commentManager.GetCommentsOnPost(post.PostId);

                Assert.NotNull(comments);
                Assert.Equal(2, comments.Count);
            }
        }
    }
}

