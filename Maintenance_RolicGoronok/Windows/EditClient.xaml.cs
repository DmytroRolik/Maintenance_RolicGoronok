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
        MaintenanceDataContext dc;
        Persons SelectedClient { get; set; }
        public EditClient()
        {
            InitializeComponent();
            Loaded += EditClient_Loaded;
        }

        private void EditClient_Loaded(object sender, RoutedEventArgs e)
        {
            using (dc = new MaintenanceDataContext()) {
                lvClient.ItemsSource = dc.Persons;
            }// using
        }// EditClient_Loaded

        // При выборе в listView lvClient заполняем окно из базы
        private void lvClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedClient = lvClient.SelectedItem as Persons;

            licen.Text = SelectedClient.License;
            address.Text = SelectedClient.Address;
            passport.Text = SelectedClient.Passport;
        }// lvClient_SelectionChanged


        // При нажатии кнопки изменить
        private void change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeClientInfo();
                using (dc = new MaintenanceDataContext())
                    dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }// try-catch
           
        }//change_Click

        // применить изменения для текущего клиента
        public void ChangeClientInfo()
        {
            SelectedClient.License = licen.Text;
            SelectedClient.Address = address.Text;
            SelectedClient.Passport = passport.Text;
        }// ChangeClientInfo
    }
}
