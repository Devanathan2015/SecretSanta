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
            // Define file paths
            string usersFilePath = "/Users/ambin03979/Projects/users.csv";
            string assignmentsFilePath = "/Users/ambin03979/Projects/assignments.csv";

            // Create instances of required classes
            IUserReader userReader = new CsvUserReader();
            IAssignmentGenerator assignmentGenerator = new SecretSantaAssignmentGenerator();
            IAssignmentPrinter assignmentPrinter = new ConsoleAssignmentPrinter();
            IAssignmentSaver assignmentSaver = new CsvAssignmentSaver();

            try
            {
                // Read users from CSV
                List<User> users = userReader.ReadUsers(usersFilePath);

                //Generate Secret Santa assignmnets
                List<Assignment> assignments = assignmentGenerator.GenerateAssignments(users);

                // Print assignments to the console
                assignmentPrinter.PrintAssignments(assignments);

                // Save assignments to CSV
                assignmentSaver.SaveAssignments(assignments, assignmentsFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
