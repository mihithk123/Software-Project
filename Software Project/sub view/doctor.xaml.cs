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

        }

       

        private void btnClear_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Software_Project.View.Admin());
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Windows;
//using System.Windows.Controls;

//namespace YourApp
//{
//    public partial class DoctorAddPage : Page
//    {
//        private ObservableCollection<Doctor> doctors;
//        private int nextId = 1;

//        public DoctorAddPage()
//        {
//            InitializeComponent();
//            InitializeDoctors();
//        }

//        private void InitializeDoctors()
//        {
//            doctors = new ObservableCollection<Doctor>();
//            dgDoctors.ItemsSource = doctors;

//            // Add some sample data
//            AddSampleDoctors();
//        }

//        private void AddSampleDoctors()
//        {
//            doctors.Add(new Doctor
//            {
//                Id = nextId++,
//                FirstName = "John",
//                LastName = "Smith",
//                Specialization = "Cardiology",
//                Phone = "555-0123",
//                Email = "john.smith@hospital.com",
//                Experience = "10"
//            });

//            doctors.Add(new Doctor
//            {
//                Id = nextId++,
//                FirstName = "Sarah",
//                LastName = "Johnson",
//                Specialization = "Pediatrics",
//                Phone = "555-0456",
//                Email = "sarah.johnson@hospital.com",
//                Experience = "8"
//            });
//        }

//        private void btnAdd_Click(object sender, RoutedEventArgs e)
//        {
//            // Validate input
//            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
//                string.IsNullOrWhiteSpace(txtLastName.Text) ||
//                string.IsNullOrWhiteSpace(txtSpecialization.Text))
//            {
//                MessageBox.Show("Please fill in all required fields (First Name, Last Name, Specialization).",
//                               "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
//                return;
//            }

//            // Create new doctor
//            var newDoctor = new Doctor
//            {
//                Id = nextId++,
//                FirstName = txtFirstName.Text.Trim(),
//                LastName = txtLastName.Text.Trim(),
//                Specialization = txtSpecialization.Text.Trim(),
//                Phone = txtPhone.Text.Trim(),
//                Email = txtEmail.Text.Trim(),
//                Experience = txtExperience.Text.Trim()
//            };

//            // Add to collection
//            doctors.Add(newDoctor);

//            // Clear form
//            ClearForm();

//            MessageBox.Show("Doctor added successfully!", "Success",
//                           MessageBoxButton.OK, MessageBoxImage.Information);
//        }

//        private void btnClear_Click(object sender, RoutedEventArgs e)
//        {
//            ClearForm();
//        }

//        private void ClearForm()
//        {
//            txtFirstName.Clear();
//            txtLastName.Clear();
//            txtSpecialization.Clear();
//            txtPhone.Clear();
//            txtEmail.Clear();
//            txtExperience.Clear();
//            txtFirstName.Focus();
//        }

//        private void btnDelete_Click(object sender, RoutedEventArgs e)
//        {
//            var button = sender as Button;
//            if (button?.Tag != null && int.TryParse(button.Tag.ToString(), out int doctorId))
//            {
//                var result = MessageBox.Show("Are you sure you want to delete this doctor?",
//                                           "Confirm Delete", MessageBoxButton.YesNo,
//                                           MessageBoxImage.Question);

//                if (result == MessageBoxResult.Yes)
//                {
//                    var doctorToRemove = doctors.FirstOrDefault(d => d.Id == doctorId);
//                    if (doctorToRemove != null)
//                    {
//                        doctors.Remove(doctorToRemove);
//                        MessageBox.Show("Doctor deleted successfully!", "Success",
//                                       MessageBoxButton.OK, MessageBoxImage.Information);
//                    }
//                }
//            }
//        }
//    }

//    // Doctor model class
//    public class Doctor
//    {
//        public int Id { get; set; }
//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public string Specialization { get; set; }
//        public string Phone { get; set; }
//        public string Email { get; set; }
//        public string Experience { get; set; }
//    }
//}