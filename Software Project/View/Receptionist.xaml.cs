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
    /// Interaction logic for Receptionist.xaml
    /// </summary>
    public partial class Receptionist : Page
    {
        public Receptionist()
        {
            InitializeComponent();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.View.Login());
        }

        private void ViewPatientsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.sub_view.Patient());
        }

        private void ViewDoctorsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.sub_view.Doctor());
        }

        private void ViewAppointmentsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.sub_view.Other_Users());
        }

        private void ViewPrescriptionsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.sub_view.AppointmentsPage());
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.sub_view.Patient());
        }
    }
}
