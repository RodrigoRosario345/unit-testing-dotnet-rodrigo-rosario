using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using StudentManagementAPI.Controllers;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services;
using StudentManagementAPI.UnitTests.Utils;

namespace StudentManagementAPI.UnitTests.Controllers
{
    public class StudentControllerTestsUseMock
    {
        private readonly Mock<IStudentService> _mockStudentService;
        private readonly StudentController _controller;

        public StudentControllerTestsUseMock()
        {
            _mockStudentService = new Mock<IStudentService>();
            _controller = new StudentController(_mockStudentService.Object);
        }

        [Fact]
        public void GetAllStudents_ReturnsOkResult_WithListOfStudents()
        {
            // Arrange - Given
            var students = StudentTestHelpers.CreateTestStudents();
            _mockStudentService.Setup(service => service.GetAllStudents())
                .Returns(students);

            // Act - When
            var result = _controller.GetAllStudents();

            // Assert - Then
            var returnedStudents = Assert.IsType<List<Student>>(result);
            Assert.NotNull(returnedStudents);
            Assert.Equal(4, returnedStudents.Count);
        }

        [Fact]
        public void GetAllStudents_ReturnsOkResult_WithEmptyList()
        {
            // Arrange - Given
            var emptyList = new List<Student>();
            _mockStudentService.Setup(service => service.GetAllStudents())
                .Returns(emptyList);

            // Act - When
            var result = _controller.GetAllStudents();

            // Assert - Then
            var returnedStudents = Assert.IsType<List<Student>>(result);
            Assert.NotNull(returnedStudents);
            Assert.Empty(returnedStudents);
        }

        //Method that proves the existence of a student's CI, nombre and nota.
        //thus fulfilling the case of the third or fourth requirement of the work
        [Fact]
        public void GetStudentById_ReturnsOkResult_WhenStudentExists()
        {
            // Arrange - Given
            var student = new Student { CI = 7898312, Nombre = "Rodrigo Rosario", Nota = 85 };
            int studentCI = 7898312;

            _mockStudentService.Setup(service => service.GetStudentByCI(studentCI))
                .Returns(student);

            // Act - When
            var result = _controller.GetStudentByCI(studentCI);

            // Assert - Then
            var returnedStudent = Assert.IsType<Student>(result);
            Assert.Equal(7898312, returnedStudent.CI);
            Assert.Equal("Rodrigo Rosario", returnedStudent.Nombre);
            Assert.Equal(85, returnedStudent.Nota);
        }

        [Fact]
        public void GetStudentById_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange - Given
            int studentCI = 7898312;

            _mockStudentService.Setup(service => service.GetStudentByCI(studentCI))
                .Throws(new KeyNotFoundException($"Student with CI {studentCI} not found"));

            // Act - When and Assert - Then
            var exception = Assert.Throws<KeyNotFoundException>(() =>
                _controller.GetStudentByCI(studentCI));

            Assert.Equal($"Student with CI {studentCI} not found", exception.Message);
        }

        //Method that proves the existence of a student's CI, nombre and nota.
        //thus fulfilling the case of the third or fourth requirement of the work
        [Fact]
        public void CreateStudent_ReturnsCreatedResult_WhenStudentIsValid()
        {
            // Arrange - Given
            var newStudent = new Student { CI = 1823873, Nombre = "Carlos Ramirez", Nota = 90 };

            _mockStudentService.Setup(service => service.Create(It.IsAny<Student>()))
                .Returns(newStudent);

            // Act - When
            var result = _controller.CreateStudent(newStudent);

            // Assert - Then
            var returnedStudent = Assert.IsType<Student>(result);
            Assert.Equal(newStudent.CI, returnedStudent.CI);
            Assert.Equal(newStudent.Nombre, returnedStudent.Nombre);
            Assert.Equal(newStudent.Nota, returnedStudent.Nota);
        }

        [Fact]
        public void CreateStudent_ReturnsBadRequest_WhenStudentAlreadyExists()
        {
            // Arrange - Given
            var duplicateStudent = new Student { CI = 7832351, Nombre = "Rodrigo Rosario", Nota = 80 };

            _mockStudentService.Setup(service => service.Create(It.IsAny<Student>()))
                .Throws(new InvalidOperationException($"Student with CI {duplicateStudent.CI} already exists"));

            // Act - When and Assert - Then
            var exception = Assert.Throws<InvalidOperationException>(() => _controller.CreateStudent(duplicateStudent));

            Assert.Contains($"Student with CI {duplicateStudent.CI} already exists", exception.Message);
        }


        //Method that proves the existence of a student's CI, nombre and nota.
        //thus fulfilling the case of the third or fourth requirement of the work
        [Fact]
        public void UpdateStudent_ReturnsOkResult_WhenStudentExists()
        {
            // Arrange - Given
            var updatedStudent = new Student { CI = 1074838, Nombre = "Jaime Contreras", Nota = 95 };

            _mockStudentService.Setup(service => service.Update(It.IsAny<Student>())).Returns(updatedStudent);

            // Act - When
            var result = _controller.UpdateStudent(updatedStudent);

            // Assert - Then
            var returnedStudent = Assert.IsType<Student>(result);
            Assert.Equal(updatedStudent.CI, returnedStudent.CI);
            Assert.Equal(updatedStudent.Nombre, returnedStudent.Nombre);
            Assert.Equal(updatedStudent.Nota, returnedStudent.Nota);
        }

        [Fact]
        public void UpdateStudent_ReturnsNotFound_WhenStudentDoesNotExist()
        {
            // Arrange - Given
            var nonExistentStudent = new Student { CI = 7892331, Nombre = "Edson Montesinos", Nota = 80 };

            _mockStudentService.Setup(service => service.Update(It.IsAny<Student>()))
                .Throws(new KeyNotFoundException($"Student with CI {nonExistentStudent.CI} not found"));

            // Act - When and Assert - Then
            var exception = Assert.Throws<KeyNotFoundException>(() => _controller.UpdateStudent(nonExistentStudent));

            Assert.Contains($"Student with CI {nonExistentStudent.CI} not found", exception.Message);
        }

        [Fact]
        public void DeleteStudent_ReturnsOkResult_WhenStudentExists()
        {
            // Arrange - Given
            int studentCI = 1184321;

            _mockStudentService.Setup(service => service.Delete(studentCI))
                .Returns(true);

            // Act - When
            var result = _controller.DeleteStudent(studentCI);

            // Assert - Then
            var returnedValue = Assert.IsType<bool>(result);
            Assert.True(returnedValue);
        }

        // Method that proves the approval of a student based on their nota >= 51 using CI
        [Fact]
        public void HasApprovedByCI_ReturnsTrue_WhenStudentHasApproved()
        {
            // Arrange - Given
            int studentCI = 1032673;

            _mockStudentService.Setup(service => service.HasApproved(studentCI, null, null))
                .Returns(true);

            // Act - When
            var result = _controller.HasStudentApprovedByCI(studentCI);

            // Assert - Then
            Assert.True(result);
        }

        // Method that proves the approval of a student based on their nota >= 51 using name
        [Fact]
        public void HasApprovedByName_ReturnsTrue_WhenStudentHasApproved()
        {
            // Arrange - Given
            string studentName = "Edson Montesinos";

            _mockStudentService.Setup(service => service.HasApproved(null, studentName, null))
                .Returns(true);

            // Act - When
            var result = _controller.HasStudentApprovedByName(studentName);

            // Assert - Then
            Assert.True(result);
        }

        // Method that proves the approval of a student based on their nota >= 51 using student object
        [Fact]
        public void HasApproved_ReturnsTrue_WhenStudentHasApproved()
        {
            // Arrange - Given
            var student = new Student { CI = 1234567, Nombre = "Carlos Ramirez", Nota = 90 };

            _mockStudentService.Setup(service => service.HasApproved(null, null, student))
                .Returns(true);

            // Act - When
            var result = _controller.HasStudentApproved(student);

            // Assert - Then
            Assert.True(result);
            Assert.InRange(student.Nota, 0, 100);
            Assert.True(student.Nota >= 51);
        }

        // Method that proves the disapproval of a student based on their nota < 51 using student object
        [Fact]
        public void HasApproved_ReturnsFalse_WhenStudentHasNotApproved()
        {
            // Arrange - Given
            var student = new Student { CI = 3277121, Nombre = "Luis Perez", Nota = 45 };

            _mockStudentService.Setup(service => service.HasApproved(null, null, student))
                .Returns(false);

            // Act - When
            var result = _controller.HasStudentApproved(student);

            // Assert - Then
            Assert.False(result);
            Assert.InRange(student.Nota, 0, 100);
            Assert.False(student.Nota >= 51);
        }

    }
}