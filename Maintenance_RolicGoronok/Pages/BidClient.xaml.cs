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
    /// Interaction logic for Bid.xaml
    /// </summary>
    public partial class BidClient : Page
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        List<Garbs> listGarbs = new List<Garbs>();                  // Колекция нарядов
        int AppealNewId, BidNewId;                                  // Поля для хранения  


        public BidClient()
        {
            InitializeComponent();
            Loaded += Bid_Loaded;
        }

        // При загрузке Page
        private void Bid_Loaded(object sender, RoutedEventArgs e)
        {
            // Загружаем в ComboBox client коллекцию клиентов
            client.ItemsSource = dc.Persons.Select(o => new { Фамилия = o.Surname + " " + o.Name[0] + "." + o.Patronymic[0] }).Select(p => p.Фамилия);

            // Загружаем в ComboBox avto коллекцию номеров машин
            avto.ItemsSource = dc.Cars.Select(c => c.Number);

            // Загружаем в ListView lvMalfun коллекцию неисправностей
            lvMalfun.ItemsSource = dc.Malfunctions.OrderBy(m => m.Name).Select(m => m.Name);

            // Загружаем в ListView lvEmpl коллекцию работников
            lvEmpl.ItemsSource = dc.Employees.Select(em => em.Persons.Surname + " " + em.Persons.Name[0] + "." + em.Persons.Patronymic[0]);

            // Загружаем в ListView lvServices коллекцию услуг
            lvServices.ItemsSource = dc.ServicesInfos.Select(s => s.Name);
        }//Bid_Loaded

        // При нажатии на кнопку новый клиент
        private void addClient_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно для создания клиента
            new AddNewClient().ShowDialog();
            // Обновляем в ComboBox client записи о клиентах
            client.ItemsSource = dc.Persons.Select(p => new { Фамилия = p.Surname + " " + p.Name[0] + "." + p.Patronymic[0] }).OrderBy(p => p.Фамилия).Select(p => p.Фамилия);
        }//addClient_Click

        // При нажатии на кнопку новый авто
        private void addCar_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно для создания клиента
            new AddNewCar().ShowDialog();
            // Обновляем в ComboBox avto записи о машинах
            avto.ItemsSource = dc.Cars.Select(c => c.Number);
        }

        // При выборе Номера автомобиля в comboBox avto выводим краткую информацию об авто
        private void avto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgAvto.ItemsSource = dc.Cars.Where(c => c.Number == avto.SelectedItem.ToString()).Select(c => new { Модель = c.Models.Name, Цвет = c.Color, Владелец = c.Persons.Surname });
        }//avto_SelectionChanged


        // При выборе Клиента в comboBox client  выводим краткую информацию о клиенте
        private void client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgClient.ItemsSource = dc.Persons.Where(p => p.Surname + " " + p.Name[0] + "." + p.Patronymic[0] == client.SelectedItem.ToString()).Select(p => new { Паспорт = p.Passport, Рожден = p.BirthDate.ToShortDateString() });
        }//client_SelectionChanged


        // Собираем данные о наряде в коллекцию
        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsAreFilledForAttire()) { MessageBox.Show("Не все данные выбраны"); return; }
            // Получаем Ид Неисправности, Ид Работника, Ид Услуги
            int MalfunId = dc.Malfunctions.Where(m => m.Name == lvMalfun.SelectedItem.ToString()).Select(m => m.Id).Single();
            int EmployeeId = dc.Employees.Where(em => em.Persons.Surname + " " + em.Persons.Name[0] + "." + em.Persons.Patronymic[0] == lvEmpl.SelectedItem.ToString()).Select(em => em.Id).Single();
            int ServecId = dc.ServicesInfos.Where(s => s.Name == lvServices.SelectedItem.ToString()).Select(s => s.Id).Single();

            // Создаем объект наряд
            Garbs garb = new Garbs(MalfunId, lvMalfun.SelectedItem.ToString(), EmployeeId, lvEmpl.SelectedItem.ToString(), ServecId, lvServices.SelectedItem.ToString());

            // Добавляем объект в коллекцию нарядов
            listGarbs.Add(garb);

            dgattire.ItemsSource = null;
            // Загружаем коллекцию в datagrid с Нарядами
            dgattire.ItemsSource = listGarbs;
        }//add_Click


        // При нажатии Добавить заявку
        private void addBids_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsAreFilledForBid()) { MessageBox.Show("Не все данные о заявке введены"); return; }
            // Обращения  на станцию техобслуживания 
            AddAppeal();
            // Заявка  на устранения неисправности и услуги  
            AddBids();
            // Метод заполняет Наряд и добавляет его в работы
            AddAttire();
            MessageBox.Show("Заявка сформирована");

        }//addBids_Click


        // Метод добавляет обращения  на станцию техобслуживания 
        private void AddAppeal()
        {
            Appeal appeal = new Appeal();
            appeal.dateAppeal = dateFrom.SelectedDate.Value;
            appeal.CarId = dc.Cars.Where(c => c.Number == avto.SelectedItem.ToString()).Select(c => c.Id).Single();
            appeal.ClientId = dc.Clients.Where(cl => cl.Surname + " " + cl.Name[0] + "." + cl.Patronymic[0] == client.SelectedItem.ToString()).Select(cl => cl.Id).Single();

            List<Appeal> listAppeal = new List<Appeal>();
            listAppeal.Add(appeal);

            // Добавляем запись в базу
            dc.GetTable<Appeal>().InsertAllOnSubmit(listAppeal);
            SubmitChanges();
            // Получаем Ид новой записи
            AppealNewId = listAppeal[0].Id;
        }//AddAppeal


        // Метод добовляет заявка  на устранения неисправности и услуги  
        private void AddBids()
        {
            Bid bid = new Bid();
            bid.AppealId = AppealNewId;
            bid.FinishDate = dateTo.SelectedDate.Value;

            List<Bid> listBid = new List<Bid>();

            listBid.Add(bid);

            // Добавляем запись в базу
            dc.GetTable<Bid>().InsertAllOnSubmit(listBid);
            SubmitChanges();

            // Получаем Ид новой записи
            BidNewId = listBid[0].Id;
        }// AddBids


        // Метод заполняет Наряд и добавляет его в работы
        private void AddAttire()
        {
            for (int i = 0; i < listGarbs.Count; i++)
            {
                Attire attire = new Attire();
                attire.MalfunctionId = listGarbs[i].MalfunId;
                attire.ServicesInfoId = listGarbs[i].ServecId;
                attire.EmployeeId = listGarbs[i].EmployeeId;

                List<Attire> listAttire = new List<Attire>();
                listAttire.Add(attire);

                // Добавляем запись в базу
                dc.GetTable<Attire>().InsertAllOnSubmit(listAttire);
                SubmitChanges();

                // Получаем Ид новой записи
                int AttireNewId = listAttire[0].Id;

                AddWork(AttireNewId);
            }
        }//AddAttire

        // Метод  добавляет  работу
        private void AddWork(int AttireNewId)
        {
            Work work = new Work();
            work.AttireId = AttireNewId;
            work.BidId = BidNewId;

            List<Work> listWork = new List<Work>();

            listWork.Add(work);

            // Добавляем запись в базу
            dc.GetTable<Work>().InsertAllOnSubmit(listWork);
            SubmitChanges();
        }//AddWork

        private void SubmitChanges()
        {
            try
            {
                dc.SubmitChanges();
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }//try-catch
        }// SubmitChanges


        // Метод проверяет все ли данные для наряда введены
        private bool FieldsAreNotFilledForAttire()
        {
            return lvEmpl.SelectedItem == null ||
                   lvMalfun.SelectedItem == null ||
                   lvServices.SelectedItem == null;
        }//FieldsAreFilled

        // Метод проверяет все ли данные для наряда введены
        private bool FieldsAreFilledForBid()
        {
            return listGarbs.Count == 0 ||
                   client.SelectedItem == null ||
                   avto.SelectedItem == null ||
                   dateFrom.SelectedDate == null ||
                   dateTo.SelectedDate == null;
        }//FieldsAreFilled
    }
}
