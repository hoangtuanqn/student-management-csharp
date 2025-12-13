using Helpers;
using Interfaces;
using Models;
using Repositories.DatabaseStore;
using Services;
namespace student_management
{
    internal class Program
    {
        public static void Menu()
        {
            string[] messages =
            {

                "1. Add student",
                "2. Update student",
                "3. Delete student",
                "4. View all students",
                "---------------------------------",
                "5. Search student",
                "6. Sort students",
                "---------------------------------",
                "7. Exit program"
            };
            Console.WriteLine("--- STUDENT MANAGEMENT SYSTEM ---");
            foreach (string message in messages)
            {
                Console.WriteLine(message);
            }
        }

        public static void SubMenuSort()
        {
            string[] messages =
            {
                "1. Sort ASC by Birth Year",
                "2. Sort DESC by Birth Year",
                "3. Return to main Menu"
            };
            Console.WriteLine("--- SUB MENU SORT STUDENT ---");
            foreach (string message in messages)
            {
                Console.WriteLine(message);
            }
        }
        static void Main(string[] args)
        {
            //IStoreData storeData = new StudentFileRepository();
            //IStudentRepository repo = new StudentRepository(storeData.ReadDataToFile());
            IStudentRepository repo = new StudentDbRepository();
            IStudentService service = new StudentService(repo);
            bool isContinue = true;
            while (isContinue)
            {
                Menu();
                string choice = InputHelper.ReadString("Enter your choice:");
                switch (choice)
                {
                    case "1":
                        AddStudent(service);
                        //storeData.WriteDataInFile(service.ShowStudents());
                        break;
                    case "2":
                        UpdateStudent(service);
                        //storeData.WriteDataInFile(service.ShowStudents());
                        break;
                    case "3":
                        DeleteStudent(service);
                        //storeData.WriteDataInFile(service.ShowStudents());
                        break;
                    case "4":
                        ShowAllStudents(service);
                        break;
                    case "5":
                        SearchStudents(service);
                        break;
                    case "6":
                        HandleSubMenuSort(service);
                        break;
                    case "7":
                        Console.WriteLine("Exit program successfully!");
                        return;
                    default:
                        Console.WriteLine("Choice incorrect!");
                        break;
                }

            }
        }

        static void AddStudent(IStudentService service)
        {
            string fullName = InputHelper.ReadString("Enter Full Name: ");

            string studentCode = InputHelper.ReadStudentCode("Enter Student Code [SExxxxxx]: ");

            int birthYear = InputHelper.ReadBirthYear("Enter Birth Year: ");

            string major = InputHelper.ReadString("Enter Major: ");

            service.AddStudent(new Student()
            {
                StudentCode = studentCode,
                FullName = fullName,
                BirthYear = birthYear,
                Major = major,
            });
            Console.WriteLine("Add student successfully!");
        }
        static void ShowAllStudents(IStudentService service)
        {
            List<Student> students = service.ShowStudents();
            if (!students.Any())
            {
                Console.WriteLine("No students to show!");
                return;
            }
            int totalStudents = students.Count;
            PrintStudents(students);
            Console.WriteLine($"Total {totalStudents} students.");

        }

        static void DeleteStudent(IStudentService service)
        {
            string studentCode = InputHelper.ReadStudentCode("Enter Student Code to Delete: ");
            try
            {
                service.DeleteStudent(studentCode);
                Console.WriteLine($"Delete [{studentCode}] successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void SearchStudents(IStudentService service)
        {
            string keyword = InputHelper.ReadString("Enter Full Name: ");

            List<Student>? students = service.FindStudentsByName(keyword);
            if (students == null || students.Count == 0)
            {
                Console.WriteLine("No students found!");
                return;
            }
            PrintStudents(students);
            Console.WriteLine($"Found {students.Count} matching students!");
        }

        static void UpdateStudent(IStudentService service)
        {
            string studentCode = InputHelper.ReadStudentCode("Enter Student Code to Update: ");
            try
            {
                Student student = service.GetStudentByStudentCode(studentCode)!;
                string fullName = InputHelper.ReadString("Enter Full Name: ", isRequire: false);

                if (fullName.Length == 0) fullName = student.FullName;
                Console.WriteLine($"Fullname: {fullName}");

                int birthYear = InputHelper.ReadBirthYear("Enter Birth Year: ", isRequire: false);
                if (birthYear == 0) birthYear = student.BirthYear;
                Console.WriteLine($"birthYear: {birthYear}");

                string major = InputHelper.ReadString("Enter Major: ", isRequire: false);
                if (major.Length == 0) major = student.Major;
                Console.WriteLine($"major: {major}");

                Student infoNewStudent = new Student()
                {
                    StudentCode = studentCode,
                    FullName = fullName,
                    BirthYear = birthYear,
                    Major = major,
                };
                service.UpdateStudentByCode(studentCode, infoNewStudent);
                Console.WriteLine("-------------Info Student Currently -------------");
                Console.WriteLine(infoNewStudent);
                Console.WriteLine($"Update student [{studentCode}] successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void HandleSubMenuSort(IStudentService service)
        {
            while (true)
            {
                SubMenuSort();
                string choice = InputHelper.ReadString("Enter your choice:");
                switch (choice)
                {
                    case "1":
                    case "2":
                        SortStudentByBirthYear(service, choice);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Choice incorrect!");
                        break;
                }
            }
        }

        static void SortStudentByBirthYear(IStudentService service, string choice)
        {
            List<Student> students = [];
            if (choice == "1")
                students = service.ShowStudents().OrderBy(s => s.BirthYear).ToList();
            else
                students = service.ShowStudents().OrderByDescending(s => s.BirthYear).ToList();
            PrintStudents(students);
        }

        private static void PrintStudents(List<Student> students)
        {
            int totalStudents = students.Count;
            for (int i = 0; i < totalStudents; i++)
            {
                Console.WriteLine($"{i + 1}. {students[i]}");
            }
        }

    }
}
