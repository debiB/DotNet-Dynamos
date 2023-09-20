using System.Text;
using BlogConsoleApp.Controller;
namespace BlogConsoleApp.Driver
{
	public partial class Driver
	{
        private async Task MakeComment()
        {
            try
            {
                var commentManager = CreateCommentManager();
                int postId = DisplayPrompt<int>("Enter the id of the blog to comment on");
                string content = DisplayPrompt<string>("Enter the comment");
                await commentManager.CreateCommentAsync(postId, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task EditComment()
        {
            try
            {
                var commentManager = CreateCommentManager();
                int commentId = DisplayPrompt<int>("Enter the id of the comment", commentManager.MaxSize);
                string content = DisplayPrompt<string>("Enter the new title of the blog");

                await commentManager.ChangeCommentTextAsync(commentId, content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private async Task RemoveComment()
        {
            try
            {
                var commentManager = CreateCommentManager();
                int commentId = DisplayPrompt<int>("Enter the id of the comment", commentManager.MaxSize);
                await commentManager.DeleteCommentAsync(commentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

