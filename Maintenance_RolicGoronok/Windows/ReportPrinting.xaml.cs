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

namespace Maintenance_RolicGoronok.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReportPrinting.xaml
    /// </summary>
    public partial class ReportPrinting : Window
    {
        // дата начала выборки
        DateTime minDate = DateTime.Today.AddMonths(-1);

        public ReportPrinting()
        {
            InitializeComponent();

            // добавить заголовок документа
            pHeader.Inlines.Add(new Run($"Отчет за период с {minDate.ToShortDateString()} по {DateTime.Today.ToShortDateString()}"));

            // Добавить строки к таблице
            InitTableRows();
        }

        // добавляет строки к таблице
        private void InitTableRows()
        {
            // создать датаконтекст для выполнения операций
            using (MaintenanceDataContext dc = new MaintenanceDataContext()) {

                // добавить каждую неисправниость и информацию к ней
                foreach (Malfunctions m in dc.Malfunctions) {
                    tableRows.Rows.Add(GetRow(m));
                }// foreach
            }// using
        }// InitTableRows

        private TableRow GetRow(Malfunctions m)
        {
            string name;
            int amount;
            int income;

            using (MaintenanceDataContext dc = new MaintenanceDataContext()) {
                name = m.Name;                                                                 // имя неисправности
                amount = dc.OrderServices.Count(os => os.CarMalfunctions.Malfunctions == m);   // кол-во обращений с данной неисправность
                income = dc.OrderServices.Where(os => os.CarMalfunctions.Malfunctions == m)
                           .Sum(os => os.ServicesInfos.Price);                                 // общая сумма дохода за устранение данной неисправности
            }// using

            // создание контейнеров для помещения данных в строку таблицы (TableRow)
            Paragraph pName = new Paragraph();      // имя
            Paragraph pAmount = new Paragraph();    // кол-во
            Paragraph pIncome = new Paragraph();    // доход

            pName.Inlines.Add(new Run(name));                       // добавление текста в параграф
            TableCell malfunctionName = new TableCell(pName);       // добаление параграфа в ячейку строки таблицы
            malfunctionName.BorderBrush = Brushes.Black;            // задание границы таблицы
            malfunctionName.BorderThickness = new Thickness(1d);

            pAmount.Inlines.Add(new Run($"{amount}"));              // добавление текста в параграф
            TableCell malfunctionAmount = new TableCell(pAmount);   // добаление параграфа в ячейку строки таблицы
            malfunctionAmount.BorderBrush = Brushes.Black;          // задание границы таблицы
            malfunctionAmount.BorderThickness = new Thickness(1d);

            pIncome.Inlines.Add(new Run($"{income}"));              // добавление текста в параграф
            TableCell malfunctionIncome = new TableCell(pIncome);   // добаление параграфа в ячейку строки таблицы
            malfunctionIncome.BorderBrush = Brushes.Black;          // задание границы таблицы
            malfunctionIncome.BorderThickness = new Thickness(1d);

            // создание стоки таблицы (TableRow) и добавление в нее
            // ячеек (TableCell)
            TableRow t = new TableRow();
            t.Cells.Add(malfunctionName);
            t.Cells.Add(malfunctionAmount);
            t.Cells.Add(malfunctionIncome);

            // вернуть сформированную строку
            return t;
        }// GetRow


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
                doc.PagePadding = new Thickness(24d);       // !!!важно!!! - задает отступ от границ страницы (по умолчанию А4)
                doc.ColumnGap = 25;
                doc.ColumnWidth = (doc.PageWidth - doc.ColumnGap        // задает ширину печати 1 столбца (можно разбить на несколько)
                    - doc.PagePadding.Left - doc.PagePadding.Right);

                // печать документа
                pd.PrintDocument(fdsvMain.Document.DocumentPaginator, "Отчет за месяц");

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
