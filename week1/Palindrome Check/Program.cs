using System;
using System.Text.RegularExpressions;

class Program {
    static void Main() {
        if (Check()) {
            Console.WriteLine("The entered string is a palindrome.");
        } else {
            Console.WriteLine("The entered string is not a palindrome.");
        }
    }

    static bool Check() {
        Console.WriteLine("Enter a string: ");
        string res = Console.ReadLine();
        res = Regex.Replace(res, @"[^a-zA-Z0-9]", "");
        int i = 0;
        int j = res.Length - 1;
        while (i < j) {
            if (res[i] != res[j]) {
                return false;
            }
            i += 1;
            j -= 1;
        }
        return true;
    }
}