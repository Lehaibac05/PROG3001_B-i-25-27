using System.Text;

namespace Bài26_27
{
    internal class Program
    {

        abstract class Person
        {
            public string Name { get; set; }
            public int age { get; set; }

            public Person(string name, int age) {
                this.Name = name;
                this.age = age;
            }

            public abstract override string ToString();
            
             
        }

        class Student : Person, KPI
        {
            public string Major {  get; set; }
            public float gpa { get; set; }


            public Student(string name, int age, string major, float gpa) : base(name, age) {
                this.Major = major;
                this.gpa = gpa;
            }

            public override string ToString()
            {
            return $"Name: {Name}, Age: {age}, Major: {Major}. ";
            }

            public double CalculateKPI()
            {

            return gpa; 
            }
        }

            interface KPI
            {
            double CalculateKPI();
            }


        class Teacher : Person
        {
            public string Major { get; set; }
            public int Publications { get; set; }

            public Teacher(string name, int age,string major, int publications) : base(name, age) {
                this.Publications = publications;
                this.Major = major;
            }

            public override string ToString()
            {
                return $"Name: {Name}, Age: {age}, Major: {Major}, Publications: {Publications}";
            }

            public double CalculateKPI()
            {
                return  Publications * 1.5;
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8 ;
            Student student = new Student("Nguyễn Tiến Dũng", 20, "CNTT&TT", 3.8f);
            Teacher teacher = new Teacher("Vũ Đức Việt", 38, "CNTT&TT", 5);
            Console.WriteLine(student.ToString());
            Console.WriteLine($"KPI: {student.CalculateKPI()}");
            Console.WriteLine();
            Console.WriteLine(teacher.ToString());
            Console.WriteLine($"KPI: {teacher.CalculateKPI()}");
        }
    }
}
