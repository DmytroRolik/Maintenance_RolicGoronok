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
           listMalfunction.ItemsSource = malfunction.ItemsSource = dc.Malfunctions.Select(m => m.Name);
            malfunction.SelectedIndex = 0;
        }

  
        // При смене элемента в comboBox
        private void malfunction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Show(malfunction.SelectedItem.ToString());
        }

        // При смене элемента в listMalfunction
        private void listMalfunction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Show(listMalfunction.SelectedItem.ToString());
        }


        // Выводим информации в dataGrid
        private void Show(string Malfunction)
        {
            dg.ItemsSource = dc.Works
                .Where(w => w.Attire.Malfunction.Name == Malfunction)
                .Select(w => new { Фамилия = w.Bid.Appeal.Client.Surname, Имя = w.Bid.Appeal.Client.Name, Отчество = w.Bid.Appeal.Client.Patronymic, Дата = w.Bid.Appeal.dateAppeal });
        }//Show
    }
}
