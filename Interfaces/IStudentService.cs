using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IStudentService
    {
        public List<Student> ShowStudents();
        public void AddStudent(Student infoNewStudent);
        public List<Student>? FindStudentsByName(string name);
        public Student? GetStudentByStudentCode(string studentCode);

        public void DeleteStudent(string studentCode);

        public void UpdateStudentByCode(string studentCode, Student infoNewStudent);

        public void ValidateAge(int birthYear);
        public void ValidateStudentCode(string studentCode);
        public Student? ChecExistsStudentCode(string studentCode);
    }
}
