// FILE: Software_Project/sub_view/Other_Users.xaml.cs

using Software_Project.Controllers;
using Software_Project.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Software_Project.sub_view
{
    public partial class Other_Users : Page
    {
        private readonly OthersCon _userController = new OthersCon();

        public Other_Users()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                dgUsers.ItemsSource = _userController.GetAllUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtUserName.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out int age))
            {
                MessageBox.Show("Please enter a valid number for Age.", "Invalid Age", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = new OthM
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Age = age,
                UserName = txtUserName.Text.Trim()
            };

            try
            {
                bool success = false;
                // If an item is selected, we update; otherwise, we add.
                if (dgUsers.SelectedItem != null)
                {
                    success = _userController.UpdateUser(user);
                    MessageBox.Show(success ? "User updated successfully!" : "Failed to update user.", "Update Status", MessageBoxButton.OK, success ? MessageBoxImage.Information : MessageBoxImage.Error);
                }
                else
                {
                    success = _userController.AddUser(user);
                    MessageBox.Show(success ? "User added successfully!" : "Failed to add user. Username may already exist.", "Add Status", MessageBoxButton.OK, success ? MessageBoxImage.Information : MessageBoxImage.Error);
                }

                if (success)
                {
                    ClearFields();
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Operation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the user object from the button's DataContext
            if ((sender as FrameworkElement)?.DataContext is OthM userToDelete)
            {
                MessageBoxResult confirm = MessageBox.Show($"Are you sure you want to delete {userToDelete.UserName}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirm == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (_userController.DeleteUser(userToDelete.UserName))
                        {
                            MessageBox.Show("User deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearFields();
                            LoadUsers();
                        }
                        else
                        {
                            MessageBox.Show("Could not delete the user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Deletion Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void DgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUsers.SelectedItem is OthM selectedUser)
            {
                txtFirstName.Text = selectedUser.FirstName;
                txtLastName.Text = selectedUser.LastName;
                txtPhone.Text = selectedUser.Phone;
                txtAge.Text = selectedUser.Age.ToString();
                txtUserName.Text = selectedUser.UserName;

                // When an item is selected, the "Add" button becomes an "Update" button
                btnAdd.Content = "Update User";
                txtUserName.IsEnabled = false; // Prevent changing the unique key while updating
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtAge.Clear();
            txtUserName.Clear();
            dgUsers.SelectedItem = null;

            btnAdd.Content = "Add User"; // Reset button text
            txtUserName.IsEnabled = true; // Re-enable the username textbox
            txtFirstName.Focus();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.View.Admin());
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}