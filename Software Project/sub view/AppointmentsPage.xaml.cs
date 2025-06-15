using Software_Project.Controllers;
using Software_Project.Models;
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

namespace Software_Project.sub_view
{
    public partial class AppointmentsPage : Page
    {
        private readonly AppointmentCon _appointmentController = new AppointmentCon();
        private readonly PatientCon _patientController = new PatientCon();
        private readonly DoctorCon _doctorController = new DoctorCon();

        // This page can be opened normally or with a specific patient pre-selected
        public AppointmentsPage(int? preselectedPatientId = null)
        {
            InitializeComponent();
            LoadInitialData();

            // If a patient ID was passed, select them in the ComboBox
            if (preselectedPatientId.HasValue)
            {
                cmbPatients.SelectedValue = preselectedPatientId.Value;
            }
        }

        private void LoadInitialData()
        {
            // Load patients and doctors into the dropdowns
            cmbPatients.ItemsSource = _patientController.GetAllPatients();
            cmbDoctors.ItemsSource = _doctorController.GetAllDoctors();
            // Load all existing appointments into the grid
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            dgAppointments.ItemsSource = _appointmentController.GetAllAppointments();
        }

        private void ClearFields()
        {
            dgAppointments.SelectedItem = null;
            cmbPatients.SelectedIndex = -1;
            cmbDoctors.SelectedIndex = -1;
            txtAppointmentDate.Text = "yyyy-mm-dd hh:mm";
            cmbStatus.SelectedIndex = -1;
            txtReason.Clear();
            btnBook.IsEnabled = true;
        }

        private void DgAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAppointments.SelectedItem is AppointmentM selectedAppt)
            {
                cmbPatients.SelectedValue = selectedAppt.PatientID;
                cmbDoctors.SelectedValue = selectedAppt.DoctorID;
                txtAppointmentDate.Text = selectedAppt.AppointmentDate.ToString("yyyy-MM-dd HH:mm");
                // Find and select the correct status in the ComboBox
                foreach (ComboBoxItem item in cmbStatus.Items)
                {
                    if (item.Content.ToString() == selectedAppt.Status)
                    {
                        cmbStatus.SelectedItem = item;
                        break;
                    }
                }
                txtReason.Text = selectedAppt.Reason;
                btnBook.IsEnabled = false; // Disable booking when editing
            }
        }

        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPatients.SelectedValue == null || cmbDoctors.SelectedValue == null || !DateTime.TryParse(txtAppointmentDate.Text, out DateTime apptDate))
            {
                MessageBox.Show("Please select a patient, a doctor, and enter a valid date/time.", "Missing Information");
                return;
            }

            var newAppt = new AppointmentM
            {
                PatientID = (int)cmbPatients.SelectedValue,
                DoctorID = (int)cmbDoctors.SelectedValue,
                AppointmentDate = apptDate,
                Status = "Scheduled", // New appointments are always scheduled
                Reason = txtReason.Text
            };

            if (_appointmentController.BookAppointment(newAppt))
            {
                MessageBox.Show("Appointment booked successfully!");
                ClearFields();
                LoadAppointments();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgAppointments.SelectedItem is AppointmentM selectedAppt)
            {
                if (cmbPatients.SelectedValue == null || cmbDoctors.SelectedValue == null || !DateTime.TryParse(txtAppointmentDate.Text, out DateTime apptDate))
                {
                    MessageBox.Show("Please select a patient, a doctor, and enter a valid date/time.", "Missing Information");
                    return;
                }

                selectedAppt.PatientID = (int)cmbPatients.SelectedValue;
                selectedAppt.DoctorID = (int)cmbDoctors.SelectedValue;
                selectedAppt.AppointmentDate = apptDate;
                selectedAppt.Status = (cmbStatus.SelectedItem as ComboBoxItem).Content.ToString();
                selectedAppt.Reason = txtReason.Text;

                if (_appointmentController.UpdateAppointment(selectedAppt))
                {
                    MessageBox.Show("Appointment updated successfully!");
                    ClearFields();
                    LoadAppointments();
                }
            }
            else { MessageBox.Show("Please select an appointment from the list to update."); }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is AppointmentM apptToCancel)
            {
                MessageBoxResult confirm = MessageBox.Show("Are you sure you want to cancel this appointment?", "Confirm Cancellation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirm == MessageBoxResult.Yes)
                {
                    if (_appointmentController.CancelAppointment(apptToCancel.AppointmentID))
                    {
                        MessageBox.Show("Appointment cancelled.");
                        LoadAppointments();
                    }
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService?.Navigate(new Software_Project.View.Login());
        }
    }
}
