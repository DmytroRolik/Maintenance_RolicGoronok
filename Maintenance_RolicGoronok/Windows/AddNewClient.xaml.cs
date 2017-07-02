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
    /// Interaction logic for AddNewClient.xaml
    /// </summary>
    public partial class AddNewClient : Window
    {
        MaintenanceDataContext dc;
        public AddNewClient()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            //проверяет все ли данные введены
            if (FieldsAreNotFilled()) { MessageBox.Show("Не все данные введены"); return; }

            using (dc = new MaintenanceDataContext()) {
                dc.Persons.InsertOnSubmit(GetPerson());
                try {
                    dc.SubmitChanges();
                    MessageBox.Show("Клиент добавлен");
                }
                catch (Exception f) {
                    MessageBox.Show(f.Message);
                }// try-catch
            }// using
        }// add_Click


        // Метод проверяет все ли данные введены
        private bool FieldsAreNotFilled()
        {
            // Имеет ли указанная строка значение null, является ли она пустой строкой или строкой,
            // состоящей только из символов-разделителей.

            return string.IsNullOrWhiteSpace(address.Text)
                || string.IsNullOrWhiteSpace(name.Text)
                || string.IsNullOrWhiteSpace(patronymic.Text)
                || string.IsNullOrWhiteSpace(surname.Text)
                || string.IsNullOrWhiteSpace(passport.Text)
                || string.IsNullOrWhiteSpace(licen.Text) 
                || dob.SelectedDate == null;
        }//FieldsAreNotFilled

        // Метод Parsing извлекаем данные из окна и возвращаем их в виде объкта Client
        private Persons GetPerson()
        {
            Persons newPerson = new Persons();
            newPerson.Surname = surname.Text;
            newPerson.Name = name.Text;
            newPerson.Patronymic = patronymic.Text;
            newPerson.Address = address.Text;
            newPerson.Passport = passport.Text;
            newPerson.BirthDate = dob.SelectedDate.Value;

            return newPerson;
        }//GetPerson
    }
}
