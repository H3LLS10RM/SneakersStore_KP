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
using SneackersStore.Infrastructure.ViewModels;

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
            ProductsGrid.ItemsSource = _repository.Search(ProductsSearchTextBox.Text);
            if (ProductsSearchTextBox.Text == "")
            {
                ProductsGrid.ItemsSource = _repository.GetList();
                _repository = new ProductRepository();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var productCard = new ProductCard(); // Создание окна для добавления нового продукста
            productCard.ShowDialog();// Отображение окна в виде диалога.
            ProductsGrid.ItemsSource = _repository.GetList(); // Обновление данных в таблице после закрытия окна добавления продукта.
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
            if (ProductsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }
            // Получение выбранного объекта из таблицы
            var item = ProductsGrid.SelectedItem as ProductViewModel;
            // Проверка, удалось ли получить данные о продукте
            if (item == null)
            {
                MessageBox.Show("Не удалось получить данные");
                return;
            }
            // Удаление продукта из репозитория и обновление данных в таблице.
            _repository.Delete(item.id);
            ProductsGrid.ItemsSource = _repository.GetList();
        }


        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            // Обновляем данные в ProductsGrid перед экспортом
            
            ProductsGrid.ItemsSource = _repository.GetList();
            var productList = ProductsGrid.Items.Cast<ProductViewModel>().ToList();

            // Обновляем привязку данных, чтобы отразить изменения в DataGrid
            ProductsGrid.Items.Refresh();

            // Создаем экземпляр ReportManager и экспортируем данные в Excel
            var reportManager = new SneackersStore.Infrastructure.Report.ReportManager();
            reportManager.ExportToExcel(ProductsGrid, productList);
        }
        private void ProductsGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ProductsGrid.SelectedItem == null)
            {
                MessageBox.Show("чет не то ты выбрал");
                return;
            }
            // Открытие окна редактирования для выбранного объекта и обновление данных в таблице.
            var productCard = new ProductCard(ProductsGrid.SelectedItem as ProductViewModel);
            productCard.ShowDialog();
            _repository = new ProductRepository();
            ProductsGrid.ItemsSource = _repository.GetList();

        }
    }
}
