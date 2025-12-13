using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Repositories.FileStore
{
    public class StudentFileRepository : IStoreData
    {
        public static string path = "students.json";
        public List<Student> ReadDataToFile()
        {
            if(!File.Exists(path))
            {
                return [];
            }
            string jsonString = File.ReadAllText(path);
            List<Student> students = JsonSerializer.Deserialize<List<Student>>(jsonString)!;
            return students;
        }

        public void WriteDataInFile(List<Student> students)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true, // xuống dòng cho dễ đọc
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) // hiển thị tiếng việt không bị lỗi font
            };
            string jsonString = JsonSerializer.Serialize(students, options);
            File.WriteAllText(path, jsonString);
        }
    }
}
