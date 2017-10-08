using SWH.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SWH
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Override to avoid casting every time
        /// </summary>
        public static new App Current { get { return Application.Current as App; } }
        /// <summary>
        /// Reference to User Database
        /// </summary>
        public IUserProvider UserProvider { get; private set; }
        /// <summary>
        /// Reference to Login Agent
        /// </summary>
        public ILoginAgent LoginAgent { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Create a UserController instance and set it as UserProvider and LoginAgent
            var controller = new UserController();
            UserProvider = controller;
            LoginAgent = controller;
            //Load users from file
            UserProvider.LoadUsers();
        }
    }
}
