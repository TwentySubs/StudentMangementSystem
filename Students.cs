using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace StudentManagementSystem
{
    public class Student : IComparable<Student>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public static List<Student> list = new List<Student>();

        public Student(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public override string ToString()
        {
            return String.Format("Name: {0}, Age: {1}", Name, Age);
        }

        public int CompareTo(Student other)
        {
            return this.Age.CompareTo(other.Age);
        }
    }

    internal class Students
    {
        static void PrintMenu()
        {
            Console.WriteLine("Enter a number from the menu:");
            Console.WriteLine("-1 - Enter two numbers and get a random between them");
            Console.WriteLine("0 - Current date and time");
            Console.WriteLine("1 - Insert number of students");
            Console.WriteLine("2 - Insert students");
            Console.WriteLine("3 - Sort students");
            Console.WriteLine("4 - Convert student names to Uppercase");
            Console.WriteLine("5 - Print sorted or unsorted students");
            Console.WriteLine("6 - Save students to a file");
            Console.WriteLine("7 - Read students from a file");
            Console.WriteLine("8 - Increase students' ages by 10 years");
            Console.WriteLine("9 - Exit the program");


        }

        static void Main(string[] args)
        {
            int numberOfStudents = 0;

            while (true)
            {
                PrintMenu();
                Console.WriteLine("Enter a number to choose an option from the menu:");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case -1:
                        GenerateRandomBetweenTwoNumbers();
                        break;

                    case 0:
                        Console.WriteLine(CurrentDateTime());
                        break;

                    case 1:
                        numberOfStudents = InsertNumberOfStudents();
                        break;

                    case 2:
                        InsertStudents(numberOfStudents);
                        break;

                    case 3:
                        Student.list.Sort();
                        break;

                    case 4:
                        ConvertNamesToUppercase();
                        break;

                    case 5:
                        PrintStudents();
                        break;

                    case 6:
                        SaveToJson();
                        break;

                    case 7:
                        ReadFromJson();
                        break;

                    case 8:
                        IncreaseAgesByTenYears();
                        break;

                    case 9:
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please select a number from the menu.");
                        break;
                }
            }
        }

        static int InsertNumberOfStudents()
        {
            Console.WriteLine("How many students will there be?");
            int n = int.Parse(Console.ReadLine());
            return n;
        }

        static void InsertStudents(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Enter name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter age:");
                int age = int.Parse(Console.ReadLine());
                Student currentStudent = new Student(name, age);
                Student.list.Add(currentStudent);
            }
        }

        static void PrintStudents()
        {
            for (int i = 0; i < Student.list.Count; i++)
            {
                Console.WriteLine(Student.list[i]);
            }
        }

        static void GenerateRandomBetweenTwoNumbers()
        {
            Console.WriteLine("Enter the first limit:");
            int a = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the second limit:");
            int b = int.Parse(Console.ReadLine());
            Random random = new Random();
            int n = random.Next(a, b);
            Console.WriteLine("I chose a number between {0} and {1}: {2}", a, b, n);
        }

        static DateTime CurrentDateTime()
        {
            return DateTime.Now;
        }

        static void ConvertNamesToUppercase()
        {
            for (int i = 0; i < Student.list.Count; i++)
            {
                Student.list[i].Name = Student.list[i].Name.ToUpper();
            }
        }

        static void IncreaseAgesByTenYears()
        {
            for (int i = 0; i < Student.list.Count; i++)
            {
                Student.list[i].Age += 10;
            }
        }

        static void SaveToJson()
        {
            try
            {
                string json = JsonConvert.SerializeObject(Student.list, Formatting.Indented);
                File.WriteAllText("students.json", json);
                Console.WriteLine("Successfully saved to file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while saving students to JSON: {ex.Message}");
            }
        }

        static void ReadFromJson()
        {
            try
            {
                string json = File.ReadAllText("students.json");
                Student.list = JsonConvert.DeserializeObject<List<Student>>(json);
                Console.WriteLine("Students loaded from JSON file.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("JSON file not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading students from JSON: {ex.Message}");
            }
        }
    }
}
