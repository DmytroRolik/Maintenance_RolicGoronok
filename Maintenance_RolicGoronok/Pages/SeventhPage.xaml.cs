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
    /// Interaction logic for SeventhPage.xaml
    /// </summary>
    public partial class SeventhPage : Page
    {
        MaintenanceDataContext dc;
        public SeventhPage()
        {
            InitializeComponent();
            dc = new MaintenanceDataContext();

            dgvEmployees.ItemsSource = dc.Specialities;
        }
    }
}
