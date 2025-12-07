using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IStoreData
    {
        public void WriteDataInFile(List<Student> students);
        public List<Student> ReadDataToFile();
    }
}
