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
    /// Логика взаимодействия для FourthQuery.xaml
    /// </summary>
    public partial class FourthQuery : Page
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        int idCar, idClient;
        List<int> idBid;
        public FourthQuery()
        {
            InitializeComponent();
            Loaded += FourthQuery_Loaded;
        }

        void FourthQuery_Loaded(object sender, RoutedEventArgs e)
        {
            client.ItemsSource = dc.Clients.Select(c => c.Surname + " " + c.Name[0] + "." + c.Patronymic[0]);
        }//FourthQuery_Loaded

        private void client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idClient = dc.Clients.Where(c => c.Surname + " " + c.Name[0] + "." + c.Patronymic[0] == client.SelectedItem.ToString()).Select(c => c.Id).Single();
            car.ItemsSource = dc.Appeals.Where(a => a.ClientId == idClient).GroupBy(a=> a.Car.Number).Select(g => g.Key);
        }//client_SelectionChanged

        private void car_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            idCar = dc.Cars.Where(c => c.Number == car.SelectedItem.ToString()).Select(c => c.Id).Single();
            malfunction.ItemsSource = dc.Works.Where(w => w.Bid.Appeal.CarId == idCar).GroupBy(w => w.Attire.Malfunction.Name).Select(g=> g.Key);
        }//car_SelectionChanged


        private void do_Click(object sender, RoutedEventArgs e)
        {
            if (client.SelectedIndex < 0 || car.SelectedIndex < 0 || malfunction.SelectedIndex < 0) return;

            idBid = dc.Bids.Where(b => b.Appeal.CarId == idCar && b.Appeal.ClientId == idClient).Select(b => b.Id).ToList();
            dg.ItemsSource = dc.Works
                                    .Where(w => idBid.Contains(w.BidId) && w.Attire.Malfunction.Name == malfunction.SelectedItem.ToString())
                                    .Select(w => new { Работник = w.Attire.Employee.Surname, Принято = w.Bid.Appeal.dateAppeal.ToShortDateString(), Здано = w.Bid.FinishDate.ToShortDateString() });
        }//do_Click





    }
}
