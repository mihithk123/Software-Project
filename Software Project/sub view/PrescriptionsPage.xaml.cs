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
    public partial class PrescriptionsPage : Page
    {
        private readonly PrescriptionCon _prescriptionController = new PrescriptionCon();
        private readonly PatientCon _patientController = new PatientCon();
        private readonly DoctorCon _doctorController = new DoctorCon();

        public PrescriptionsPage()
        {
            InitializeComponent();
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            // Load dropdowns
            cmbPatients.ItemsSource = _patientController.GetAllPatients();
            cmbDoctors.ItemsSource = _doctorController.GetAllDoctors();
            // Load grid
            LoadPrescriptions();
        }

        private void LoadPrescriptions()
        {
            dgPrescriptions.ItemsSource = _prescriptionController.GetAllPrescriptions();
        }

        private void ClearFields()
        {
            dgPrescriptions.SelectedItem = null;
            cmbPatients.SelectedIndex = -1;
            cmbDoctors.SelectedIndex = -1;
            txtMedication.Clear();
            txtDosage.Clear();
            txtFrequency.Clear();
            dpStartDate.SelectedDate = null;
            dpEndDate.SelectedDate = null;
            txtNotes.Clear();
            btnCreate.IsEnabled = true;
        }

        private void DgPrescriptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPrescriptions.SelectedItem is PrescriptionM selectedPres)
            {
                cmbPatients.SelectedValue = selectedPres.PatientID;
                cmbDoctors.SelectedValue = selectedPres.DoctorID;
                txtMedication.Text = selectedPres.Medication;
                txtDosage.Text = selectedPres.Dosage;
                txtFrequency.Text = selectedPres.Frequency;
                dpStartDate.SelectedDate = selectedPres.StartDate;
                dpEndDate.SelectedDate = selectedPres.EndDate;
                txtNotes.Text = selectedPres.Notes;
                btnCreate.IsEnabled = false; // Disable create when editing
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPatients.SelectedValue == null || cmbDoctors.SelectedValue == null || string.IsNullOrWhiteSpace(txtMedication.Text) || dpStartDate.SelectedDate == null)
            {
                MessageBox.Show("Patient, Doctor, Medication, and Start Date are required.", "Missing Information");
                return;
            }

            var newPres = new PrescriptionM
            {
                PatientID = (int)cmbPatients.SelectedValue,
                DoctorID = (int)cmbDoctors.SelectedValue,
                Medication = txtMedication.Text,
                Dosage = txtDosage.Text,
                Frequency = txtFrequency.Text,
                StartDate = dpStartDate.SelectedDate.Value,
                EndDate = dpEndDate.SelectedDate,
                Notes = txtNotes.Text
            };

            if (_prescriptionController.CreatePrescription(newPres))
            {
                MessageBox.Show("Prescription created successfully!");
                ClearFields();
                LoadPrescriptions();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgPrescriptions.SelectedItem is PrescriptionM selectedPres)
            {
                if (cmbPatients.SelectedValue == null || cmbDoctors.SelectedValue == null || string.IsNullOrWhiteSpace(txtMedication.Text) || dpStartDate.SelectedDate == null)
                {
                    MessageBox.Show("Patient, Doctor, Medication, and Start Date are required.", "Missing Information");
                    return;
                }

                // Update the selected object with the new values
                selectedPres.PatientID = (int)cmbPatients.SelectedValue;
                selectedPres.DoctorID = (int)cmbDoctors.SelectedValue;
                selectedPres.Medication = txtMedication.Text;
                selectedPres.Dosage = txtDosage.Text;
                selectedPres.Frequency = txtFrequency.Text;
                selectedPres.StartDate = dpStartDate.SelectedDate.Value;
                selectedPres.EndDate = dpEndDate.SelectedDate;
                selectedPres.Notes = txtNotes.Text;

                if (_prescriptionController.UpdatePrescription(selectedPres))
                {
                    MessageBox.Show("Prescription updated successfully!");
                    ClearFields();
                    LoadPrescriptions();
                }
            }
            else
            {
                MessageBox.Show("Please select a prescription from the list to update.");
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
    }
}
