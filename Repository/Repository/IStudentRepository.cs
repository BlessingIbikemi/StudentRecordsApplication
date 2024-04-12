using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetStudentById(int Id);
        Task<IEnumerable<Student>> GetStudentByName(string Name);
        Task<Student> LogStudent(Student student);
        Task<bool> DeleteStudent(int Id);
    }
}
