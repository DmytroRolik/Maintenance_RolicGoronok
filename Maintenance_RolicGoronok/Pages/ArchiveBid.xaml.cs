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

        private void ArchiveBid_Loaded(object sender, RoutedEventArgs e)
        {
            lvAppeal.ItemsSource = dc.Bids.Where(b => b.Finish == true).Select(b => new { Номер = b.Id, Клиент = b.Appeal.Client.Surname, Модель = b.Appeal.Car.Model.Name, ГосНомер = b.Appeal.Car.Number, Дата = b.Appeal.dateAppeal.Day + "-" + b.Appeal.dateAppeal.Month + "-" + b.Appeal.dateAppeal.Year });
        }

      

        private void lvAppeal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Regex myReg = new Regex(@"\d+");

            Match match = myReg.Match(lvAppeal.SelectedItem.ToString());

            int.TryParse(match.Value, out id);


            dg.ItemsSource = dc.Works.Where(w => w.BidId == id).Select(w => new { w.Attire.Malfunction.Name, service = w.Attire.ServicesInfo.Name, w.Attire.ServicesInfo.Price, w.Attire.Employee.Surname, w.Bid.FinishDate });
        }
    }
}
