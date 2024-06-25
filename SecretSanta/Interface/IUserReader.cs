using System;
using System.Collections.Generic;
using SecretSanta.Models;

namespace SecretSanta.Interface
{
    public interface IUserReader
    {
        List<User> ReadUsers(string filePath);
    }
}
