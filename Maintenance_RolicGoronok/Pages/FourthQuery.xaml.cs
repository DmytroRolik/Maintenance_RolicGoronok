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
        Persons curClient;
        Cars curCar;
        Malfunctions curMalfunnction;

        public FourthQuery()
        {
            InitializeComponent();
            Loaded += FourthQuery_Loaded;
        }

        void FourthQuery_Loaded(object sender, RoutedEventArgs e)
        {
            info.Text = "Фамилия, имя, отчество работника станции, устранявшего данную неисправность в автомобиле данного клиента, и время ее устранения";
            client.ItemsSource = dc.Persons.Select(c => c.Surname + " " + c.Name[0] + "." + c.Patronymic[0]);
        }//FourthQuery_Loaded

        private void client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curClient = dc.Persons.Single(c => c.Surname + " " + c.Name[0] + "." + c.Patronymic[0] == client.SelectedItem.ToString());
            car.ItemsSource = dc.Cars.Where(c => c.Persons == curClient).Select(c => c.Number);
        }//client_SelectionChanged

        private void car_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curCar = dc.Cars.Single(c => c.Number == car.SelectedItem.ToString());
            malfunction.ItemsSource = dc.CarMalfunctions.Where(cm => cm.Cars == curCar).Select(cm => cm.Malfunctions);
        }//car_SelectionChanged

        private void malfunction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curMalfunnction = dc.Malfunctions.Single(m => m.Name == malfunction.SelectedItem.ToString());
        }// malfunction_SelectionChanged

        private void do_Click(object sender, RoutedEventArgs e)
        {
            if (client.SelectedIndex < 0 || car.SelectedIndex < 0 || malfunction.SelectedIndex < 0) return;

            // idBid = dc.Bids.Where(b => b.Appeal.CarId == idCar && b.Appeal.ClientId == idClient).Select(b => b.Id).ToList();
            //dg.ItemsSource = dc.Works
            //                        .Where(w => idBid.Contains(w.BidId) && w.Attire.Malfunction.Name == malfunction.SelectedItem.ToString())
            //                        .Select(w => new { Работник = w.Attire.Employee.Surname, Принято = w.Bid.Appeal.dateAppeal.ToShortDateString(), Здано = w.Bid.FinishDate.ToShortDateString() });
            dg.ItemsSource = dc.OrderServices.Where(os => os.CarMalfunctions.Malfunctions == curMalfunnction && os.CarMalfunctions.Cars == curCar)
                                             .Select(os => new { Работник = os.Executors, Принято = os.Orders.BeginDate.ToShortDateString(), Здано = os.Orders.FinishDate.ToShortDateString() });
        }//do_Click

        
    }
}
