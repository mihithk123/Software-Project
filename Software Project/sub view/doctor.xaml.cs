using Software_Project.Controllers; // Using your new controller
using Software_Project.Models;      // Using your new model
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Software_Project.sub_view
{
    public partial class Doctor : Page
    {
        // Use your controller name: DoctorCon
        private readonly DoctorCon _doctorController = new DoctorCon();

        public Doctor()
        {
            InitializeComponent();
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            try
            {
                dgDoctors.ItemsSource = _doctorController.GetAllDoctors();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtSpecialization.Text))
            {
                MessageBox.Show("First Name, Last Name, and Specialization are required!");
                return;
            }

            // Create an instance of your model: DocM
            var newDoctor = new DocM
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Specialization = txtSpecialization.Text.Trim()
            };

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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("First Name and Last Name are required to identify the doctor to update!");
                return;
            }

            var doctorToUpdate = new DocM
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Specialization = txtSpecialization.Text.Trim()
            };

            int.TryParse(txtExperience.Text.Trim(), out int years);
            doctorToUpdate.ExperienceYears = years;

            try
            {
                if (_doctorController.UpdateDoctor(doctorToUpdate))
                {
                    MessageBox.Show("Doctor updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                    LoadDoctors();
                }
                else
                {
                    MessageBox.Show("No matching doctor found to update!", "Update Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter both First Name and Last Name to delete.", "Missing Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult confirm = MessageBox.Show($"Are you sure you want to delete Dr. {txtFirstName.Text} {txtLastName.Text}?",
                "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes)
                return;

            try
            {
                if (_doctorController.DeleteDoctor(txtFirstName.Text.Trim(), txtLastName.Text.Trim()))
                {
                    MessageBox.Show("Doctor deleted successfully!", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                    LoadDoctors();
                }
                else
                {
                    MessageBox.Show("Doctor not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting doctor: " + ex.Message);
            }
        }

        private void dgDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDoctors.SelectedItem is DocM selectedDoctor)
            {
                txtFirstName.Text = selectedDoctor.FirstName;
                txtLastName.Text = selectedDoctor.LastName;
                txtPhone.Text = selectedDoctor.Phone;
                txtExperience.Text = selectedDoctor.ExperienceYears.ToString();
                txtSpecialization.Text = selectedDoctor.Specialization;
            }
        }

        private void ClearFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtExperience.Clear();
            txtSpecialization.Clear();
            txtFirstName.Focus();
            dgDoctors.SelectedItem = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.View.Admin());
        }
    }
}