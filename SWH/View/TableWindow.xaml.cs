using Microsoft.Win32;
using SWH.BL;
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
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        public TableWindow()
        {
            InitializeComponent();

            UsersListView.ItemsSource = App.Current.UserProvider.GetUsers();
        }

        private void OnSaveXML(object sender, RoutedEventArgs e)
        {
            //Create dialog to specify file location
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "XML files (*.xml)|*.xml";
            //Showdialog returns Nullable<bool>, that is true if user pressed 'OK', false elsewise
            if (ofd.ShowDialog().Value)
            {
                //Request UserController to save to file
                App.Current.UserProvider.SaveToXML(ofd.FileName);
            }
        }

        private void FilterConditionChanged(object sender, TextChangedEventArgs e)
        {
            //If the new key is longer than 3 characters..
            if (SearchTextBox.Text.Length >= 3)
            {
                //..set source to filtered items
                UsersListView.ItemsSource = App.Current.UserProvider.GetUsers(ApplySearch);
            }
            else
            {
                //.. if not, then dont filter the items
                UsersListView.ItemsSource = App.Current.UserProvider.GetUsers();
            }
        }

        /// <summary>
        /// Predicate method for filtering User objects
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if the parameter fits the search key, false otherwise</returns>
        private bool ApplySearch(User user)
        {
            string key = SearchTextBox.Text;

            //Compare search key to all fields' string representation
            if (user.ID.ToString().Contains(key)) return true;
            if (user.UserName.Contains(key)) return true;
            if (user.Password.Contains(key)) return true;
            if (user.FirstName.Contains(key)) return true;
            if (user.LastName.Contains(key)) return true;
            if (user.PlaceOfBirth.Contains(key)) return true;
            if (user.DateOfBirth.ToString().Contains(key)) return true;
            if (user.HomeTown.ToString().Contains(key)) return true;

            return false;
        }

        /// <summary>
        /// Called when the Edit button is pressed. Opens a new DetailsWindow with the selected User object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEditButton(object sender, RoutedEventArgs e)
        {
            User user = UsersListView.SelectedItem as User;
            if (user == null)
                return;

            DetailsWindow details = new DetailsWindow();
            details.User = user;
            details.ShowDialog();
        }
    }
}
