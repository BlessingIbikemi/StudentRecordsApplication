using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services.Repository;

namespace Student_Records_Web_App.Controllers
{
    public class StudentController : Controller
    {
        IStudentRepository studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }


        public IActionResult AddStudents()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudents(Student student)
        {
            student.DateAdded = DateTime.Now;
            var result = studentRepository.LogStudent(student).Result;

            if (result != null)
            {
                ViewBag.ResponseCode = "00";
            }
            else
            {
                ViewBag.ResponseCode = "01";
            }
            return View();
        }

        public IActionResult ViewAllStudents()
        {
            var students = studentRepository.GetAllStudents().Result;
            return View(students);
        }

        [HttpPost]
        public IActionResult ViewAllStudents(string Name)
        {
            var students = studentRepository.GetStudentByName(Name).Result;
            return View(students);
        }

        public IActionResult DeleteStudent(int Id)
        {
            var result = studentRepository.DeleteStudent(Id).Result;

            if (result == true)
            {
                ViewBag.ResponseCode = "00";
            }
            else
            {
                ViewBag.ResponseCode = "01";
            }

            return RedirectToAction("ViewAllStudents");
        }


    }
}
