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
    /// Interaction logic for Check.xaml
    /// </summary>
    public partial class Check : Window
    {
        public Orders order;
        MaintenanceDataContext dc = new MaintenanceDataContext();
        public Check()
        {
            InitializeComponent();
        }

        public Check(Orders order)
        {
            InitializeComponent();
            this.order = order;

            Loaded += Check_Loaded;
        }

        private void Check_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> temp  = dc.OrderServices.Where(os => os.Orders == order)
                                                 .Select(os => os.ServicesInfos.Name)
                                                 .ToList();
            int cost = dc.OrderServices.Sum(os => os.ServicesInfos.Price);

            foreach (var item in temp) service.Text += item + "\n";

            service.Text += "\n\nЦена услуг: " + cost.ToString();
        }
    }
}
