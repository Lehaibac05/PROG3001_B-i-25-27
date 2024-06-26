using System;
using System.Collections.Generic;

namespace Quan_ly_hoc_phan
{
    abstract class Person
    {
        public string name { get; set; }
        public int age { get; set; }
        public string gender { get; set; }

        public Person(string name, int age, string gender)
        {
            this.name = name;
            this.age = age;
            this.gender = gender;
        }

        public abstract string getRole();
    }

    interface KPIEvaluator
    {
        double calculateKPI();
    }

    class TeachingAssistant : Person, KPIEvaluator
    {
        public string employeeID { get; set; }
        private int numberOfCourses;

        public TeachingAssistant(string name, int age, string gender, string employeeID, int numberOfCourses)
            : base(name, age, gender)
        {
            this.employeeID = employeeID;
            this.numberOfCourses = numberOfCourses;
        }

        public override string getRole()
        {
            return "Teaching Assistant";
        }

        public double calculateKPI()
        {
            return numberOfCourses * 5.0;
        }
    }

    class Lecturer : Person, KPIEvaluator
    {
        public string employeeID { get; set; }
        private int numberOfPublications;

        public Lecturer(string name, int age, string gender, string employeeID, int numberOfPublications)
            : base(name, age, gender)
        {
            this.employeeID = employeeID;
            this.numberOfPublications = numberOfPublications;
        }

        public override string getRole()
        {
            return "Lecturer";
        }

        public double calculateKPI()
        {
            return numberOfPublications * 7.0;
        }
    }

    sealed class Professor : Lecturer, IDisposable
    {
        public static int countProfessors = 0;
        private int numberOfProjects;

        public Professor(string name, int age, string gender, string employeeID, int numberOfPublications, int numberOfProjects)
            : base(name, age, gender, employeeID, numberOfPublications)
        {
            this.numberOfProjects = numberOfProjects;
            countProfessors++;
        }

        public override string getRole()
        {
            return "Professor";
        }

        public override double calculateKPI()
        {
            return base.calculateKPI() + numberOfProjects * 10.0;
        }

        public void Dispose()
        {
            countProfessors--;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();

            Console.WriteLine("Nhập số lượng đối tượng (2-10): ");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n) || n < 2 || n > 10)
            {
                Console.WriteLine("Giá trị không hợp lệ, xin mời nhập lại");
            }

            for (int i = 0; i < n; i++)
            {
                persons.Add(NhapDoiTuong());
            }

            HienThiMangDoiTuong(persons);
            Console.WriteLine($"Tổng số giáo sư: {Professor.countProfessors}");
        }

        static Person NhapDoiTuong()
        {
            string type;
            while (true)
            {
                Console.WriteLine("Nhập loại đối tượng (ta, lec, gs): ");
                type = Console.ReadLine().ToLower();

                if (type == "ta" || type == "lec" || type == "gs")
                    break;
                else
                    Console.WriteLine("Loại đối tượng không hợp lệ.");
            }

            Console.WriteLine("Nhập tên: ");
            string name = Console.ReadLine();
            Console.WriteLine("Nhập tuổi: ");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Nhập giới tính: ");
            string gender = Console.ReadLine();
            Console.WriteLine("Nhập mã nhân viên:");
            string employeeID = Console.ReadLine();

            if (type == "ta")
            {
                Console.WriteLine("Nhập số lượng khóa học hỗ trợ: ");
                int numberOfCourses = int.Parse(Console.ReadLine());
                return new TeachingAssistant(name, age, gender, employeeID, numberOfCourses);
            }
            else if (type == "lec")
            {
                Console.WriteLine("Nhập số lượng bài báo đã xuất bản: ");
                int numberOfPublications = int.Parse(Console.ReadLine());
                return new Lecturer(name, age, gender, employeeID, numberOfPublications);
            }
            else
            {
                Console.WriteLine("Nhập số lượng bài báo đã xuất bản: ");
                int numberOfPublications = int.Parse(Console.ReadLine());
                Console.WriteLine("Nhập số lượng dự án đã chủ trì: ");
                int numberOfProjects = int.Parse(Console.ReadLine());
                return new Professor(name, age, gender, employeeID, numberOfPublications, numberOfProjects);
            }
        }

        static void HienThiMangDoiTuong(List<Person> persons)
        {
            foreach (var person in persons)
            {
                KPIEvaluator kpiEvaluator = (KPIEvaluator)person;
                Console.WriteLine($"Tên: {person.name}, Tuổi: {person.age}, Giới tính: {person.gender}");
                Console.WriteLine($"Vai trò: {person.getRole()}, KPI: {kpiEvaluator.calculateKPI()}");
                Console.WriteLine();
            }
        }
    }
}
