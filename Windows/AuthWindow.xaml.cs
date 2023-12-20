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
using SneackersStore.Infrastructure;
using SneackersStore.Infrastructure.Consts;
using SneackersStore.Infrastructure.DataBase;
using SneackersStore.Infrastructure.ViewModels;

namespace SneakersStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private UserRepository _repository;
        public AuthWindow()
        {
            InitializeComponent();
            _repository = new UserRepository();
            Title = "Окно авторизации";
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            UserEntity loggedInUser = _repository.Login(Login.Text, Password.Password);

            if (loggedInUser != null)
            {
                // Вход успешен, выполните необходимые действия
                string userName = $"{loggedInUser.name} {loggedInUser.surname}";
                MessageBox.Show($"Добро пожаловать, {userName}!");

                // Пример: установка информации о пользователе в ресурсы приложения
                Application.Current.Resources[UserInfoConsts.Role] = loggedInUser.role;
                Application.Current.Resources[UserInfoConsts.Role_name] = loggedInUser.role1.role_name;
                Application.Current.Resources[UserInfoConsts.Login] = loggedInUser.login;

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                // Вход не удался, выведите сообщение об ошибке
                MessageBox.Show("Ошибка входа. Проверьте логин и пароль.");
            }
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources[UserInfoConsts.Role] = 4;
            Application.Current.Resources[UserInfoConsts.Role_name] = "Гость";
            Application.Current.Resources[UserInfoConsts.Login] = "Гость";

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
