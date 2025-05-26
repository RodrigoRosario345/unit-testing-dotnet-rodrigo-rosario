using StudentManagementAPI.Models;
using StudentManagementAPI.Services;

namespace StudentManagementAPI.Tests.Stubs
{
    public class StudentServiceStub : IStudentService
    {
        private List<Student> _students;

        public StudentServiceStub()
        {
            _students = new List<Student>
            {
                new Student { CI = 7789322, Nombre = "Rodrigo Rosario", Nota = 85 },
                new Student { CI = 7776522, Nombre = "Alex Montellano", Nota = 92 },
                new Student { CI = 5489322, Nombre = "Sebastian Carballo", Nota = 76 },
                new Student { CI = 7939322, Nombre = "Joaquin Perez", Nota = 45 }
            };
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

        public Student GetStudentByCI(int ci)
        {
            return _students.FirstOrDefault(s => s.CI == ci);
        }

        public Student Create(Student student)
        {
            _students.Add(student);
            return student;
        }

        public Student Update(Student student)
        {
            return _students[0];
        }

        public bool Delete(int ci)
        {
            return true;
        }

        public bool HasApproved(int? ci = null, string? nombre = null, Student? student = null)
        {
            Student? target = student ??
                             _students.FirstOrDefault(s =>
                                 (ci.HasValue && s.CI == ci) ||
                                 (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(s.Nombre) && s.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)));

            return target != null && target.Nota >= 51;
        }

        public Student GetStudentBySelector<T>(T value, Func<Student, T, bool> selector, string parameterName = "CI")
        {
            return _students.FirstOrDefault(s => selector(s, value));
        }
    }
}
