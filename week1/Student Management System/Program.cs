using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public readonly int RollNumber;
    public string Grade { get; set; }

    public Student(int rollNumber)
    {
        RollNumber = rollNumber;
    }
}

class StudentList<T>
{
    public List<Student> Students;

    public StudentList(List<Student> students)
    {
        Students = students;
    }

    public void Filter(int optionalRollNumber = 0, string optionalName = null)
    {
        var filteredStudents = Students.FindAll(stu => stu.RollNumber == optionalRollNumber || stu.Name == optionalName);
        PrintQueryResults(filteredStudents);
    }

    public void PrintQueryResults(List<Student> students)
    {
        foreach (var student in students)
        {
            Console.WriteLine(student.RollNumber + ": " + student.Name);
        }
    }

    public void SaveToJson(string filePath)
    {
        var json = JsonConvert.SerializeObject(this);
        File.WriteAllText(filePath, json);
    }

    public static StudentList<T> LoadFromJson(string filePath)
    {
        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<StudentList<T>>(json);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var students = new List<Student>
        {
            new Student(1) { Name = "Alice", Age = 20, Grade = "A" },
            new Student(2) { Name = "Bob", Age = 21, Grade = "B" },
            new Student(3) { Name = "Charlie", Age = 19, Grade = "C" }
        };

        var studentList = new StudentList<Student>(students);
        studentList.Filter(optionalRollNumber: 3);

        studentList.SaveToJson("studentList.json");

        var loadedStudentList = StudentList<Student>.LoadFromJson("studentList.json");

        Console.WriteLine("Deserialized objects are:");
        foreach (var student in loadedStudentList.Students)
        {
            Console.WriteLine($"Roll Number: {student.RollNumber}");
            Console.WriteLine($"Name: {student.Name}");
            Console.WriteLine($"Age: {student.Age}");
            Console.WriteLine($"Grade: {student.Grade}");
            Console.WriteLine();
        }
    }
}

