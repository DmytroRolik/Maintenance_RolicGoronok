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

        private void add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addMarca_Click(object sender, RoutedEventArgs e)
        {

        }
        // При нажатии добавить владельца
        private void addOwner_Click(object sender, RoutedEventArgs e)
        {
           AddNewClient newOwner =  new AddNewClient();
           newOwner.Title = "Добавить нового владельца";
           newOwner.ShowDialog();
            // Обновляем персоны в comboBox owner
            owner.ItemsSource = dc.Owners.Select(p => p.Surname + " " + p.Name[0] + "." + p.Patronymic[0]);
        }
    }
}
