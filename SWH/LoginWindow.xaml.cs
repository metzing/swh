using SWH.BL;
using SWH.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWH
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when the login button is pushed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoginButton(object sender, RoutedEventArgs e)
        {
            //Try to log in with the creditentials
            if (UserController.Instance.TryLogin(UserNameTextBox.Text, PasswordTextBox.Password))
            {
                //Success, open new TableWindow
                ResponseLabel.Content = "Successful login";
                ResponseLabel.Foreground = new SolidColorBrush(Colors.Green);
                new TableWindow().ShowDialog();
            }
            else
            {
                //Error, notify user
                ResponseLabel.Content = "Unsuccessful login";
                ResponseLabel.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
