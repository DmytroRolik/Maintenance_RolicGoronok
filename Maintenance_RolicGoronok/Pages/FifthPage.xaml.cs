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
    /// Interaction logic for FifthPage.xaml
    /// </summary>
    public partial class FifthPage : Page
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        public FifthPage()
        {
            InitializeComponent();
            info.Text = "Фамилия, имя, отчество клиентов, сдавших в ремонт автомобили с указанным типом неисправности";
            Loaded += FifthPage_Loaded;
        }

        private void FifthPage_Loaded(object sender, RoutedEventArgs e)
        {
            listMalfunction.ItemsSource = malfunction.ItemsSource = dc.Malfunctions;
            malfunction.SelectedIndex = 0;
        }// FifthPage_Loaded


        // При смене элемента в comboBox
        private void malfunction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Show(malfunction.SelectedItem as Malfunctions);
        }

        // При смене элемента в listMalfunction
        private void listMalfunction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Show(listMalfunction.SelectedItem as Malfunctions);
        }


        // Выводим информации в dataGrid
        private void Show(Malfunctions malfunction)
        {
            dg.ItemsSource = dc.OrderServices
                .Where(os => os.CarMalfunctions.Malfunctions == malfunction)
                .Select(os => new { Фамилия = os.Orders.Persons.Surname, Имя = os.Orders.Persons.Name, Отчество = os.Orders.Persons.Patronymic, Дата = os.Orders.BeginDate.ToShortDateString() });
        }//Show
    }
}
