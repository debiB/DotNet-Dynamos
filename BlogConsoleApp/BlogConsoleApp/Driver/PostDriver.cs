using System;
namespace BlogConsoleApp.Driver
{
    public partial class Driver
    {
        private async Task MakePost()
        {
            var postManager = CreatePostManager();
            string title = DisplayPrompt<string>("Enter the title of the blog");
            string content = DisplayPrompt<string>("Enter the Content of the blog");
            await postManager.CreatePostAsync(title, content);
        }

        private async Task Retitle()
        {
            try
            {
                var postManager = CreatePostManager();
                int postId = DisplayPrompt<int>("Enter the id of the blog", postManager.MaxSize);
                string title = DisplayPrompt<string>("Enter the new title of the blog");

                await postManager.ChangePostTitleAsync(postId, title);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private async Task Edit()
        {
            try
            {
                var postManager = CreatePostManager();
                int postId = DisplayPrompt<int>("Enter the id of the blog", postManager.MaxSize);
                string Content = DisplayPrompt<string>("Enter the new content of the blog");

                await postManager.ChangePostContentAsync(postId, Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task Delete()
        {
            try
            {
                var postManager = CreatePostManager();
                int postId = DisplayPrompt<int>("Enter the id of the blog", postManager.MaxSize);
                await postManager.DeletePostAsync(postId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task ShowPost()
        {
            try
            {
                var postManager = CreatePostManager();
                int postId = DisplayPrompt<int>("Enter the id of the blog", postManager.MaxSize);
                await postManager.ShowBlog(postId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static async Task ShowAllPosts()
        {
            try
            {
                await CreatePostManager().ShowAllPostsSmallAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static async Task ShowAllPostsDetail()
        {
            try
            {
                await CreatePostManager().ShowAllPostsAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}



