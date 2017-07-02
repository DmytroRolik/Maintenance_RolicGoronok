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
        Orders curOrder;
        MaintenanceDataContext dc = new MaintenanceDataContext();
        int totalPrice = 0; // общая стоимость заказа

        public Check()
        {
            InitializeComponent();
        }

        public Check(Orders order)
        {
            InitializeComponent();
            curOrder = order;

            Loaded += Check_Loaded;
        }

        private void Check_Loaded(object sender, RoutedEventArgs e)
        {
            // добавить заголовок документа
            pHeader.Inlines.Add(new Run($"Заказ №{curOrder.Id}"));

            // Добавить строки к таблице
            InitTableRows();

            // добавить общую стоимость
            pTotal.Inlines.Add(new Run($"Итого: {totalPrice} руб"));
        }// Check_Loaded

        // добавляет строки к таблице
        private void InitTableRows()
        {
            // добавить каждую неисправниость и информацию к ней
            foreach (OrderServices os in curOrder.OrderServices) {
                tableRows.Rows.Add(GetRow(os));
            }// foreach
        }// InitTableRows


        private TableRow GetRow(OrderServices os)
        {
            string malfunction; // неисправность
            string service;     // услуга
            int price;          // стоимость

            malfunction = os.CarMalfunctions.Malfunctions.Name;
            service = os.ServicesInfos.Name;
            price = os.ServicesInfos.Price;

            // создание контейнеров для помещения данных в строку таблицы (TableRow)
            Paragraph pMalfunction = new Paragraph();      // неисправность
            Paragraph pService = new Paragraph();          // услуга
            Paragraph pPrice = new Paragraph();            // стоимость

            pMalfunction.Inlines.Add(new Run(malfunction));                       // добавление текста в параграф
            TableCell malfunctionName = new TableCell(pMalfunction);       // добаление параграфа в ячейку строки таблицы
            malfunctionName.BorderBrush = Brushes.Black;            // задание границы таблицы
            malfunctionName.BorderThickness = new Thickness(1d);

            pService.Inlines.Add(new Run(service));              // добавление текста в параграф
            TableCell malfunctionAmount = new TableCell(pService);   // добаление параграфа в ячейку строки таблицы
            malfunctionAmount.BorderBrush = Brushes.Black;          // задание границы таблицы
            malfunctionAmount.BorderThickness = new Thickness(1d);

            pPrice.Inlines.Add(new Run($"{price}"));              // добавление текста в параграф
            TableCell malfunctionIncome = new TableCell(pPrice);   // добаление параграфа в ячейку строки таблицы
            malfunctionIncome.BorderBrush = Brushes.Black;          // задание границы таблицы
            malfunctionIncome.BorderThickness = new Thickness(1d);

            // создание стоки таблицы (TableRow) и добавление в нее
            // ячеек (TableCell)
            TableRow t = new TableRow();
            t.Cells.Add(malfunctionName);
            t.Cells.Add(malfunctionAmount);
            t.Cells.Add(malfunctionIncome);

            CalculateTotalPrice(price); // добавить стоимость текущей услуги к общей сумме

            // вернуть сформированную строку
            return t;
        }// GetRow


        // расчет общей стоимости заказа
        private void CalculateTotalPrice(int price)
        {
            totalPrice += price;
        }// CalculateTotalPrice


        // печать содержимого документа 
        private void tlbtnPrint_Click(object sender, RoutedEventArgs e)
        {
            // создать стандартный диалог печати
            PrintDialog pd = new PrintDialog();

            // если была выбрана "Печать"
            if (pd.ShowDialog() == true) {

                // получить текущий документ
                FlowDocument doc = fdReport;

                // Сохранить все имеющиеся настройки
                double pageHeight = doc.PageHeight;
                double pageWidth = doc.PageWidth;
                Thickness pagePadding = doc.PagePadding;
                double columnGap = doc.ColumnGap;
                double columnWidth = doc.ColumnWidth;

                // Привести страницу FlowDocument в соответствие с печатной страницей
                doc.PageHeight = pd.PrintableAreaHeight;
                doc.PageWidth = pd.PrintableAreaWidth;
                doc.PagePadding = new Thickness(24d);       // !!!важно!!! - задает отступ от границ страницы (по умолчанию без отступа)
                doc.ColumnGap = 25;
                doc.ColumnWidth = (doc.PageWidth - doc.ColumnGap        // задает ширину печати 1 столбца (можно разбить на несколько)
                    - doc.PagePadding.Left - doc.PagePadding.Right);

                // печать документа
                pd.PrintDocument(fdsvMain.Document.DocumentPaginator, "Чек");

                // Восстановить старые настройки
                doc.PageHeight = pageHeight;
                doc.PageWidth = pageWidth;
                doc.PagePadding = pagePadding;
                doc.ColumnGap = columnGap;
                doc.ColumnWidth = columnWidth;
            }// if
        }// tlbtnPrint_Click
    }
}
