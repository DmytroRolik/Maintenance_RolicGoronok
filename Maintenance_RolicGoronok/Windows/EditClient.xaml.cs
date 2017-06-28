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
using System.Windows.Shapes;

namespace Maintenance_RolicGoronok
{
    /// <summary>
    /// Interaction logic for EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        int idclient;
        public EditClient()
        {
            InitializeComponent();
            Loaded += EditClient_Loaded;
        }

        private void EditClient_Loaded(object sender, RoutedEventArgs e)
        {
           lvClient.ItemsSource = dc.Clients.Select(p => new { Фамилия = p.Surname + " " + p.Name[0] + "." + p.Patronymic[0] }).Select(p => p.Фамилия);
        }

        // При выборе в listView lvClient заполняем окно из базы
        private void lvClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client client = dc.Clients.Where(c => c.Surname + " " + c.Name[0] + "." + c.Patronymic[0] == lvClient.SelectedItem.ToString()).Select(c => c).Single();

            licen.Text = client.License;
            phone.Text = client.Phone;
            address.Text = client.Address;
            passport.Text = client.Passport;
            idclient = client.Id;
        }

      
        // При нажатии кнопки изменить
        private void change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChengeInfo(idclient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }//change_Click

        // Изменяем информацию о клиенте из окна
        private void ChengeInfo(int id)
        {
            var query = from cl in dc.Clients where cl.Id == id select cl;

            foreach (var item in query)
            {
                item.License = licen.Text;
                item.Phone = phone.Text;
                item.Address = address.Text;
                item.Passport = passport.Text;
            }
            dc.SubmitChanges();
        }//ChengeInfo
    }
}
