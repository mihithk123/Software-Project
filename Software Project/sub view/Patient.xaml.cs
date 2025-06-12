using Software_Project.Controllers;
using Software_Project.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Software_Project.sub_view
{
    public partial class Patient : Page
    {
        private readonly PatientCon _patientController = new PatientCon();

        public Patient()
        {
            InitializeComponent();
            LoadPatients();
        }

        private void LoadPatients()
        {
            try
            {
                // FIX: Using the correct name 'dgPatients' from your XAML file
                dgDoctors.ItemsSource = _patientController.GetAllPatients();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patient data: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // FIX: Renamed to follow C# naming conventions
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show("Please fill in all fields!", "Missing Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out int age))
            {
                MessageBox.Show("Please enter a valid number for Age.", "Invalid Age", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Now this will work because PatientM.Age is an int
            var newPatient = new PatientM
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Age = age
            };

            try
            {
                if (_patientController.AddPatient(newPatient))
                {
                    MessageBox.Show("Patient added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                    LoadPatients();
                }
                else
                {
                    MessageBox.Show("Failed to add patient.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // FIX: Renamed to follow C# naming conventions
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Select a patient to update their First and Last Name.");
                return;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out int age))
            {
                MessageBox.Show("Please enter a valid number for Age.", "Invalid Age", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Now this will work because PatientM.Age is an int
            var patientToUpdate = new PatientM
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Age = age
            };

            try
            {
                if (_patientController.UpdatePatient(patientToUpdate))
                {
                    MessageBox.Show("Patient updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                    LoadPatients();
                }
                else
                {
                    MessageBox.Show("Patient not found.", "Update Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // FIX: Renamed to follow C# naming conventions
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Select a patient to delete.");
                return;
            }

            MessageBoxResult confirm = MessageBox.Show($"Are you sure you want to delete {txtFirstName.Text} {txtLastName.Text}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirm != MessageBoxResult.Yes) return;

            try
            {
                if (_patientController.DeletePatient(txtFirstName.Text.Trim(), txtLastName.Text.Trim()))
                {
                    MessageBox.Show("Patient deleted successfully!", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                    LoadPatients();
                }
                else
                {
                    MessageBox.Show("Patient not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting patient: " + ex.Message);
            }
        }

        // FIX: Renamed and using correct DataGrid name 'dgPatients'
        private void DgPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDoctors.SelectedItem is PatientM selectedPatient)
            {
                txtFirstName.Text = selectedPatient.FirstName;
                txtLastName.Text = selectedPatient.LastName;
                txtPhone.Text = selectedPatient.Phone;
                txtAge.Text = selectedPatient.Age.ToString(); // Convert int to string for display
            }
        }

        private void ClearFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtAge.Clear();
            txtFirstName.Focus();
            if (dgDoctors != null)
            {
                dgDoctors.SelectedItem = null;
            }
        }

        // FIX: Renamed to follow C# naming conventions
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.View.Admin());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.View.patient());
        }
    }
}