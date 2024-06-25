using System;
namespace SecretSanta.Models
{
    public class Assignment
    {
        public User Giver { get; set; }
        public User Receiver { get; set; }
    }
}
