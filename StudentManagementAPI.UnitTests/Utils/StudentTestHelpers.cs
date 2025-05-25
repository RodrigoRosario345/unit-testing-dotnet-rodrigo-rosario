using StudentManagementAPI.Models;

namespace StudentManagementAPI.UnitTests.Utils
{
    public static class StudentTestHelpers
    {
        public static List<Student> CreateTestStudents()
        {
            return new List<Student>
            {

                new Student { CI = 7789322, Nombre = "Rodrigo Rosario", Nota = 85 },
                new Student { CI = 7776522, Nombre = "Alex Montellano", Nota = 92 },
                new Student { CI = 5489322, Nombre = "Sebastian Carballo", Nota = 76 },
                new Student { CI = 7939322, Nombre = "Joaquin Perez", Nota = 45 }

            };
        }

        public static Student CreateSingleStudent()
        {
            return new Student { CI = 1023456, Nombre = "Jhon Cena", Nota = 80 };
        }
    }
}