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
    /// Interaction logic for AddNewOwner.xaml
    /// </summary>
    public partial class AddNewOwner : Window
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        public AddNewOwner()
        {
            InitializeComponent();
        }

        // При нажатии кнопки добавить
        private void add_Click(object sender, RoutedEventArgs e)
        {
            //проверяет все ли данные введены
            if (FieldsAreFilled()) { MessageBox.Show("Не все данные введены"); return; }

            // Добавляем запись в базу
            dc.Owners.InsertAllOnSubmit(Parsing());
            try
            {
                dc.SubmitChanges();
                MessageBox.Show($"Новый владелец добавлена в {DateTime.Now}");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
                dc.SubmitChanges();
            }//try-catch
        }

        // Метод Parsing извлекаем данные из окна и возвращаем их в виде объкта Doctor
        private List<Owner> Parsing()
        {
            var owner = new Owner();
            owner.Surname = surname.Text;
            owner.Name = name.Text;
            owner.Patronymic = patronymic.Text;
            owner.Address = address.Text;

            return new List<Owner>(new[] { owner });
        }//Parsing


        // Метод проверяет все ли данные введены
        private bool FieldsAreFilled()
        {
            // Имеет ли указанная строка значение null, является ли она пустой строкой или строкой,
            // состоящей только из символов-разделителей.

            if (string.IsNullOrWhiteSpace(surname.Text)
                || string.IsNullOrWhiteSpace(name.Text)
                || string.IsNullOrWhiteSpace(patronymic.Text)
                || string.IsNullOrWhiteSpace(address.Text)) return true;

            return false;
        }//FieldsAreFilled


    }
}
