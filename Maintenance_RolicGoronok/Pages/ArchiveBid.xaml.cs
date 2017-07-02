using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ArchiveBid.xaml
    /// </summary>
    public partial class ArchiveBid : Page
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        int id;
        public ArchiveBid()
        {
            InitializeComponent();
            Loaded += ArchiveBid_Loaded;
        }

        //При загрузке
        private void ArchiveBid_Loaded(object sender, RoutedEventArgs e)
        {
            lvAppeal.ItemsSource = dc.Orders.Where(o => o.IsFinished);
        }//AllBid_Loaded

        // При выборе в listView
        private void lvAppeal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg.ItemsSource = dc.Executors.Where(ex => ex.OrderServices.Orders == lvAppeal.SelectedItem as Orders);
        }//lvAppeal_SelectionChanged
    }
}
