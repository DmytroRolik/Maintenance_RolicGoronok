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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maintenance_RolicGoronok
{
    /// <summary>
    /// Interaction logic for FirstQuery.xaml
    /// </summary>
    public partial class FirstQuery : Page
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        public FirstQuery()
        {
            InitializeComponent();
            info.Text = "Фамилия, имя, отчество и адрес владельца автомобиля с данным номером государственной регистрации";
            listNumber.ItemsSource = dc.Cars.OrderBy(c => c.Number).Select(c=> c.Number);
        }

        // При нажатии на кнопку выполнить выводим в dataGrid Фамилия, имя, отчество и адрес владельца автомобиля с данным  номером государственной регистрации
        private void do_Click(object sender, RoutedEventArgs e)
        {
            Show(numberRegistration.Text);
        }

        // При выборе в listView
        private void listNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Show(listNumber.SelectedItem.ToString());
        }


        // Выводим информации в dataGrid
        private void Show(string Number)
        {
            dg.ItemsSource = dc.Cars.Where(c => c.Number == Number).Select(c => new {
                Фамилия = c.Owner.Surname,
                Имя = c.Owner.Name,
                Отчество = c.Owner.Patronymic,
                Адрес = c.Owner.Address
            });
        }//Show
    }
}
