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
            category.ItemsSource = dc.Categories.Select(c=> c.Name);
            speciality.ItemsSource = dc.Specialities.Select(s=> s.Name);
        }

        // При нажатии принять
        private void accept_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsAreFilled()) { MessageBox.Show("Не все данные введены!"); return; }

            dc.Employees.InsertAllOnSubmit(Parsing());
            try
            {
                dc.SubmitChanges();
                MessageBox.Show($"Добавлен новый работник в  {DateTime.Now}");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
                dc.SubmitChanges();
            }
        }

        private void category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idcategory = dc.Categories.Where(c => c.Name == category.SelectedItem.ToString()).Select(c => c.Id).Single();
        }

        private void speciality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idspecialiti = dc.Specialities.Where(s => s.Name == speciality.SelectedItem.ToString()).Select(s => s.Id).Single();
        }

        // Метод Parsing извлекаем данные из окна и возвращаем их в виде объкта Doctor
        private List<Employee> Parsing()
        {
            int.TryParse(ex.Text, out experience);

            var employee = new Employee();
            employee.Surname = surname.Text;
            employee.Name = name.Text;
            employee.Patronymic = patronymic.Text;
            employee.CategoryId = idcategory;
            employee.SpecialityId = idspecialiti;
            employee.Experience = experience;

            return new List<Employee>(new[] { employee });
        }//Parsing


        // Метод проверяет все ли данные введены
        private bool FieldsAreFilled()
        {
            // Имеет ли указанная строка значение null, является ли она пустой строкой или строкой,
            // состоящей только из символов-разделителей.

            if (string.IsNullOrWhiteSpace(name.Text)
                || string.IsNullOrWhiteSpace(patronymic.Text)
                || string.IsNullOrWhiteSpace(surname.Text)
                || string.IsNullOrWhiteSpace(ex.Text) ) return true;

            if (category.SelectedItem == null || speciality.SelectedItem == null) return true;

            return false;
        }//FieldsAreFilled

    }
}
