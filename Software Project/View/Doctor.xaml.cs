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

namespace Software_Project.View
{
    /// <summary>
    /// Interaction logic for Doctor.xaml
    /// </summary>
    public partial class Doctor : Page
    {
        public Doctor()
        {
            InitializeComponent();

        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.View.Login());
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.sub_view.Patient());
        }

        private void ViewDoctorsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.sub_view.Doctor());
        }

        private void ViewPatientsBtn_Click(object sender, RoutedEventArgs e)
        {

            NavigationService?.Navigate(new Software_Project.sub_view.Patient());
        }

        private void ViewPrescriptionsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.sub_view.AppointmentsPage());
        }

        private void GenerateReportsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreatePrescriptionBtn_Click(object sender, RoutedEventArgs e)
        {

            NavigationService?.Navigate(new Software_Project.sub_view.PrescriptionsPage());

        }

        private void QuickReportsBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
