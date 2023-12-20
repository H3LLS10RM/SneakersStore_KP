using SneackersStore.Infrastructure.DataBase;
using SneackersStore.Infrastructure.QR;
using SneackersStore.Infrastructure.ViewModels;
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

namespace SneakersStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для DiscountPage.xaml
    /// </summary>
    public partial class DiscountPage : Page
    {
        private DiscountRepository _repository;
        public DiscountPage()
        {
            InitializeComponent();
            _repository = new DiscountRepository();
            DiscountsGrid.ItemsSource = _repository.GetList();
            Title = "Скидки";
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
            DiscountsGrid.ItemsSource = _repository.Search(DiscountsSearchTextBox.Text);
            if (DiscountsSearchTextBox.Text == "")
            {
                DiscountsGrid.ItemsSource = _repository.GetList();
                _repository = new DiscountRepository();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var discountCard = new DiscountCard(); // Создание окна для добавления нового продукста
            discountCard.ShowDialog();// Отображение окна в виде диалога.
            DiscountsGrid.ItemsSource = _repository.GetList(); // Обновление данных в таблице после закрытия окна добавления продукта.
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
            if (DiscountsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }
            // Получение выбранного объекта из таблицы
            var item = DiscountsGrid.SelectedItem as DiscountViewModel;
            // Проверка, удалось ли получить данные о skidke
            if (item == null)
            {
                MessageBox.Show("Не удалось получить данные");
                return;
            }
            // Удаление продукта из репозитория и обновление данных в таблице.
            _repository.Delete(item.id);
            DiscountsGrid.ItemsSource = _repository.GetList();
        }


        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            // Обновляем данные в ProductsGrid перед экспортом

            DiscountsGrid.ItemsSource = _repository.GetList();
            var discountList = DiscountsGrid.Items.Cast<DiscountViewModel>().ToList();

            // Обновляем привязку данных, чтобы отразить изменения в DataGrid
            DiscountsGrid.Items.Refresh();

            // Создаем экземпляр ReportManager и экспортируем данные в Excel
            var reportManager = new SneackersStore.Infrastructure.Report.ReportManager();
            reportManager.ExportToExcel(DiscountsGrid, discountList);
        }
        private void DiscountsGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DiscountsGrid.SelectedItem == null)
            {
                MessageBox.Show("чет не то ты выбрал");
                return;
            }
            // Открытие окна редактирования для выбранного объекта и обновление данных в таблице.
            var discountCard = new DiscountCard(DiscountsGrid.SelectedItem as DiscountViewModel);
            discountCard.ShowDialog();
            _repository = new DiscountRepository();
            DiscountsGrid.ItemsSource = _repository.GetList();

        }
    }
}
