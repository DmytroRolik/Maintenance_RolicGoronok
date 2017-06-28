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
    /// Interaction logic for SecondQuery.xaml
    /// </summary>
    public partial class SecondQuery : Page
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        public SecondQuery()
        {
            InitializeComponent();
            info.Text = "Марка и год выпуска автомобиля данного владельца";
            listOwner.ItemsSource = dc.Owners.OrderBy(o => o.Surname).Select(o=> o.Surname+" "+ o.Name[0]+"."+o.Patronymic[0]);
        }

        // При нажатии кнопки Выполнить
        private void do_Click(object sender, RoutedEventArgs e)
        {
            Show(personSurname.Text);
        }//do_Click



        private void listOwner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Show(listOwner.SelectedItem.ToString());
        }//listOwner_SelectionChanged

        // Выводим информации в dataGrid
        private void Show(string Owner)
        {
            dg.ItemsSource = dc.Cars
                .Where(c => c.Owner.Surname + " " + c.Owner.Name[0] + "." + c.Owner.Patronymic[0] == Owner)
                .Select(a => new { Марка = a.Model.Name, Год = a.ProductionYear.Year });
        }//Show

    }
}
