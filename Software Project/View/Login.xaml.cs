using Microsoft.Data.SqlClient;
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
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Software_Project.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private string? userRole;

        public Login()
        {
            InitializeComponent();
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (AuthenticateUser(username, password))
            {
                string connectionString = "Data Source=MIHITH💖😎\\SQLEXPRESS;Initial Catalog=CMS_System;Integrated Security=True;Trust Server Certificate=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT userRole FROM Users WHERE username = @Username AND password = @Password";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@Password", password);

                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                userRole = result.ToString(); // Retrieve user role if found
                                MessageBox.Show($"Login successful! Role: {userRole}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                                // Navigate to the main page after successful login
                                if (System.Windows.Application.Current.MainWindow is MainWindow mainWin) // Corrected namespace
                                {
                                   
                                    if (!string.IsNullOrEmpty(userRole)) // Ensure userRole is not null or empty
                                    {
                                        if (userRole.Trim() == "admin")
                                        {
                                            Admin Admin = new Admin(); // Create an instance of AdminPage
                                            mainWin.MainContent.Navigate(Admin); // Navigate to AdminPage

                                        }else if (userRole.Trim() == "Receptionist")
                                        {
                                            Receptionist Receptionist = new Receptionist(); // Create an instance of AdminPage
                                            mainWin.MainContent.Navigate(Receptionist); // Navigate to AdminPage

                                        }
                                        else if (userRole.Trim() == "doctor")
                                        {
                                            Doctor doctor = new Doctor(); // Create an instance of AdminPage
                                            mainWin.MainContent.Navigate(doctor); // Navigate to AdminPage

                                        }
                                        else if (userRole.Trim() == "patient")
                                        {
                                            patient patient = new patient(); // Create an instance of AdminPage
                                            mainWin.MainContent.Navigate(patient); // Navigate to AdminPage

                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("User role is not defined!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Navigation service is not available!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        private bool AuthenticateUser(string username, string password)
        {
            string connectionString = "Data Source=MIHITH💖😎\\SQLEXPRESS;Initial Catalog=CMS_System;Integrated Security=True;Trust Server Certificate=True";
            bool isValidUser = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE username = @Username AND password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        isValidUser = count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return isValidUser;
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
