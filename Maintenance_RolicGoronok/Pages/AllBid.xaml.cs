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
    /// Interaction logic for AllBid.xaml
    /// </summary>
    public partial class AllBid : Page
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        //int id;
        public AllBid()
        {
            InitializeComponent();
            Loaded += AllBid_Loaded;
        }

        //При загрузке
        private void AllBid_Loaded(object sender, RoutedEventArgs e)
        {
            lvAppeal.ItemsSource = dc.Orders.Where(o => !o.IsFinished);
        }//AllBid_Loaded

        // При выборе в listView
        private void lvAppeal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg.ItemsSource = dc.OrderServices.Where(os => os.Orders == lvAppeal.SelectedItem as Orders);
        }//lvAppeal_SelectionChanged


        // При нажатии на кнопку Заявку в архив
        private void btToArchive_Click(object sender, RoutedEventArgs e)
        {
            (lvAppeal.SelectedItem as Orders).IsFinished = true;

            dc.SubmitChanges();

            lvAppeal.ItemsSource = dc.Orders.Where(o => !o.IsFinished);
        }//btToArchive_Click


        // Расчитать счет по ид заявки
        private void btCalculation_Click(object sender, RoutedEventArgs e)
        {
            //GetIdBid();                   // Получить ид выбраной в listView заявки
            new Check(lvAppeal.SelectedItem as Orders).ShowDialog();   // Открываем окно с чеком и передаем ему ид заявки

        }//btCalculation_Click
    }
 }

