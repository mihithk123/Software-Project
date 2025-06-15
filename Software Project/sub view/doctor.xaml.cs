using Software_Project.Controllers;
using Software_Project.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Software_Project.sub_view
{
    public partial class Doctor : Page
    {
        private DoctorCon _doctorController;

        public Doctor()
        {
            InitializeComponent();
            _doctorController = new DoctorCon();
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            try
            {
                var doctors = _doctorController.GetAllDoctors();
                dgDoctors.ItemsSource = doctors;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // *** CORRECTED ***
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtSpecialization.Text))
            {
                MessageBox.Show("First Name, Last Name, and Specialization are required!");
                return;
            }

            // Create the doctor object WITHOUT the ID. The database will create it.
            var newDoctor = new DocM
            {
                // ID IS REMOVED FROM HERE - THIS FIXES THE CRASH
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Specialization = txtSpecialization.Text.Trim()
            };

            // Safely parse experience years
            int.TryParse(txtExperience.Text.Trim(), out int years);
            newDoctor.ExperienceYears = years;

            try
            {
                if (_doctorController.AddDoctor(newDoctor))
                {
                    MessageBox.Show("Doctor added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                    LoadDoctors();
                }
                else
                {
                    MessageBox.Show("Failed to add doctor!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // *** CORRECTED and COMPLETED ***
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ID.Text.Trim(), out int doctorId))
            {
                MessageBox.Show("Please select a doctor from the list to update.", "No Doctor Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var doctorToUpdate = new DocM
            {
                Id = doctorId,
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Specialization = txtSpecialization.Text.Trim()
            };

            // *** ADDED: Don't forget to update experience years! ***
            int.TryParse(txtExperience.Text.Trim(), out int years);
            doctorToUpdate.ExperienceYears = years;

            try
            {
                // *** ADDED: Call the update method and handle the result ***
                if (_doctorController.UpdateDoctor(doctorToUpdate)) // Assuming you have an UpdateDoctor method
                {
                    MessageBox.Show("Doctor updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                    LoadDoctors();
                }
                else
                {
                    MessageBox.Show("Failed to update doctor!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating doctor: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDoctors.SelectedItem is DocM selectedDoctor)
            {
                ID.Text = selectedDoctor.Id.ToString();
                txtFirstName.Text = selectedDoctor.FirstName;
                txtLastName.Text = selectedDoctor.LastName;
                txtPhone.Text = selectedDoctor.Phone;
                txtExperience.Text = selectedDoctor.ExperienceYears.ToString();
                txtSpecialization.Text = selectedDoctor.Specialization;
            }
        }

        // *** CORRECTED ***
        private void ClearFields()
        {
            ID.Clear(); // <-- ADDED: Clear the ID field as well
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtExperience.Clear();
            txtSpecialization.Clear();
            txtFirstName.Focus();
            dgDoctors.SelectedItem = null;
        }

        // Navigation methods (unchanged)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.View.Doctor());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.sub_view.Doctor());
        }
    }
}