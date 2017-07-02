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
    /// Логика взаимодействия для MalfunctionsPage.xaml
    /// </summary>
    public partial class MalfunctionsPage : Page
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        public MalfunctionsPage()
        {
            InitializeComponent();
            info.Text = "Перечень устраненных неисправностей в автомобиле данного владельца";
            listOwner.ItemsSource = dc.Persons.OrderBy(o => o.Surname);
        }

        // При нажатии кнопки выполнить
        private void do_Click(object sender, RoutedEventArgs e)
        {
            //Show(personSurname.Text);
        }

        // Получаем Ф.И.О владельца из listView
        private void listOwner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Show(listOwner.SelectedItem as Persons);
        }

        // Выводим информации в dataGrid
        private void Show(Persons Owner)
        {
            //dg.ItemsSource = dc.Works
            //    .Where(w => w.Bid.Appeal.Car.Owner.Surname+" "+ w.Bid.Appeal.Car.Owner.Name[0]+"."+ w.Bid.Appeal.Car.Owner.Patronymic[0] == Owner && w.Bid.Finish == true)
            //    .Select(w => new { Дата = w.Bid.FinishDate.Date.ToShortDateString(), Неисправность = w.Attire.Malfunction.Name });

            dg.ItemsSource = dc.OrderServices.Where(os => os.Orders.Cars.Persons == listOwner.SelectedItem as Persons && os.Orders.IsFinished)
                                             .Select(os => new { Дата = os.Orders.FinishDate.ToShortDateString(), Неисправность = os.CarMalfunctions.Malfunctions });
        }//Show
    }
}
