using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using Course.Entities;

namespace Course {
    class Program {

        static void Main() {

            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            Console.Write("Enter salary: ");
            double salary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            
            List<Employee> list = new List<Employee>();

            try {
                using (StreamReader sr = File.OpenText(path)) {
                    while (!sr.EndOfStream) {
                        string[] vect = sr.ReadLine().Split(',');
                        string name = vect[0];
                        string email = vect[1];
                        double price = double.Parse(vect[2], CultureInfo.InvariantCulture);
                        list.Add(new Employee(name, email, price));
                    }
                }

                var emails = list
                    .Where(p => p.Salary > salary)
                    .OrderBy(p => p.Email)
                    .Select(p => p.Email);

                var sum = list
                    .Where(p => p.Name[0] == 'M')
                    .Select(p => p.Salary).DefaultIfEmpty(0.0).Sum();

                Console.WriteLine($"Email of people whose salary is more than {salary.ToString("F2", CultureInfo.InvariantCulture)}:");
                foreach (string email in emails) {
                    Console.WriteLine(email);
                }
                Console.WriteLine("Sum of salary of people whose name starts with 'M': " + sum.ToString("F2", CultureInfo.InvariantCulture));
            }
            catch(IOException e) {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}