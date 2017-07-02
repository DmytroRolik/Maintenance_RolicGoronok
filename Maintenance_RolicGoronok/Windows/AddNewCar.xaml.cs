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
    /// Interaction logic for AddNewCar.xaml
    /// </summary>
    public partial class AddNewCar : Window
    {
        MaintenanceDataContext dc;
        int idmodel, idowner;
        public AddNewCar()
        {
            InitializeComponent();
            Loaded += AddNewCar_Loaded;
        }

        // При загрузке окна 
        void AddNewCar_Loaded(object sender, RoutedEventArgs e)
        {
            using (dc = new MaintenanceDataContext()) {
                // Все модели в comboBox model
                model.ItemsSource = dc.Models;
                // Все персоны в comboBox owner
                owner.ItemsSource = dc.Persons;
            }// using
        }// AddNewCar_Loaded


        // При нажатии добавить владельца
        private void addOwner_Click(object sender, RoutedEventArgs e)
        {
            new AddNewOwner().ShowDialog();

            using (dc = new MaintenanceDataContext()) {
                // Обновляем персоны в comboBox owner
                owner.ItemsSource = dc.Persons;
            }// using
        }// addOwner_Click

        private void addModel_Click(object sender, RoutedEventArgs e)
        {
            new AddModel().ShowDialog();

            using (dc = new MaintenanceDataContext()) {
                // Все модели в comboBox model
                model.ItemsSource = dc.Models;
            }// using
        }// addModel_Click

        private void add_Click(object sender, RoutedEventArgs e)
        {
            //проверяет все ли данные введены
            if (FieldsAreNotFilled()) { MessageBox.Show("Не все данные введены"); return; }

            using (dc = new MaintenanceDataContext()) {
                dc.Cars.InsertOnSubmit(GetCar());

                try {
                    dc.SubmitChanges();
                    MessageBox.Show("Авто добавлено");
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }// try-catch
            }// using
        }// add_Click


        // Метод Parsing извлекаем данные из окна и возвращаем их в виде объкта Car
        private Cars GetCar()
        {
            Cars newCar = new Cars();
            newCar.Models = model.SelectedItem as Models;
            newCar.ProductionYear = date.SelectedDate.Value;
            newCar.Number = number.Text;
            newCar.Color = color.Text;
            newCar.Persons = owner.SelectedItem as Persons;

            return newCar;
        }//GetCar


        // Метод проверяет все ли данные введены
        private bool FieldsAreNotFilled()
        {
            // Имеет ли указанная строка значение null, является ли она пустой строкой или строкой,
            // состоящей только из символов-разделителей.

            return string.IsNullOrWhiteSpace(color.Text) || 
                   string.IsNullOrWhiteSpace(number.Text) ||
                   model.SelectedItem == null || 
                   owner.SelectedItem == null||
                   date.SelectedDate == null;
        }//FieldsAreFilled
    }
}
