using SneackersStore.Infrastructure.DataBase;
using SneackersStore.Infrastructure.QR;
using SneakersStore.Cards;
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
using System.IO;
using System.Reflection;
using Path = System.IO.Path;

namespace SneakersStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private ProductRepository _repository;
        public ProductPage()
        {
            InitializeComponent();
            _repository = new ProductRepository();
            ProductsGrid.ItemsSource = _repository.GetList();
            Title = "Товары";
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
            if (ProductsGrid.SelectedItem != null)
            {
                using (var repository = new ProductRepository())
                {
                    // Создание QR-кода для выбранного клиента и его отображение в новом окне.
                    var qrManager = new QRManager();
                    var qrCodeImage = qrManager.Generate(ProductsGrid.SelectedItem);
                    var qrWindow = new QRWindow();
                    qrWindow.qrImage.Source = qrCodeImage;
                    qrWindow.Show();
                }
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
            ProductsGrid.ItemsSource = _repository.GetList();

            // Обновляем привязку данных, чтобы отразить изменения в DataGrid
            ProductsGrid.Items.Refresh();

            // Создаем экземпляр ReportManager и экспортируем данные в Excel
            var reportManager = new SneackersStore.Infrastructure.Report.ReportManager();
            reportManager.ExportToExcel(ProductsGrid);
        }
        private void ProductsGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ProductsGrid.SelectedItem == null)
                return;


        }
    }
}
