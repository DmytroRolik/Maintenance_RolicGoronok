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
            client.ItemsSource = dc.Persons.Where(p => p.Cars != null);
        }//FourthQuery_Loaded

        private void client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (car.SelectedItem != null)
                ResetSelection();

            curClient = client.SelectedItem as Persons;
            car.ItemsSource = dc.Cars.Where(c => c.Persons == curClient);
        }//client_SelectionChanged

        private void car_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curCar = car.SelectedItem as Cars;
            malfunction.ItemsSource = dc.CarMalfunctions.Where(cm => cm.Cars == curCar).Select(cm => cm.Malfunctions);
        }//car_SelectionChanged

        private void malfunction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curMalfunnction = malfunction.SelectedItem as Malfunctions;
        }// malfunction_SelectionChanged

        private void do_Click(object sender, RoutedEventArgs e)
        {
            if (client.SelectedIndex < 0 || car.SelectedIndex < 0 || malfunction.SelectedIndex < 0) return;

            dg.ItemsSource = dc.Executors.Where(ex => ex.OrderServices.CarMalfunctions.Malfunctions == curMalfunnction 
                                                   && ex.OrderServices.CarMalfunctions.Cars == curCar)
                                         .Select(ex => new {
                                             Работник = ex.Employees,
                                             Принято = ex.OrderServices.Orders.ShortBeginDate,
                                             Сдано = ex.OrderServices.Orders.ShortFinishDate
                                         });
        }//do_Click

        private void ResetSelection()
        {
            car.SelectedItem = null;
            malfunction.SelectedItem = null;
        }// ResetSelection
    }
}
