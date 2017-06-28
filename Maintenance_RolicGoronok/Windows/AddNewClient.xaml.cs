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
        MaintenanceDataContext dc = new MaintenanceDataContext();
        public AddNewClient()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            //проверяет все ли данные введены
            if (FieldsAreFilled()) { MessageBox.Show("Не все данные введены"); return; }

            dc.Clients.InsertAllOnSubmit(Parsing());
            SubmitChanges("Клиент добавлен");
        }


        // Метод проверяет все ли данные введены
        private bool FieldsAreFilled()
        {
            // Имеет ли указанная строка значение null, является ли она пустой строкой или строкой,
            // состоящей только из символов-разделителей.

            if (string.IsNullOrWhiteSpace(address.Text)
                || string.IsNullOrWhiteSpace(name.Text)
                || string.IsNullOrWhiteSpace(patronymic.Text)
                || string.IsNullOrWhiteSpace(surname.Text)
                || string.IsNullOrWhiteSpace(passport.Text)
                || string.IsNullOrWhiteSpace(licen.Text)) return true;

            if (dob.SelectedDate == null) return true;

            return false;
        }//FieldsAreFilled

        // Метод Parsing извлекаем данные из окна и возвращаем их в виде объкта Client
        private List<Client> Parsing()
        {
            var person = new Client();
            person.Surname = surname.Text;
            person.Name = name.Text;
            person.Patronymic = patronymic.Text;
            person.Address = address.Text;
            person.Passport = passport.Text;
            person.BirthDate = dob.SelectedDate.Value;

            return new List<Client>(new[] { person });
        }//Parsing

        public void SubmitChanges(string text)
        {
            try
            {
                dc.SubmitChanges();
                MessageBox.Show(text + "  {0}, DateTime.Now");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
                dc.SubmitChanges();
            }
        }
    }
}
