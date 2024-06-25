using System;
using System.Collections.Generic;
using SecretSanta.Interface;
using SecretSanta.Models;

namespace SecretSanta.Implementations
{
    public class ConsoleAssignmentPrinter : IAssignmentPrinter
    {
        public void PrintAssignments(List<Assignment> assignments)
        {
            try
            {
                foreach (var assignment in assignments)
                {
                    Console.WriteLine($"{assignment.Giver.Name} ({assignment.Giver.Email}) -> {assignment.Receiver.Name} ({assignment.Receiver.Email})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
    }
}
