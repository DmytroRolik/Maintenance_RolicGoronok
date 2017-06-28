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
        public int idBid;
        MaintenanceDataContext dc = new MaintenanceDataContext();
        public Check()
        {
            InitializeComponent();
        }

        public Check(int id)
        {
            InitializeComponent();
            idBid = id;

            Loaded += Check_Loaded;
        }

        private void Check_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> temp  = dc.Works.Where(w => w.BidId == idBid).Select(w=> w.Attire.ServicesInfo.Name).ToList();
            int cost = dc.Works.Where(w => w.BidId == idBid).Sum(w=>w.Attire.ServicesInfo.Price);

            foreach (var item in temp) service.Text += item + "\n";

            service.Text += "\n\nЦена услуг: " + cost.ToString();
        }
    }
}
