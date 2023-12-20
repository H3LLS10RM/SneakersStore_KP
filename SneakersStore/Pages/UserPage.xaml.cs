using SneakersStore.Windows;
using SneakersStore.Cards;
using System.Windows;
using System.Windows.Controls;
using SneackersStore.Infrastructure.DataBase;
using SneackersStore.Infrastructure;
using SneackersStore.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using SneackersStore.Infrastructure.QR;

namespace SneakersStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>

    public partial class UserPage : Page
    {
        private UserRepository _repository;
        private UserViewModel _selectedItem = null;
        public UserPage()
        {
            InitializeComponent();
            _repository = new UserRepository();
            UsersGrid.ItemsSource = _repository.GetList();
            Title = "Пользователи";

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
            UsersGrid.ItemsSource = _repository.Search(UsersSearchTextBox.Text);
            if (UsersSearchTextBox.Text == "")
            {
                UsersGrid.ItemsSource = _repository.GetList();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var userCard = new UserCard(); // Создание окна для добавления нового клиента
            userCard.ShowDialog();// Отображение окна в виде диалога.
            UsersGrid.ItemsSource = _repository.GetList(); // Обновление данных в таблице после закрытия окна добавления клиента.

        }

        private void QRButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem != null)
            {
                using (var repository = new UserRepository())
                {
                    // Создание QR-кода для выбранного клиента и его отображение в новом окне.
                    var qrManager = new QRManager();
                    var qrCodeImage = qrManager.Generate(UsersGrid.SelectedItem);
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
            if (UsersGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }
            // Получение выбранного объекта из таблицы
            var item = UsersGrid.SelectedItem as UserViewModel;
            // Проверка, удалось ли получить данные о клиенте
            if (item == null)
            {
                MessageBox.Show("Не удалось получить данные");
                return;
            }
            // Удаление клиента из репозитория и обновление данных в таблице.
            _repository.Delete(item.id);
            UsersGrid.ItemsSource = _repository.GetList();
        }


        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            // Обновляем данные в UsersGrid перед экспортом
            UsersGrid.ItemsSource = _repository.GetList();

            // Обновляем привязку данных, чтобы отразить изменения в DataGrid
            UsersGrid.Items.Refresh();

            // Создаем экземпляр ReportManager и экспортируем данные в Excel
            var reportManager = new SneackersStore.Infrastructure.Report.ReportManager();
            reportManager.ExportToExcel(UsersGrid);
        }
        private void UsersGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (UsersGrid.SelectedItem == null)
            {
                MessageBox.Show("чет не то ты выбрал");
                return;
            }
            // Открытие окна редактирования для выбранного объекта и обновление данных в таблице.
            var userCard = new UserCard(UsersGrid.SelectedItem as UserViewModel);
            userCard.ShowDialog();
            UsersGrid.ItemsSource = _repository.GetList();

        }
    }
}
