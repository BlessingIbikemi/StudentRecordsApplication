using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services.Repository;

namespace Student_Records_API.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        IStudentRepository studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;    
        }

        [HttpPost]
        [Route("add/student")]
        public async Task<IActionResult> AddStudent(Student student)
        {
            var loggedStudents = await studentRepository.LogStudent(student);
            return Ok(loggedStudents);
        }

        [HttpGet]
        [Route("get/all/students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentRepository.GetAllStudents();
            if(students == null)
            {
                return NotFound("No students has been added");
            }

            return Ok(students);
        }

        [HttpGet]
        [Route("get/student/{Id}")]
        public async Task<IActionResult> GetStudentbyId(int Id)
        {
            var student = await studentRepository.GetStudentById(Id);
            if (student == null)
            {
                return NotFound("No students was found with this Id");
            }

            return Ok(student);
        }

        [HttpGet]
        [Route("get/students/{Name}")]
        public async Task<IActionResult> GetStudentbyName(string Name)
        {
            var student = await studentRepository.GetStudentByName(Name);
            if (student == null)
            {
                return NotFound("No students was found with this Id");
            }

            return Ok(student);
        }

       
        [HttpDelete]
        [Route("delete/student/{Id}")]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            var deletedStudent = await studentRepository.DeleteStudent(Id);
            if (deletedStudent)
            {
                return Ok("Student deleted successfully.");
            }
            else
            {
                return NotFound("Student not found.");
            }
        }

    }
}
