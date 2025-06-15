// FILE: Software_Project/sub_view/Patient.xaml.cs

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
                dgPatients.ItemsSource = _patientController.GetAllPatients();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patient data: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("First Name and Last Name are required.", "Missing Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newPatient = new PatientM
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Age = txtAge.Text.Trim()
            };

            try
            {
                if (_patientController.AddPatient(newPatient))
                {
                    MessageBox.Show("Patient added successfully!");
                    ClearFields();
                    LoadPatients();
                }
                else
                {
                    MessageBox.Show("Failed to add patient.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error");
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgPatients.SelectedItem is PatientM selectedPatient)
            {
                var patientToUpdate = new PatientM
                {
                    Id = selectedPatient.Id, // Use the ID for a safe update
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Age = txtAge.Text.Trim()
                };

                try
                {
                    if (_patientController.UpdatePatient(patientToUpdate))
                    {
                        MessageBox.Show("Patient updated successfully!");
                        ClearFields();
                        LoadPatients();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update patient.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error");
                }
            }
            else
            {
                MessageBox.Show("Please select a patient from the list to update.");
            }
        }

        // FIX: This method handles the delete button click
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            if (dgPatients.SelectedItem is PatientM selectedPatient)
            {
                MessageBoxResult confirm = MessageBox.Show($"Are you sure you want to delete {selectedPatient.FirstName} {selectedPatient.LastName}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirm != MessageBoxResult.Yes) return;

                try
                {
                    // Call the safe delete method with the patient's ID
                    if (_patientController.DeletePatient(selectedPatient.Id))
                    {
                        MessageBox.Show("Patient deleted successfully!");
                        ClearFields();
                        LoadPatients();
                    }
                    else
                    {
                        MessageBox.Show("Patient not found or could not be deleted.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting patient: " + ex.Message, "Database Error");
                }
            }
            else
            {
                MessageBox.Show("Please select a patient from the list to delete.");
            }
        }

        private void DgPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPatients.SelectedItem is PatientM selectedPatient)
            {
                txtFirstName.Text = selectedPatient.FirstName;
                txtLastName.Text = selectedPatient.LastName;
                txtPhone.Text = selectedPatient.Phone;
                txtAge.Text = selectedPatient.Age;
            }
        }

        private void ClearFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtAge.Clear();
            dgPatients.SelectedItem = null;
            txtFirstName.Focus();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            // Assuming you have an Admin view to navigate back to
            NavigationService?.Navigate(new Software_Project.View.Admin());
        }

        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            // Get the patient from the row where the button was clicked
            if ((sender as FrameworkElement)?.DataContext is Models.PatientM selectedPatient)
            {
                // Create the full name string
                string fullName = $"{selectedPatient.FirstName} {selectedPatient.LastName}";

                // Navigate to the history page, passing the patient's ID and name
                NavigationService?.Navigate(new MedicalHistoryPage(selectedPatient.Id, fullName));
            }
        }

        private void BookAppointment_Click(object sender, RoutedEventArgs e)
        {
            // Get the patient from the row where the button was clicked
            if ((sender as FrameworkElement)?.DataContext is Models.PatientM selectedPatient)
            {
                // Navigate to the appointments page, passing the patient's ID to pre-select them
                NavigationService?.Navigate(new AppointmentsPage(selectedPatient.Id));
            }
        }
    }
}