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
    /// Логика взаимодействия для AddModel.xaml
    /// </summary>
    public partial class AddModel : Window
    {
        MaintenanceDataContext dc = new MaintenanceDataContext();
        public AddModel()
        {
            InitializeComponent();
        }


        // При нажатии кнопки добавить
        private void add_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем все ли данные введены
            if (FieldsAreNotFilled()) { MessageBox.Show("Не все данные введены"); return; }

            // Проверяем есть ли такая модель
            int count = dc.Models.Where(m => m.Name == model.Text).Count();

            // Если такая модель, оповещаем пользователя
            if (count > 0) { MessageBox.Show("Такая модель уже есть"); return; }

            // Добавляем запись в базу
            dc.Models.InsertOnSubmit(GetModel());
            try
            {
                dc.SubmitChanges();
                MessageBox.Show($"Новая модель добавлена");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }//try-catch
        }//add_Click


        // Извлекаем данные из окна
        private Models GetModel()
        {
            Models newModel = new Models();
            newModel.Name = model.Text;

            return newModel;
        }//GetModel

        // Метод проверяет все ли данные введены
        private bool FieldsAreNotFilled()
        {
            return string.IsNullOrWhiteSpace(model.Text);
        }//FieldsAreNotFilled
    }
}