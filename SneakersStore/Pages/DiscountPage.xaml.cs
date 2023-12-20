using SneackersStore.Infrastructure.DataBase;
using SneackersStore.Infrastructure.QR;
using SneakersStore.Windows;
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

namespace SneakersStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для DiscountPage.xaml
    /// </summary>
    public partial class DiscountPage : Page
    {
        private UserRepository _repository;
        public DiscountPage()
        {
            InitializeComponent();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MenuPage menuPage = new MenuPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = menuPage.Title;
            mainWindow.MainFrame.Navigate(menuPage);
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QRButton_Click(object sender, RoutedEventArgs e)
        {
            if (DiscountsGrid.SelectedItem != null)
            {
                // Создание QR-кода для выбранного клиента и его отображение в новом окне.
                var qrManager = new QRManager();
                var qrCodeImage = qrManager.Generate(DiscountsGrid.SelectedItem);
                var qrWindow = new QRWindow();
                qrWindow.qrImage.Source = qrCodeImage;
                qrWindow.Show();
            }
            else
            {
                MessageBox.Show("Объект не выбран");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            // Обновляем данные в UsersGrid перед экспортом
            DiscountsGrid.ItemsSource = _repository.GetList();

            // Обновляем привязку данных, чтобы отразить изменения в DataGrid
            DiscountsGrid.Items.Refresh();

            // Создаем экземпляр ReportManager и экспортируем данные в Excel
            var reportManager = new SneackersStore.Infrastructure.Report.ReportManager();
            reportManager.ExportToExcel(DiscountsGrid);
        }
        private void DiscountsGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DiscountsGrid.SelectedItem == null)
                return;
        }
    }
}
