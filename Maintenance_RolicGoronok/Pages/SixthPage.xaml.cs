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
    /// Interaction logic for SixthPage.xaml
    /// </summary>
    public partial class SixthPage : Page
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        public SixthPage()
        {
            InitializeComponent();
            Loaded += SixthPage_Loaded;
        }

        // При загрузке страницы
        private void SixthPage_Loaded(object sender, RoutedEventArgs e)
        {
            listMarka.ItemsSource = dc.Models;
        }//SixthPage_Loaded

        // Получаем имя марки из listView
        private void listMarka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Show(listMarka.SelectedItem.ToString());
        }//listMarka_SelectionChanged

        //  Получаем имя марки из textBox
        private void do_Click(object sender, RoutedEventArgs e)
        {
            Show(tbMarka.ToString());
        }//do_Click

        // Выводим информации в dataGrid
        private void Show(string Model)
        {
            //dg.ItemsSource = dc.Works.Where(w => w.Bid.Appeal.Car.Model.Name == Model).
            //   GroupBy(w => w.Attire.Malfunction.Name).Select(lg => new
            //   {
            //       Неисправность = lg.Key,
            //       Количество = lg.Count()
            //   });
            dg.ItemsSource = dc.CarMalfunctions.Where(cm => cm.Cars.Models == listMarka.SelectedItem as Models)
                                               .GroupBy(cm => cm.Malfunctions)
                                               .Select(res => new
                                               {
                                                   Неисправность = res.Key,
                                                   Количество = res.Count()
                                               });
        }//Show
    }
}
