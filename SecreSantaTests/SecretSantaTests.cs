using System;
using System.Collections.Generic;
using System.IO;
using SecretSanta.Implementations;
using SecretSanta.Interface;
using SecretSanta.Models;
using Xunit;

namespace SecreSantaTests
{
    public class SecretSantaTests
    {
        [Fact]
        public void ReadUsers_ValidFile_ReturnsUsers()
        {
            // Create temporary test CSV file
            var filePath = "test_users.csv";
            File.WriteAllLines(filePath, new[]
            {
            "id,name,email",
            "1,John Doe,john@example.com",
            "2,Jane Doe,jane@example.com",
            "3,Bob Smith,bob@example.com"
            });

            IUserReader userReader = new CsvUserReader();
            var users = userReader.ReadUsers(filePath);

            // Assert that 3 users were read
            Assert.Equal(3, users.Count);

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void ReadUsers_MissingFile_ThrowsFileNotFoundException()
        {
            IUserReader userReader = new CsvUserReader();
            Assert.Throws<FileNotFoundException>(() => userReader.ReadUsers("nonexistent.csv"));
        }

        [Fact]
        public void ReadUsers_EmptyFile_ThrowsInvalidDataException()
        {
            // Create an empty temporary test CSV file
            var filePath = "empty.csv";
            File.WriteAllText(filePath, "");

            IUserReader userReader = new CsvUserReader();
            Assert.Throws<InvalidDataException>(() => userReader.ReadUsers(filePath));

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void ReadUsers_MalformedFile_ThrowsInvalidDataException()
        {
            // Create a malformed test CSV file
            var filePath = "malformed.csv";
            File.WriteAllLines(filePath, new[]
            {
            "id,name,email",
            "1,John Doe",
            "2,Jane Doe,jane@example.com"
        });

            IUserReader userReader = new CsvUserReader();
            Assert.Throws<InvalidDataException>(() => userReader.ReadUsers(filePath));

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void GenerateAssignments_ValidUsers_ReturnsAssignments()
        {
            var users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" },
            new User { Id = 3, Name = "Bob Smith", Email = "bob@example.com" }
        };

            IAssignmentGenerator assignmentGenerator = new SecretSantaAssignmentGenerator();
            var assignments = assignmentGenerator.GenerateAssignments(users);

            // Assert that 3 assignments were generated
            Assert.Equal(3, assignments.Count);

            var givers = new HashSet<int>();
            var receivers = new HashSet<int>();

            // Ensure no one is assigned to themeselves and all users participate
            foreach (var assignment in assignments)
            {
                Assert.NotEqual(assignment.Giver.Id, assignment.Receiver.Id);
                givers.Add(assignment.Giver.Id);
                receivers.Add(assignment.Receiver.Id);
            }

            Assert.Equal(3, givers.Count);
            Assert.Equal(3, receivers.Count);
        }

        [Fact]
        public void GenerateAssignments_InsufficientUsers_ThrowsInvalidOperationException()
        {
            var users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" }
        };

            IAssignmentGenerator assignmentGenerator = new SecretSantaAssignmentGenerator();
            Assert.Throws<InvalidOperationException>(() => assignmentGenerator.GenerateAssignments(users));
        }
    }
}
