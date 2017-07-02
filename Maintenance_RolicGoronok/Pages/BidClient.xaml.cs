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

        Orders curOrder = new Orders();
        List<CarMalfunctions> carMalfunctions = new List<CarMalfunctions>();
        List<OrderServices> services = new List<OrderServices>();
        List<Executors> curExecutors = new List<Executors>();

        public BidClient()
        {
            InitializeComponent();
            Loaded += Bid_Loaded;
        }

        // При загрузке Page
        private void Bid_Loaded(object sender, RoutedEventArgs e)
        {
            // Загружаем в ComboBox client коллекцию клиентов
            client.ItemsSource = dc.Persons;

            // Загружаем в ComboBox avto коллекцию номеров машин
            avto.ItemsSource = dc.Cars;

            // Загружаем в ListView lvMalfun коллекцию неисправностей
            lvMalfun.ItemsSource = dc.Malfunctions.OrderBy(m => m.Name);

            // Загружаем в ListView lvEmpl коллекцию работников
            lvEmpl.ItemsSource = dc.Employees;

            // Загружаем в ListView lvServices коллекцию услуг
            lvServices.ItemsSource = dc.ServicesInfos;

            curOrder.IsFinished = false;
        }//Bid_Loaded

        // При нажатии на кнопку новый клиент
        private void addClient_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно для создания клиента
            new AddNewClient().ShowDialog();
            // Обновляем в ComboBox client записи о клиентах
            client.ItemsSource = dc.Persons;
        }//addClient_Click

        // При нажатии на кнопку новый авто
        private void addCar_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно для создания клиента
            new AddNewCar().ShowDialog();
            // Обновляем в ComboBox avto записи о машинах
            avto.ItemsSource = dc.Cars;
        }

        // При выборе Номера автомобиля в comboBox avto выводим краткую информацию об авто
        private void avto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgAvto.ItemsSource = dc.Cars.Where(c => c == avto.SelectedItem as Cars)
                                        .Select(c => new { Модель = c.Models.Name, Цвет = c.Color, Владелец = c.Persons });

            curOrder.Cars = avto.SelectedItem as Cars;
        }//avto_SelectionChanged


        // При выборе Клиента в comboBox client  выводим краткую информацию о клиенте
        private void client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgClient.ItemsSource = dc.Persons.Where(p => p == client.SelectedItem as Persons)
                                             .Select(p => new { Паспорт = p.Passport, Рожден = p.ShortBirthDate });

            curOrder.Persons = client.SelectedItem as Persons;
        }//client_SelectionChanged


        // Собираем данные о наряде в коллекцию
        private void addService_Click(object sender, RoutedEventArgs e)
        {
            // проверка были ли введены данные
            if (FieldsAreNotFilledForOrder()) {
                MessageBox.Show("Не все данные для заказа были введены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }// if

            if (FieldsAreNotFilledForServices()) {
                MessageBox.Show("Не все данные по услугам были введены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }// if

            // добавление данных
            CarMalfunctions curCarMalfunction = GetCarMalfunction();
            OrderServices curService = GetOrderService(curCarMalfunction);

            carMalfunctions.Add(curCarMalfunction);
            services.Add(curService);
            curExecutors.AddRange(GetExecutors(curService));

            SetItemSourceForDgService();
        }//add_Click


        // При нажатии Добавить заявку
        private void addOrder_Click(object sender, RoutedEventArgs e)
        {
            try {
                dc.Executors.InsertAllOnSubmit(curExecutors);
                dc.SubmitChanges();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }// try-catch
        }//addBids_Click


        // Метод проверяет все ли данные для наряда введены
        private bool FieldsAreNotFilledForServices()
        {
            return lvEmpl.SelectedItem == null ||
                   lvMalfun.SelectedItem == null ||
                   lvServices.SelectedItem == null;
        }//FieldsAreFilled

        // Метод проверяет все ли данные для наряда введены
        private bool FieldsAreNotFilledForOrder()
        {
            return //listGarbs.Count == 0 ||
                   client.SelectedItem == null ||
                   avto.SelectedItem == null ||
                   dateFrom.SelectedDate == null ||
                   dateTo.SelectedDate == null;
        }//FieldsAreFilled

        private void dateFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            curOrder.BeginDate = (sender as DatePicker).SelectedDate.Value;
        }// dateFrom_SelectedDateChanged

        private void dateTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            curOrder.FinishDate = (sender as DatePicker).SelectedDate.Value;
        }// dateTo_SelectedDateChanged

        private CarMalfunctions GetCarMalfunction()
        {
            return new CarMalfunctions { Cars = curOrder.Cars, Malfunctions = lvMalfun.SelectedItem as Malfunctions };
        }// GetCarMalfunction

        private OrderServices GetOrderService(CarMalfunctions curMalfunction)
        {
            return new OrderServices { Orders = curOrder, CarMalfunctions = curMalfunction, ServicesInfos = lvServices.SelectedItem as ServicesInfos };
        }// GetOrderService

        private List<Executors> GetExecutors(OrderServices curService)
        {
            List<Executors> executors = new List<Executors>();

            foreach(Employees emp in lvEmpl.SelectedItems) {
                executors.Add(new Executors { Employees = emp, OrderServices = curService });
            }// foreach

            return executors;
        }// GetExecutors

        private void SetItemSourceForDgService()
        {
            if (dgService.ItemsSource != null)
                dgService.ItemsSource = null;

            dgService.ItemsSource = curExecutors;
        }// SetItemSourceForDgService
    }
}
