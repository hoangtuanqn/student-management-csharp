using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.FileStore
{
    public class StudentRepository(List<Student> students) : IStudentRepository
    {
        private readonly List<Student> _students = students;
        public List<Student> GetAll() => _students;

        public Student? GetStudentByCode(string studentCode)
        {
            return _students.FirstOrDefault(s => s.StudentCode == studentCode);
        }

        public int? GetIndexByCode(string studentCode)
        {
            return _students.FindIndex(s => s.StudentCode == studentCode);
        }


        public void Add(Student student)
        {
            if (student != null)
            {
                _students.Add(student);
            }
        }

        public void Delete(Student student)
        {
            if (student != null) _students.Remove(student);
        }

        public void Update(string studentCode, Student infoNewStudent)
        {
            int? index = GetIndexByCode(studentCode);
            if (index != null)
            {
                _students[index.Value] = (Student)infoNewStudent.Clone();
            }
        }

        public void Delete(string studentCode)
        {
            Student? student = GetStudentByCode(studentCode);
            if (student != null)
            {
                _students.Remove(student);
            }
        }


        public bool Exists(string studentCode) => _students.Any(s => s.StudentCode == studentCode);
    }
}
