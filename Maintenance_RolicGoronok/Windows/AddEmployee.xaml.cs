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
using System.Windows.Shapes;

namespace Maintenance_RolicGoronok
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        int experience, idcategory, idspecialiti;
        public AddEmployee()
        {
            InitializeComponent();
            Loaded += AddEmployee_Loaded;
        }

        private void AddEmployee_Loaded(object sender, RoutedEventArgs e)
        {
            category.ItemsSource = dc.Categories;
            speciality.ItemsSource = dc.Specialities;
        }

        // При нажатии принять
        private void accept_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsAreNotFilled()) { MessageBox.Show("Не все данные введены!"); return; }

            dc.Employees.InsertOnSubmit(GetEmployee());
            try
            {
                dc.SubmitChanges();
                MessageBox.Show($"Добавлен новый работник");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        // Метод Parsing извлекаем данные из окна и возвращаем их в виде объкта Employee
        private Employees GetEmployee()
        {
            Employees newEmp = new Employees();

            Persons temp = new Persons();
            temp.Surname = surname.Text;
            temp.Name = name.Text;
            temp.Patronymic = patronymic.Text;
            temp.BirthDate = dob.SelectedDate.Value;
            temp.Address = address.Text;
            temp.Passport = passport.Text;
            temp.License = licen.Text;

            newEmp.Persons = temp;
            newEmp.Specialities = speciality.SelectedItem as Specialities;
            newEmp.Categories = category.SelectedItem as Categories;
            newEmp.Experience = int.Parse(ex.Text);

            return newEmp;
        }//Parsing


        // Метод проверяет все ли данные введены
        private bool FieldsAreNotFilled()
        {
            int exp;
            return string.IsNullOrWhiteSpace(name.Text) ||
                   string.IsNullOrWhiteSpace(surname.Text) ||
                   dob.SelectedDate.HasValue ||
                   string.IsNullOrWhiteSpace(address.Text) ||
                   string.IsNullOrWhiteSpace(passport.Text) ||
                   speciality.SelectedItem == null ||
                   int.TryParse(ex.Text, out exp) ||
                   category.SelectedItem == null;
        }//FieldsAreNotFilled

    }
}
