using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Interface;
using SecretSanta.Models;

namespace SecretSanta.Implementations
{
    public class SecretSantaAssignmentGenerator : IAssignmentGenerator
    {
        public List<Assignment> GenerateAssignments(List<User> users)
        {
            if (users != null && users.Count < 3)
            {
                throw new InvalidOperationException("There must be at least 3 users to perform a Secret Santa.");
            }

            List<User> shuffledUsers = users.OrderBy(u => Guid.NewGuid()).ToList();
            List<Assignment> assignments = new List<Assignment>();

            for (int i = 0; i < shuffledUsers.Count; i++)
            {
                User giver = shuffledUsers[i];
                User receiver = shuffledUsers[(i + 1) % shuffledUsers.Count];
                assignments.Add(new Assignment
                {
                    Giver = giver,
                    Receiver = receiver
                });
            }

            return assignments;
        }
    }
}
