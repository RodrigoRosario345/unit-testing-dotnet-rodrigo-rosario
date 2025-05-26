using Microsoft.AspNetCore.Mvc;
using Xunit;
using StudentManagementAPI.Controllers;
using StudentManagementAPI.Models;
using StudentManagementAPI.Tests.Stubs;

namespace StudentManagementAPI.UnitTests.Controllers
{
    public class StudentControllerTestsUseStub
    {
        private readonly StudentServiceStub _studentServiceStub;
        private readonly StudentController _controller;

        public StudentControllerTestsUseStub()
        {
            _studentServiceStub = new StudentServiceStub();
            _controller = new StudentController(_studentServiceStub);
        }

        //Method that proves the existence of a student's CI, nombre and nota.
        //thus fulfilling the case of the third or fourth requirement of the work
        [Fact]
        public void GetStudentByCI_ReturnsOkResult_WhenStudentExists()
        {
            // Arrange
            var studentCI = 7789322;

            // Act
            var result = _controller.GetStudentByCI(studentCI);

            // Assert
            var returnedStudent = Assert.IsType<Student>(result);
            Assert.Equal(studentCI, returnedStudent.CI);
            Assert.Equal("Rodrigo Rosario", returnedStudent.Nombre);
            Assert.Equal(85, returnedStudent.Nota);
        }

        //Method that proves the existence of a student's CI, nombre and nota.
        //thus fulfilling the case of the third or fourth requirement of the work
        [Fact]
        public void CreateStudent_ReturnsCreatedResult_WhenStudentIsValid()
        {
            // Arrange
            var newStudent = new Student { CI = 19343102, Nombre = "Carlos Sanchez", Nota = 93 };

            // Act
            var result = _controller.CreateStudent(newStudent);

            // Assert
            var returnedStudent = Assert.IsType<Student>(result);
            Assert.Equal(newStudent.CI, returnedStudent.CI);
            Assert.Equal(newStudent.Nombre, returnedStudent.Nombre);
            Assert.Equal(newStudent.Nota, returnedStudent.Nota);
        }

        // Method that proves the approval of a student based on their nota >= 51 using student object and service stub
        [Fact]
        public void HasApproved_ReturnsTrue_WhenStudentHasApproved()
        {
            // Arrange
            var student = new Student { CI = 1043882, Nombre = "Ana Manzilla", Nota = 70 };
            _studentServiceStub.Create(student);

            // Act
            var result = _controller.HasStudentApproved(student);

            // Assert
            Assert.True(result);
            Assert.InRange(student.Nota, 0, 100);
            Assert.True(student.Nota >= 51);
        }

        // Method that proves the disapproval of a student based on their nota < 51 using student object and service stub
        [Fact]
        public void HasApproved_ReturnsFalse_WhenStudentHasNotApproved()
        {
            // Arrange
            var student = new Student { CI = 7743882, Nombre = "Rodrigo Carrasco", Nota = 39 };
            _studentServiceStub.Create(student);

            // Act
            var result = _controller.HasStudentApproved(student);

            // Assert
            Assert.False(result);
            Assert.InRange(student.Nota, 0, 100);
            Assert.False(student.Nota >= 51);
        }


    }
}