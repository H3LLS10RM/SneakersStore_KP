using SneackersStore.Infrastructure.Consts;
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
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            Title = "Главное меню";
        }

        private void UserButton(object sender, RoutedEventArgs e)
        {
            UserPage userPage = new UserPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = userPage.Title;
            mainWindow.MainFrame.Navigate(userPage);

        }

        private void ProductButton(object sender, RoutedEventArgs e)
        {
            ProductPage productPage = new ProductPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = productPage.Title;
            mainWindow.MainFrame.Navigate(productPage);
        }

        private void DiscountButton(object sender, RoutedEventArgs e)
        {
            DiscountPage discountPage = new DiscountPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = discountPage.Title;
            mainWindow.MainFrame.Navigate(discountPage);
        }


        private void ExitButton(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoginBlock.Text = Application.Current.Resources[UserInfoConsts.Login].ToString();
            RolenameBlock.Text = Application.Current.Resources[UserInfoConsts.Role_name].ToString();
        }
    }
}
