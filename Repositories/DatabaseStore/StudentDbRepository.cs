using Interfaces;
using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Repositories.DatabaseStore
{
    public class StudentDbRepository : IStudentRepository
    {
        private readonly IDbContext _db;

        public StudentDbRepository()
        {
            _db = new DbContext();
        }

        public List<Student> GetAll()
        {
            List<Student> students = [];
            using var conn = _db.Create();
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM students", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                students.Add(new Student
                {
                    StudentCode = reader.GetString("student_code"),
                    FullName = reader.GetString("full_name"),
                    BirthYear = reader.GetInt32("birth_year"),
                    Major = reader.GetString("major"),
                });
            }
            return students;

        }


        public void Add(Student student)
        {
            if (student == null) return;
            using var conn = _db.Create();
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO students (student_code, full_name, birth_year, major)" +
                "VALUES (@SC, @FN, @BY, @Major)", conn);

            cmd.Parameters.AddWithValue("@SC", student.StudentCode);
            cmd.Parameters.AddWithValue("@FN", student.FullName);
            cmd.Parameters.AddWithValue("@BY", student.BirthYear);
            cmd.Parameters.AddWithValue("@Major", student.Major);
            cmd.ExecuteNonQuery();

        }

        public void Delete(Student student)
        {
            this.Delete(student.StudentCode);
        }

        public void Delete(string studentCode)
        {
            using var conn = _db.Create();
            conn.Open();
            var cmd = new MySqlCommand("DELETE FROM students WHERE student_code = @SC", conn);
            cmd.Parameters.AddWithValue("@SC", studentCode);
            cmd.ExecuteNonQuery();
        }

        public bool Exists(string studentCode)
        {
            using var conn = _db.Create();
            conn.Open();

            var cmd = new MySqlCommand("SELECT COUNT(*) FROM students WHERE student_code = @SC", conn);
            cmd.Parameters.AddWithValue("@SC", studentCode);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }



        public int? GetIndexByCode(string studentCode)
        {
            return null;
            //throw new NotImplementedException();
        }

        public Student? GetStudentByCode(string studentCode)
        {
            using var conn = _db.Create();
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM students WHERE student_code = @SC LIMIT 1", conn);
            cmd.Parameters.AddWithValue("@SC", studentCode);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? new Student
            {
                StudentCode = reader.GetString("student_code"),
                FullName = reader.GetString("full_name"),
                BirthYear = reader.GetInt32("birth_year"),
                Major = reader.GetString("major"),
            } : null;
        }

        public void Update(string studentCode, Student infoNewStudent)
        {
            using var conn = _db.Create();
            conn.Open();
            var cmd = new MySqlCommand("UPDATE students SET full_name = @FN, birth_year = @BY, major = @Major WHERE student_code = @SC", conn);

            cmd.Parameters.AddWithValue("@FN", infoNewStudent.FullName);
            cmd.Parameters.AddWithValue("@BY", infoNewStudent.BirthYear);
            cmd.Parameters.AddWithValue("@Major", infoNewStudent.Major);
            cmd.Parameters.AddWithValue("@SC", studentCode);
            cmd.ExecuteNonQuery();
        }
    }
}
