using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IStudentRepository
    {
        public List<Student> GetAll();
        public Student? GetStudentByCode(string studentCode);
        public int? GetIndexByCode(string studentCode);
        public void Add(Student student);
        public void Delete(Student student);
        public void Update(string studentCode, Student infoNewStudent);
        public void Delete(string studentCode);
        public bool Exists(string studentCode);
    }
}
