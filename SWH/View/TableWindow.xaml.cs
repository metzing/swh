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

            DataContext = UserController.Instance;
        }

        private void OnSaveXML(object sender, RoutedEventArgs e)
        {
            //Create dialog to specify file location
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "XML files (*.xml)|*.xml";
            //Showdialog returns true if user pressed 'OK', false elsewise
            if (ofd.ShowDialog().Value) {
                //Request UserController to save to file
                UserController.Instance.SerializeUsersToXML(ofd.FileName);
            }
        }

        private void FilterConditionChanged(object sender, TextChangedEventArgs e)
        {
            //If the new key is longer than 3 characters..
            if (SearchTextBox.Text.Length >= 3)
            {
                //..set Filter predicate to method with proper signature
                UsersListView.Items.Filter = ApplySearch;
            }
            else
            {
                //.. if not, then dont filter the items
                UsersListView.Items.Filter = null;
            }
        }

        /// <summary>
        /// Predicate method for filtering User objects
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if the parameter fits the search key, false otherwise</returns>
        private bool ApplySearch(object obj)
        {
            //Safe cast object to User
            User user = obj as User;

            if (user == null)
            {
                return false;
            }
            else
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
        }

        /// <summary>
        /// Called when the Edit button is pressed. Opens a new DetailsWindow with the selected User object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEditButton(object sender, RoutedEventArgs e)
        {
            try
            {
                DetailsWindow details = new DetailsWindow();
                details.User = UsersListView.Items[UsersListView.SelectedIndex] as User;
                details.ShowDialog();
            }
            catch (ArgumentOutOfRangeException exception)
            {
                //Index can be out of range when nothing is selected   
            }
        }
    }
}
