using StudentManagementAPI.Models;

namespace StudentManagementAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly List<Student> _students;

        public StudentService()
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
            var student = _students.FirstOrDefault(s => s.CI == ci);
            if (student == null)
                throw new KeyNotFoundException($"Student with CI {ci} not found");

            return student;
        }

        public Student Create(Student student)
        {
            if (_students.Any(s => s.CI == student.CI))
                throw new InvalidOperationException($"Student with CI {student.CI} already exists");

            _students.Add(student);
            return student;
        }

        public Student Update(Student student)
        {
            var existingStudent = GetStudentByCI(student.CI);
            existingStudent.Nombre = student.Nombre;
            existingStudent.Nota = student.Nota;

            return existingStudent;
        }

        public bool Delete(int ci)
        {
            var student = GetStudentByCI(ci);
            _students.Remove(student);
            return true;
        }

        // This method checks if a student has approved based on their Nota using pameters ci, Nombre or student object
        public bool HasApproved(int? ci = null, string? nombre = null, Student? student = null)
        {
            Student? targetStudent = null;

            if (student != null)
            {
                targetStudent = student;
            }
            else if (ci.HasValue)
            {
                targetStudent = GetStudentBySelector(ci.Value, (s, ciVal) => s.CI == ciVal, nameof(ci));
            }
            else if (!string.IsNullOrEmpty(nombre))
            {
                targetStudent = GetStudentBySelector(nombre,
                    (s, nombreVal) => s.Nombre.Equals(nombreVal, StringComparison.OrdinalIgnoreCase),
                    nameof(nombre)
                );
            }
            else
            {
                throw new ArgumentException("At least one parameter must be provided");
            }

            return targetStudent!.Nota > 51;
        }

        // this method finds a student based on a selector function.
        // T: Type of the value to compare
        // value: Value to search for
        // selector: Function to compare student with the value
        // parameterName: Name of the parameter for error messaging
        // returns: The found student
        // throws KeyNotFoundException when no student matches the criteria
        public Student GetStudentBySelector<T>(T value, Func<Student, T, bool> selector, string parameterName = "CI")
        {
            var student = _students.FirstOrDefault(s => selector(s, value));
            if (student == null)
                throw new KeyNotFoundException($"Student with {parameterName} '{value}' not found");

            return student;
        }

    }
}