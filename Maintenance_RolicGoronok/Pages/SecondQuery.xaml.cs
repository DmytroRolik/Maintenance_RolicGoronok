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
            listOwner.ItemsSource = dc.Cars.Select(c => c.Persons);
        }

        // При нажатии кнопки Выполнить
        private void do_Click(object sender, RoutedEventArgs e)
        {
            //Show(personSurname.Text);
        }//do_Click



        private void listOwner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Show(listOwner.SelectedItem as Persons);
        }//listOwner_SelectionChanged

        // Выводим информации в dataGrid
        private void Show(Persons owner)
        {
            dg.ItemsSource = dc.Cars
                .Where(c => c.Persons == owner)
                .Select(a => new { Марка = a.Models.Name, Год = a.ProductionYear.Year });
        }//Show

    }
}
