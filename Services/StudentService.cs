using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Services
{
    public class StudentService(IStudentRepository _repo) : IStudentService
    {

        public void AddStudent(Student student)
        {

            ValidateStudentCode(student.StudentCode);
            if (_repo.Exists(student.StudentCode)) throw new Exception("Student ID already exists in the system!");
            ValidateAge(student.BirthYear);
            _repo.Add(student);
        }

        public void DeleteStudent(string studentCode)
        {
            Student student = ChecExistsStudentCode(studentCode)!;

            _repo.Delete(student);
        }

        public Student? GetStudentByStudentCode(string studentCode) => ChecExistsStudentCode(studentCode);

        public List<Student>? FindStudentsByName(string name) => ShowStudents().FindAll(student => student.FullName.ToLower().Contains(name.ToLower()));

        public List<Student> ShowStudents() => _repo.GetAll();

        public void UpdateStudentByCode(string studentCode, Student infoNewstudent)
        {
            Student? student = ChecExistsStudentCode(studentCode)!;
            ValidateAge(student.BirthYear);

            _repo.Update(studentCode, infoNewstudent);
        }

        public void ValidateAge(int birthYear)
        {
            int age = DateTime.Now.Year - birthYear;
            if (age < 18 || age > 30)
            {
                throw new Exception("Age must be between 18 and 30 years old!");

            }
        }

        public void ValidateStudentCode(string studentCode)
        {
            if (!Regex.IsMatch(studentCode, @"^SE\d{6}$", RegexOptions.IgnoreCase))

            {
                throw new Exception("Student ID must be in the form SExxxxxx!");
            }
        }

        public Student? ChecExistsStudentCode(string studentCode)
        {
            Student? student = _repo.GetStudentByCode(studentCode);
            if (student == null) throw new Exception("Student ID does not exist in the system!");
            return student;
        }
    }
}
