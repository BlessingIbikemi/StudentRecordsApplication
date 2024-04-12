using Microsoft.EntityFrameworkCore;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DatabaseConnection context;
        public StudentRepository(DatabaseConnection context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteStudent(int Id)
        {
            try
            {
                var student = await context.Student.FindAsync(Id);
                if (student is null)
                {
                    return false;
                }

                context.Student.Remove(student);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting student.", ex);
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            try
            {
                var students = await context.Student.ToListAsync();
                return students;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting students.", ex);
            }
        }

        public async Task<Student> GetStudentById(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    throw new ArgumentException("Student Name Is required");
                }

                Student student = await context.Student.FindAsync(Id);
                return student;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching student.", ex);
            }
        }

        public async Task<IEnumerable<Student>> GetStudentByName(string Name)
        {
            try
            {
                if (string.IsNullOrEmpty(Name))
                {
                    throw new ArgumentException("Student Name Is required");
                }

                var students = await context.Student
        .Where(s => s.Name.Contains(Name)).ToListAsync();
                return students;
            }
            catch (Exception ex)
            {
                throw new Exception("An Error occured while searching for students", ex);
            }
        }

        public async Task<Student> LogStudent(Student student)
        {
            try
            {
                if (student == null)
                {
                    throw new ArgumentNullException("Invalid Request, Student Data is required");
                }

                if (string.IsNullOrEmpty(student.Name))
                {
                    throw new ArgumentException("Student Name is required.");
                }

                if (string.IsNullOrEmpty(student.MobileNumber))
                {
                    throw new ArgumentException("Student Mobile Number is required.");
                }
                if (string.IsNullOrEmpty(student.Gender))
                {
                    throw new ArgumentException("Student Gender is required");
                }

                if (string.IsNullOrEmpty(student.Email))
                {
                    throw new ArgumentException("Student Email is required.");
                }

                student.DateAdded = DateTime.Now;

                context.Student.Add(student);
                await context.SaveChangesAsync();
                return student;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while logging student.", ex);
            }
        }
    }
}
