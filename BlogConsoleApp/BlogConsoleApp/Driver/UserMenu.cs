using System;
using System.Text;

namespace BlogConsoleApp.Driver
{
	public partial class Driver
	{
        public void CheckInvalidInput<T>(string input, decimal? upperBound = null)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new Exception("Input cannot be empty.");
            }

            if (typeof(T) == typeof(decimal) || typeof(T) == typeof(Int32))
            {
                if (upperBound == null)
                {
                    throw new Exception("Upper bound cannot be null");
                }
                decimal output;
                if (decimal.TryParse(input, out output))
                {
                    if (!(output >= 0 && output <= upperBound))
                    {
                        throw new Exception($"The Input cannot be greater than {upperBound} and lower than 0.");
                    }

                }
                else
                {
                    throw new Exception("Input needs to be a number.");
                }
            }
        }

        // a method to continously prompt the user when there is an invalid input
        public T DisplayPrompt<T>(string prompt, decimal? upperBound = null)
        {
            Console.Write($"{prompt}: ");
            string variable = Console.ReadLine().Trim();
            try
            {
                CheckInvalidInput<T>(variable, upperBound);
            }
            catch (Exception error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error.Message);
                Console.ResetColor();
                return DisplayPrompt<T>(prompt, upperBound);
            }

            return (T)Convert.ChangeType(variable, typeof(T));
        }

        public async Task Menu()
        {

            StringBuilder prompt = new();
            prompt.AppendLine("Choose an action");
            prompt.AppendLine("\t 1. Make a new post");
            prompt.AppendLine("\t 2. Change blog title");
            prompt.AppendLine("\t 3. Edit a post");
            prompt.AppendLine("\t 4. Remove a post");
            prompt.AppendLine("\t 5. Comment on a post");
            prompt.AppendLine("\t 6. Edit a comment");
            prompt.AppendLine("\t 7. Delete a comment");
            prompt.AppendLine("\t 8. Look at a specific blog");
            prompt.AppendLine("\t 9. Look at all the blogs");
            prompt.AppendLine("\t 10. Look at all the blogs in detail");
            prompt.AppendLine("\t 0. Exit");


            int choice = DisplayPrompt<int>(prompt.ToString(), 10);
            await SwitchAction(choice);
        }
    }
}

