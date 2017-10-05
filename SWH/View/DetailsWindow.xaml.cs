using SWH.Model;
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
using System.Windows.Shapes;

namespace SWH.View
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        /// <summary>
        /// User whos details are being edited
        /// </summary>
        public User User { get; set; }

        public DetailsWindow()
        {
            InitializeComponent();

            //Set datacontext for binding
            DataContext = this;
        }

        /// <summary>
        /// Saves the data given into the User object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSave(object sender, RoutedEventArgs e)
        {
            //Save fields
            User.UserName = UserNameTextBox.Text;
            User.Password = PasswordTextBox.Text;
            User.LastName = LastNameTextBox.Text;
            User.FirstName = FirstNameTextBox.Text;
            User.PlaceOfBirth = PlaceOfBirthTextBox.Text;
            User.HomeTown = HomwTownTextBox.Text;
            User.DateOfBirth = BirthDatePicker.SelectedDate.Value;

            //Close window
            Close();
        }
    }
}
