using System;
using System.Collections.Generic;

class Program {
    static void Main() {
        Console.WriteLine("Enter a string: ");
        string inp = Console.ReadLine();
        Dictionary<string, int> dict = new Dictionary<string, int>();

        for (int i = 0; i < inp.Length; i++) {
            if (char.IsLetter(inp[i])){
            string upperCaseKey = inp[i].ToString().ToUpper();
            if (!dict.ContainsKey(upperCaseKey)) {
                dict.Add(upperCaseKey, 1);
            }else{
                 dict[upperCaseKey] += 1;
            }
            }
        }

        foreach (var kvp in dict) {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }
}
