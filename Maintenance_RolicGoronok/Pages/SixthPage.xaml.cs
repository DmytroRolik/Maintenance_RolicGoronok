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

        private void SixthPage_Loaded(object sender, RoutedEventArgs e)
        {
            listMarka.ItemsSource = dc.Models.Select(m => m.Name);
        }

        private void listMarka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg.ItemsSource = dc.Works.Where(w => w.Bid.Appeal.Car.Model.Name == listMarka.SelectedItem.ToString()).
                GroupBy(w => w.Attire.Malfunction.Name).Select(lg => new
                {
                    Неисправность = lg.Key,
                    Количество = lg.Count()
                });
        }

        private void do_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
