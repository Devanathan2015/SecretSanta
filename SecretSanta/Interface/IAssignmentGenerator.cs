using System;
using System.Collections.Generic;
using SecretSanta.Models;

namespace SecretSanta.Interface
{
    public interface IAssignmentGenerator
    {
        List<Assignment> GenerateAssignments(List<User> users);
    }
}
