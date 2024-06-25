using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using SecretSanta.Implementations;
using SecretSanta.Interface;
using SecretSanta.Models;

namespace SecretSanta
{
    class Program
    {
        static void Main(string[] args)
        {

            string usersFilePath = "/Users/ambin03979/Projects/users.csv";
            string assignmentsFilePath = "/Users/ambin03979/Projects/assignments.csv";

            IUserReader userReader = new CsvUserReader();
            IAssignmentGenerator assignmentGenerator = new SecretSantaAssignmentGenerator();
            IAssignmentPrinter assignmentPrinter = new ConsoleAssignmentPrinter();
            IAssignmentSaver assignmentSaver = new CsvAssignmentSaver();

            try
            {
                List<User> users = userReader.ReadUsers(usersFilePath);
                List<Assignment> assignments = assignmentGenerator.GenerateAssignments(users);

                assignmentPrinter.PrintAssignments(assignments);
                assignmentSaver.SaveAssignments(assignments, assignmentsFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
