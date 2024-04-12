using Microsoft.EntityFrameworkCore;
using Services;
using Services.Model;
using Services.Repository;

namespace StudentRepositoryTest
{
    public class RepositoryTest
    {
        [Fact]
        public async Task LogStudent_ValidStudent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseConnection>()
                .UseInMemoryDatabase(databaseName: "LogStudentTest")
                .Options;

            using (var context = new DatabaseConnection(options))
            {
                var student = new Student()
                {
                    ID = 1,
                    Name = "Test Student",
                    DOB = DateTime.Now.AddYears(-20), // Assuming DOB is 20 years ago
                    Gender = "Male",
                    MobileNumber = "123-456-7890",
                    Email = "test.student@example.com",
                    DateAdded = DateTime.Now
                };
                var repository = new StudentRepository(context);

                // Act
                var returnedStudent = await repository.LogStudent(student);

                // Assert
                Assert.NotNull(returnedStudent);
                Assert.Equal(student.Name, returnedStudent.Name);
                Assert.Equal(student.MobileNumber, returnedStudent.MobileNumber);
            }
        }

        [Fact]
        public async Task LogStudent_InvalidStudent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseConnection>()
                .UseInMemoryDatabase(databaseName: "LogStudentTest")
                .Options;

            using (var context = new DatabaseConnection(options))
            {
                var student = new Student()
                {
                    ID = 2,
                    Name = "Student",
                    DOB = DateTime.Now.AddYears(-20), // Assuming DOB is 20 years ago
                    Gender = "Male",
                    DateAdded = DateTime.Now
                };
                var repository = new StudentRepository(context);

                //ACT & Assert
                await Assert.ThrowsAsync<Exception>(async () => await repository.LogStudent(student));

            }
        }

        [Fact]
        public async Task DeleteStudent_ExistingStudent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseConnection>()
                .UseInMemoryDatabase(databaseName: "DeleteStudentTest")
                .Options;

            using (var context = new DatabaseConnection(options))
            {
                var student = new Student()
                {
                    ID = 1,
                    Name = "Test Student",
                    DOB = DateTime.Now.AddYears(-20),
                    Gender = "Female",
                    MobileNumber = "123-456-7890",
                    Email = "test.student@example.com",
                    DateAdded = DateTime.Now
                };

                var repository = new StudentRepository(context);
                var loggedStudent = await repository.LogStudent(student);

                // Act
                var result = await repository.DeleteStudent(1);

                // Assert
                Assert.True(result);
            }
        }


        [Fact]
        public async Task DeleteStudent_NonExistingStudent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseConnection>()
                .UseInMemoryDatabase(databaseName: "DeleteStudentTest")
                .Options;

            using (var context = new DatabaseConnection(options))
            {
                var repository = new StudentRepository(context);

                // Act
                var result = await repository.DeleteStudent(1);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public async Task GetAllStudent_ExistingStudent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseConnection>()
                .UseInMemoryDatabase(databaseName: "GetStudents")
                .Options;

            using (var context = new DatabaseConnection(options))
            {
                var student = new Student()
                {
                    ID = 1,
                    Name = "Test Student",
                    DOB = DateTime.Now.AddYears(-20), // Assuming DOB is 20 years ago
                    Gender = "Male",
                    MobileNumber = "123-456-7890",
                    Email = "test.student@example.com",
                    DateAdded = DateTime.Now
                };

                var repository = new StudentRepository(context);
                var loggedStudent = await repository.LogStudent(student);

                // Act
                var students = await repository.GetAllStudents();

                // Assert
                Assert.NotNull(students);
                Assert.NotEmpty(students);
            }
        }


        [Fact]
        public async Task GetStudentbyName_ExistingStudent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseConnection>()
                .UseInMemoryDatabase(databaseName: "GetStudents")
                .Options;

            using (var context = new DatabaseConnection(options))
            {

                var repository = new StudentRepository(context);

                // Act
                var students = await repository.GetStudentByName("Test Student");

                // Assert
                Assert.NotNull(students);
                Assert.NotEmpty(students);
            }
        }

        [Fact]
        public async Task GetStudentById_ExistingStudent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseConnection>()
                .UseInMemoryDatabase(databaseName: "GetStudents")
                .Options;

            using (var context = new DatabaseConnection(options))
            {
                var student = new Student()
                {
                    ID = 2,
                    Name = "Test Student",
                    DOB = DateTime.Now.AddYears(-20), 
                    Gender = "Male",
                    MobileNumber = "123-456-7890",
                    Email = "test@example.com",
                    DateAdded = DateTime.Now
                };

                var repository = new StudentRepository(context);
                var loggedStudent = await repository.LogStudent(student);

                // Act
                var returnedStudent = await repository.GetStudentById(2);

                // Assert
                Assert.NotNull(returnedStudent);
                Assert.Equal(student.ID, returnedStudent.ID);
                Assert.Equal(student.Name, returnedStudent.Name);
            }
        }

    }
}