using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for doctor.xaml
    /// </summary>
    public partial class Doctor : Page
    {
        public Doctor()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=MIHITH💖😎\\SQLEXPRESS;Initial Catalog=Doctor;Integrated Security=True;Trust Server Certificate=True");
            SqlCommand command = new SqlCommand("INSERT INTO Doctor (DoctorID, DoctorName, Specialization, ContactNumber) VALUES (@DoctorID, @DoctorName, @Specialization, @ContactNumber)", con);
        }

       

        private void btnClear_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.View.Admin());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}

