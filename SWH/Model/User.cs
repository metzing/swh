using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWH.Model
{
    /// <summary>
    /// Represents a User of the system
    /// </summary>
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string HomeTown { get; set; }
    }

    /// <summary>
    /// Factory class for creating User objects
    /// </summary>
    public static class UserFactory
    {
        /// <summary>
        /// Creates a User object from a string containing the fields separated by ';'
        /// </summary>
        /// <param name="line">String of fields</param>
        /// <returns>New User object with properties set</returns>
        public static User FromCSVLine(string line)
        {
            string[] fields = line.Split(';');
            return new User
            {
                ID = int.Parse(fields[0]),
                UserName = fields[1],
                Password = fields[2],
                LastName = fields[3],
                FirstName = fields[4],
                DateOfBirth = DateTime.Parse(fields[5]),
                PlaceOfBirth = fields[6],
                HomeTown = fields[7]
            };
        }
    }
}
