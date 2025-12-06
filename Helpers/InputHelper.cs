using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace Helpers
{
    public static class InputHelper
    {
        public static string ReadString(string label)
        {
            while (true)
            {
                Console.Write($"{label} ");
                string? value = Console.ReadLine();
                if (value != null && value.Length > 0)
                {
                    return value;
                }
                Console.WriteLine("Please do not leave blank!");
            }
        }

        public static string ReadString(string label, bool isRequire = true)
        {
            if (isRequire) return ReadString(label);
            Console.Write($"{label} ");
            string? value = Console.ReadLine();
            return value;
        }

        public static int ReadInt(string label)
        {
            while (true)
            {
                Console.Write($"{label} ");
                string? value = Console.ReadLine();
                if (int.TryParse(value, out int _value))
                {
                    return _value;
                }

                Console.WriteLine("Please enter an integer!");
            }
        }

        public static string ReadStudentCode(string label)
        {
            while (true)
            {
                string value = ReadString(label);
                if (Regex.IsMatch(value, @"^SE\d{6}$", RegexOptions.IgnoreCase))
                    return value.ToUpper();
                Console.WriteLine("Student ID must be in the form SExxxxxx!");
            }
        }

        public static int ReadBirthYear(string label)
        {
            while (true)
            {
                int birthYear = ReadInt(label);
                int age = DateTime.Now.Year - birthYear;
                if (age >= 18 && age <= 30)
                    return birthYear;

                Console.WriteLine("Age must be between 18 and 30 years old!");
            }
        }
        public static int ReadBirthYear(string label, bool isRequire = true)
        {
            if (isRequire) return ReadBirthYear(label);
            Console.Write($"{label} ");
            string? value = Console.ReadLine();
            if (int.TryParse(value, out int birthYear))
            {
                return birthYear;
            }
            return 0;

        }
    }
}
