using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Services;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IEnumerable<Student> GetAllStudents()
        {
            return _studentService.GetAllStudents();
        }

        [HttpGet("{ci}")]
        public Student GetStudentById(int ci)
        {
            return _studentService.GetStudentByCI(ci);
        }

        [HttpPost]
        public Student CreateStudent([FromBody] Student student)
        {
            return _studentService.Create(student);
        }

        [HttpPut]
        public Student UpdateStudent([FromBody] Student student)
        {
            return _studentService.Update(student);
        }

        [HttpDelete("{ci}")]
        public bool DeleteStudent(int ci)
        {
            return _studentService.Delete(ci);
        }

        [HttpGet("approved/ci/{ci}")]
        public bool HasStudentApprovedByCI(int ci)
        {
            return _studentService.HasApproved(ci);
        }

        [HttpGet("approved/nombre/{nombre}")]
        public bool HasStudentApprovedByName(string nombre)
        {
            return _studentService.HasApproved(null, nombre);
        }

        [HttpPost("approved")]
        public bool HasStudentApproved([FromBody] Student student)
        {
            return _studentService.HasApproved(student: student);
        }
    }
}
