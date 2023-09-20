using System;
namespace BlogConsoleApp.Driver
{
	public partial class Driver
	{
        private async Task SwitchAction(int choice)
        {
            switch (choice)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    await MakePost();
                    break;
                case 2:
                    await Retitle();
                    break;
                case 3:
                    await Edit();
                    break;
                case 4:
                    await Delete();
                    break;
                case 5:
                    await MakeComment();
                    break;
                case 6:
                    await EditComment();
                    break;
                case 7:
                    await RemoveComment();
                    break;
                case 8:
                    await ShowPost();
                    break;
                case 9:
                    await ShowAllPosts();
                    break;
                case 10:
                    await ShowAllPostsDetail();
                    break;
            }
        }
    }
}

