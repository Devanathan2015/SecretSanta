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
            Assert.Equal(3, users.Count);

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
            var filePath = "empty.csv";
            File.WriteAllText(filePath, "");

            IUserReader userReader = new CsvUserReader();
            Assert.Throws<InvalidDataException>(() => userReader.ReadUsers(filePath));

            File.Delete(filePath);
        }

        [Fact]
        public void ReadUsers_MalformedFile_ThrowsInvalidDataException()
        {
            var filePath = "malformed.csv";
            File.WriteAllLines(filePath, new[]
            {
            "id,name,email",
            "1,John Doe",
            "2,Jane Doe,jane@example.com"
        });

            IUserReader userReader = new CsvUserReader();
            Assert.Throws<InvalidDataException>(() => userReader.ReadUsers(filePath));

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
            Assert.Equal(3, assignments.Count);

            var givers = new HashSet<int>();
            var receivers = new HashSet<int>();

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
