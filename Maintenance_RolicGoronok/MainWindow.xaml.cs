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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<TabItem> _tabItems;           // Коллекция вкладок
        public MainWindow()
        {
            InitializeComponent();
            // Инициализировать массив tabItem
            _tabItems = new List<TabItem>();

            Loaded += MainWindow_Loaded;
            exit.Click += (e, s) => Application.Current.Shutdown();
        }//MainWindow


        // При загрузке главного окна
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Создаем и заполняем базу и таблицы
            CreateAndInsertTable createInsertTable = new CreateAndInsertTable();
            createInsertTable.StartConnection();

            editClient.Click += (s, en) => new EditClient().ShowDialog();
            newEmp.Click += (s, en) => new AddEmployee().ShowDialog();
        }//MainWindow_Loaded

        // При нажатии на кнопку закрыть вкладку
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // Получить имя удаляемой вкладки
            string tabName = (sender as Button).CommandParameter.ToString();

            // С помощью имени получить объект TabItem из TabControl с именем tabDynamic
            TabItem tabItem = tabDynamic.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();

            if (tabItem != null) {
                if (MessageBox.Show(string.Format("Вы уверены что хотите закрыть вкладку {0} ?", tabItem.Header), "Закрытие вкладки", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                    tabDynamic.DataContext = null;                            // Отсоединяемся
                    _tabItems.Remove(tabItem);                                // Удалаем  TabItem из коллекции
                    tabDynamic.DataContext = _tabItems;                       // Присоединяемся

                    // Если есть еще другие вкладки переходим на последнюю
                    if (_tabItems.Count > 0) tabDynamic.SelectedItem = _tabItems[_tabItems.Count - 1];
                }
            }
        }//Close_Click



        private void queryButton_click(object sender, RoutedEventArgs e)
        {
            // Получаем контент нажатой кнопки
            string buttonName = ((Button)sender).Name.ToString();

            // Количество вкладок с именем name
            int count = _tabItems.Where(t => buttonName.Contains(t.Name)).Count();

            // Если вкладка с таким именем есть
            if (count > 0) return;

            // Создаем вкладку
            TabItem tabItem = CreateTabItem(buttonName);


            Frame frame = new Frame();
            switch (buttonName)
            {
                case "tlbtnOwner": frame.NavigationService.Navigate(new Uri("Pages/FirstQuery.xaml", UriKind.Relative)); break;
                case "tlbtnCar": frame.NavigationService.Navigate(new Uri("Pages/SecondQuery.xaml", UriKind.Relative)); break;
                case "tlbtnGarbs": frame.NavigationService.Navigate(new Uri("Pages/BidClient.xaml", UriKind.Relative)); break;
                case "tlbtnMalfunction": frame.NavigationService.Navigate(new Uri("Pages/MalfunctionsPage.xaml", UriKind.Relative)); break;
                case "tlbtnEmployee": frame.NavigationService.Navigate(new Uri("Pages/FourthQuery.xaml", UriKind.Relative)); break;
                case "tlbtnBreakages": frame.NavigationService.Navigate(new Uri("Pages/FifthPage.xaml", UriKind.Relative)); break;
                case "tlbtnCommon": frame.NavigationService.Navigate(new Uri("Pages/SixthPage.xaml", UriKind.Relative)); break;
                case "tlbtnBids": frame.NavigationService.Navigate(new Uri("AllBid.xaml", UriKind.Relative)); break;
                case "tlbtnWorkersAmount": frame.NavigationService.Navigate(new Uri("Pages/SeventhPage.xaml", UriKind.Relative)); break;
            }

            tabDynamic.DataContext = null;
            tabItem.Content = frame;                         // установка содержимого вкладки
            count = _tabItems.Count;
            _tabItems.Insert(count, tabItem);                // Вставка tabItem в коллекцию _tabItems
            tabDynamic.DataContext = _tabItems;              // Привязка к коллекции вкладок
            tabDynamic.SelectedItem = tabItem;               // Выбераем добавленную вкладку
        }//queryButton_click


        // Создаем вкладку
        private TabItem CreateTabItem(string name)
        {
            // создать TabItem
            TabItem tabItem = new TabItem();
            tabItem.Header = string.Format(name);
            tabItem.Name = string.Format(name);
            tabItem.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;
            return tabItem;
        }//CreateTabItem
    }
}
