using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using SecretSanta.Interface;
using SecretSanta.Models;

namespace SecretSanta.Implementations
{
    public class CsvUserReader : IUserReader
    {
        public List<User> ReadUsers(string filePath)
        {
            var users = new List<User>();

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} does not exist.");
            }

            using (var reader = new StreamReader(filePath))
            {
                string headerLine = reader.ReadLine();
                if (string.IsNullOrEmpty(headerLine))
                {
                    throw new InvalidDataException("The CSV file is empty or missing headers.");
                }

                bool hasData = false;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    hasData = true;
                    var values = line.Split(',');
                    if (values.Length != 3)
                    {
                        throw new InvalidDataException("The CSV file is malformed.");
                    }

                    if (!int.TryParse(values[0], out int id))
                    {
                        throw new InvalidDataException($"Invalid user ID: {values[0]}");
                    }

                    var user = new User
                    {
                        Id = id,
                        Name = values[1],
                        Email = values[2]
                    };

                    users.Add(user);
                }

                if (!hasData)
                {
                    throw new InvalidDataException("The CSV file does not contain any user data.");
                }
            }

            if (users.Count < 3)
            {
                throw new InvalidOperationException("There must be at least 3 users to perform a Secret Santa.");
            }


            return users;
        }
    }
}
