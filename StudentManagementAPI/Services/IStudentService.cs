using StudentManagementAPI.Models;

namespace StudentManagementAPI.Services
{
    public interface IStudentService
    {
        Student GetStudentByCI(int ci);
        IEnumerable<Student> GetAllStudents();
        Student Create(Student student);
        Student Update(Student student);
        bool Delete(int ci);
        bool HasApproved(int? ci = null, string? nombre = null, Student? student = null);
    }
}