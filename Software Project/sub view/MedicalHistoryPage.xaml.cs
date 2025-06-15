using Software_Project.Controllers;
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
    public partial class MedicalHistoryPage : Page
    {
        private readonly HistoryCon _historyController = new HistoryCon();

        // The constructor now takes the Patient's ID and Name as input
        public MedicalHistoryPage(int patientId, string patientFullName)
        {
            InitializeComponent();

            // Set the title
            lblPatientName.Text = $"Medical History for: {patientFullName}";

            // Load the history for that specific patient
            lvHistory.ItemsSource = _historyController.GetHistoryForPatient(patientId);
        }

        private void GoBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // This safely navigates back to the previous page (the patient list)
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}