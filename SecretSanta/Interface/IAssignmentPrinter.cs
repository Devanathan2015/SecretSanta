using System;
using System.Collections.Generic;
using SecretSanta.Models;

namespace SecretSanta.Interface
{
    public interface IAssignmentPrinter
    {
        void PrintAssignments(List<Assignment> assignments);
    }
}
