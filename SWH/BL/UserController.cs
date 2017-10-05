using SWH.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWH.BL
{
    class UserController
    {
        private static UserController instance;
        /// <summary>
        /// Singleton instance
        /// </summary>
        public static UserController Instance
        {
            get
            {
                //If instance is null (first access), create singleton instance and return it
                return instance ?? (instance = new UserController());
            }
        }

        /// <summary>
        /// Users in the database
        /// </summary>
        public ObservableCollection<User> Users { get; private set; }

        private UserController()
        {
            LoadUsers();
        }

        /// <summary>
        /// Reads users into memory from database file
        /// </summary>
        public void LoadUsers()
        {
            Users = new ObservableCollection<User>();

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
                //Throws InvalidOperationException when there is no item that meet criteria
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
    }
}
