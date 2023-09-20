using System;
using BlogConsoleApp.Controller;
using BlogConsoleApp.Model;
using Microsoft.EntityFrameworkCore;

namespace BlogConsoleAppTest
{
	public class PostManagerTest
	{
        private DbContextOptions<BlogConsoleAppDbContext> GetInMemoryDbContextOptions()
        {
            return new DbContextOptionsBuilder<BlogConsoleAppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }


        // test valid Create
        [Fact]
        public async Task CreatePostAsync_ValidInput_CreatesPost()
        {

            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var postManager = new PostManager(dbContext);

                await postManager.CreatePostAsync("Test Title", "Test Content");
                var createdPost = await dbContext.Posts.FirstOrDefaultAsync();
                Assert.NotNull(createdPost);
                Assert.Equal("Test Title", createdPost.Title);
                Assert.Equal("Test Content", createdPost.Content);
            }
        }

        // tests valid get
        [Fact]
        public async Task GetPostAsync_ExistingPostId_ReturnsPost()
        {

            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var postManager = new PostManager(dbContext);


                var post = new Post { Title = "Test Post", Content = "Test Content" };
                dbContext.Posts.Add(post);
                await dbContext.SaveChangesAsync();


                var result = await postManager.GetPostAsync(post.PostId);


                Assert.NotNull(result);
                Assert.Equal("Test Post", result.Title);
                Assert.Equal("Test Content", result.Content);
            }
        }

        // tests valid change of title
        [Fact]
        public async Task ChangePostTitleAsync_ExistingPostId_ChangesTitle()
        {

            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var postManager = new PostManager(dbContext);


                var post = new Post { Title = "Old Title", Content = "Test Content" };
                dbContext.Posts.Add(post);
                await dbContext.SaveChangesAsync();


                await postManager.ChangePostTitleAsync(post.PostId, "New Title");


                var updatedPost = await dbContext.Posts.FindAsync(post.PostId);
                Assert.NotNull(updatedPost);
                Assert.Equal("New Title", updatedPost.Title);
            }
        }

        [Fact]
        public async Task ChangePostTitleAsync_NonExistingPostId_ChangesTitle()
        {

            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var postManager = new PostManager(dbContext);
                await Assert.ThrowsAsync<Exception>(async () =>
                {
                    await postManager.ChangePostTitleAsync(-1000, "New Title");
                });

            }
        }
        [Fact]
        public async Task ChangePostContentAsync_ExistingPostId_ChangesContent()
        {

            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var postManager = new PostManager(dbContext);


                var post = new Post { Title = "Test Title", Content = "Old Content" };
                dbContext.Posts.Add(post);
                await dbContext.SaveChangesAsync();


                await postManager.ChangePostContentAsync(post.PostId, "New Content");


                var updatedPost = await dbContext.Posts.FindAsync(post.PostId);
                Assert.NotNull(updatedPost);
                Assert.Equal("New Content", updatedPost.Content);
            }
        }

        [Fact]
        public async Task ChangePostContentAsync_NonExistingPostId_ChangesContent()
        {

            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var postManager = new PostManager(dbContext);
                await Assert.ThrowsAsync<Exception>(async () =>
                {
                    await postManager.ChangePostTitleAsync(-1000, "New Content");
                });

            }
        }

        // tests valid delete
        [Fact]
        public async Task DeletePostAsync_ExistingPostId_DeletesPost()
        {
            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var postManager = new PostManager(dbContext);

                var post = new Post { Title = "Test Title", Content = "Test Content" };
                dbContext.Posts.Add(post);
                await dbContext.SaveChangesAsync();

                var result = await postManager.DeletePostAsync(post.PostId);

                Assert.True(result);
                var deletedPost = await dbContext.Posts.FindAsync(post.PostId);
                Assert.Null(deletedPost);
            }
        }

        // tests valid show
        [Fact]
        public async Task ShowBlog_NonExistingPostId()
        {
            using (var dbContext = new BlogConsoleAppDbContext(GetInMemoryDbContextOptions()))
            {
                var postManager = new PostManager(dbContext);
                await Assert.ThrowsAsync<Exception>(async () =>
                {
                    await postManager.ShowBlog(-1000);
                });

            }
        }

    }
}


