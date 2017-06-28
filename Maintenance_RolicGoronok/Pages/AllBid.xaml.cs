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
        int id;
        public AllBid()
        {
            InitializeComponent();
            Loaded += AllBid_Loaded;
        }

        //При загрузке
        private void AllBid_Loaded(object sender, RoutedEventArgs e)
        {
            lvAppeal.ItemsSource = dc.Bids.Where(b=> b.Finish == false).Select(b => new {Номер= b.Id, Клиент = b.Appeal.Client.Surname, Модель = b.Appeal.Car.Model.Name, ГосНомер = b.Appeal.Car.Number, Дата = b.Appeal.dateAppeal.Day+"-"+b.Appeal.dateAppeal.Month+"-"+b.Appeal.dateAppeal.Year });
        }//AllBid_Loaded

        // При выборе в listView
        private void lvAppeal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetIdBid();

            dg.ItemsSource = dc.Works.Where(w => w.BidId == id).Select(w => new { w.Attire.Malfunction.Name, service = w.Attire.ServicesInfo.Name, w.Attire.ServicesInfo.Price, w.Attire.Employee.Surname, w.Bid.FinishDate });
        }//lvAppeal_SelectionChanged


        // При нажатии на кнопку Заявку в архив
        private void btToArchive_Click(object sender, RoutedEventArgs e)
        {
            GetIdBid();                                                 // Получить ид выбраной в listView заявки

            var query = dc.Bids.Where(b=> b.Id == id).Select(b=> b);    // Выбераем в базе записи с ид полученой заявки

            foreach (var item in query) item.Finish = true;             // Помечаем заявку как архивную


            dc.SubmitChanges();                                         // Отправляем изменения в базу

            lvAppeal.SelectionChanged -= lvAppeal_SelectionChanged;     // Отписываемся от события SelectionChanged

            lvAppeal.ItemsSource = dc.Bids.Where(b => b.Finish == false).Select(b => new { Номер = b.Id, Клиент = b.Appeal.Client.Surname, Модель = b.Appeal.Car.Model.Name, ГосНомер = b.Appeal.Car.Number, Дата = b.Appeal.dateAppeal.Day + "-" + b.Appeal.dateAppeal.Month + "-" + b.Appeal.dateAppeal.Year });

            lvAppeal.SelectionChanged += lvAppeal_SelectionChanged;     // Подписываемся на события SelectionChanged
        }//btToArchive_Click


        // Расчитать счет по ид заявки
        private void btCalculation_Click(object sender, RoutedEventArgs e)
        {
            GetIdBid();                   // Получить ид выбраной в listView заявки
            new Check(id).ShowDialog();   // Открываем окно с чеком и передаем ему ид заявки

        }//btCalculation_Click

        // Получить ид выбраной в listView заявки
        private void GetIdBid()
        {
            Regex myReg = new Regex(@"\d+");
            Match match = myReg.Match(lvAppeal.SelectedItem.ToString());
            int.TryParse(match.Value, out id);
        }//GetIdBid
    }
 }

