using SWH.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SWH.BL
{
    class UserController : IUserProvider, ILoginAgent
    {
        /// <summary>
        /// Users in the database
        /// </summary>
        public List<User> Users { get; private set; }

        /// <summary>
        /// Reads users into memory from database file
        /// </summary>
        public void LoadUsers()
        {
            Users = new List<User>();

            foreach (var line in File.ReadAllLines("database.txt"))
            {
                Users.Add(UserFactory.FromCSVLine(line));
            }
        }

        /// <summary>
        /// Tries to log in user using their UserName and Password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>Returns true if a matching User could be found, false otherwise</returns>
        public bool TryLogin(string userName, string password)
        {
            try
            {
                //Throws InvalidOperationException when there is no item that meets criteria
                Users.First(u => u.UserName == userName && u.Password == password);

                //User could be found, Login successful
                return true;
            }
            catch (InvalidOperationException exception)
            {
                //User could not be found, Login unsuccessful
                return false;
            }
        }

        /// <summary>
        /// Saves the Users to a file
        /// </summary>
        /// <param name="filename">File to be written into</param>
        public void SaveToXML(string filename)
        {
            FileStream outputFile = File.OpenWrite(filename);
            XmlSerializer ser = new XmlSerializer(typeof(List<User>));
            ser.Serialize(outputFile, Users);
            outputFile.Close();
        }

        /// <summary>
        /// Get all users in the database
        /// </summary>
        /// <returns>List of all users</returns>
        public List<User> GetUsers()
        {
            return Users;
        }

        /// <summary>
        /// Get the group of users that match the predicate in parameter
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>List of users that match the given predicate</returns>
        public List<User> GetUsers(Predicate<User> predicate)
        {
            List<User> result = new List<User>();
            foreach (var user in Users)
            {
                if (predicate(user))
                {
                    result.Add(user);
                }
            }
            return result;
        }

        /// <summary>
        /// Saves the content of the memory into the database file
        /// </summary>
        public void SaveToDataBase()
        {
            List<string> lines = new List<string>();
            foreach (var user in Users)
            {
                lines.Add(UserToCSV(user));
            }

            File.WriteAllLines("database.txt", lines.ToArray());
        }

        /// <summary>
        /// Creates a CSV string representation of the User object passed
        /// </summary>
        /// <param name="user">Object to be serialized</param>
        /// <returns>CSV string</returns>
        private string UserToCSV(User user)
        {
            return $"{user.ID};{user.UserName};{user.Password};{user.LastName};{user.FirstName};{user.DateOfBirth};{user.PlaceOfBirth};{user.HomeTown}\n";
        }
    }

    /// <summary>
    /// Interface containing the neccessary methods for using the User database
    /// </summary>
    public interface IUserProvider
    {
        void LoadUsers();
        List<User> GetUsers();
        List<User> GetUsers(Predicate<User> predicate);
        void SaveToXML(string filename);
        void SaveToDataBase();
    }

    /// <summary>
    /// Interface with the method for logging in to the program.
    /// </summary>
    public interface ILoginAgent
    {
        bool TryLogin(string userName, string password);
    }
}
