using System;
using System.Collections.Generic;
using System.IO;
using SecretSanta.Interface;
using SecretSanta.Models;

namespace SecretSanta.Implementations
{
    public class CsvAssignmentSaver : IAssignmentSaver
    {
        public void SaveAssignments(List<Assignment> assignments, string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("giver_id,giver_name,giver_email,receiver_id,receiver_name,receiver_email");
                    foreach (var assignment in assignments)
                    {
                        writer.WriteLine($"{assignment.Giver.Id},{assignment.Giver.Name},{assignment.Giver.Email}," +
                                         $"{assignment.Receiver.Id},{assignment.Receiver.Name},{assignment.Receiver.Email}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
