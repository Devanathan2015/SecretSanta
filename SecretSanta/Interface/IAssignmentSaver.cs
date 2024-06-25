using System;
using System.Collections.Generic;
using SecretSanta.Models;

namespace SecretSanta.Interface
{
    public interface IAssignmentSaver
    {
        void SaveAssignments(List<Assignment> assignments, string filePath);
    }
}
