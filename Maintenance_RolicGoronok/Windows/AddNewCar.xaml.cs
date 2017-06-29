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
        MaintenanceDataContext dc = new MaintenanceDataContext();
        int idmodel, idowner;
        public AddNewCar()
        {
            InitializeComponent();
            Loaded += AddNewCar_Loaded;
        }

        // При загрузке окна 
        void AddNewCar_Loaded(object sender, RoutedEventArgs e)
        {
            // Все модели в comboBox model
            model.ItemsSource = dc.Models.Select(m => m.Name);
            // Все персоны в comboBox owner
            owner.ItemsSource = dc.Owners.Select(p => p.Surname + " " + p.Name[0] + "." + p.Patronymic[0]);
        }


        // При нажатии добавить владельца
        private void addOwner_Click(object sender, RoutedEventArgs e)
        {
            new AddNewOwner().ShowDialog();

            // Обновляем персоны в comboBox owner
            owner.ItemsSource = dc.Owners.Select(p => p.Surname + " " + p.Name[0] + "." + p.Patronymic[0]);
        }

        private void addModel_Click(object sender, RoutedEventArgs e)
        {
            new AddModel().ShowDialog();
            // Все модели в comboBox model
            model.ItemsSource = dc.Models.Select(m => m.Name);
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            //проверяет все ли данные введены
            if (FieldsAreFilled()) { MessageBox.Show("Не все данные введены"); return; }

            dc.Cars.InsertAllOnSubmit(Parsing());
            SubmitChanges("Авто добавлено");
        }


        // Метод Parsing извлекаем данные из окна и возвращаем их в виде объкта Car
        private List<Car> Parsing()
        {
            var car = new Car();
            car.Color = color.Text;
            car.Number = number.Text;
            car.ModelId = idmodel;
            car.OwnerId = idowner;
            car.ProductionYear = date.SelectedDate.Value;

            return new List<Car>(new[] { car });
        }//Parsing


        // Метод проверяет все ли данные введены
        private bool FieldsAreFilled()
        {
            // Имеет ли указанная строка значение null, является ли она пустой строкой или строкой,
            // состоящей только из символов-разделителей.

            return string.IsNullOrWhiteSpace(color.Text)
                || string.IsNullOrWhiteSpace(number.Text) || model.SelectedItem == null || owner.SelectedItem == null|| 
                date.SelectedDate == null;

        }//FieldsAreFilled

        // Получаем ид выбраной в comboBox модели
        private void model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idmodel = dc.Models.Where(m => m.Name == model.SelectedItem.ToString()).Select(m => m.Id).Single();
        }//model_SelectionChanged
        // Получаем ид выбраного в comboBox владельца
        private void owner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idowner = dc.Owners.Where(o => o.Surname + " " + o.Name[0] + "." + o.Patronymic[0] == owner.SelectedItem.ToString()).Select(o => o.Id).Single();
        }//owner_SelectionChanged

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
