using SneackersStore.Infrastructure;
using SneackersStore.Infrastructure.ViewModels;
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
using SneakersStore.Pages;
using SneackersStore.Infrastructure.DataBase;

namespace SneakersStore.Cards
{
    /// <summary>
    /// Логика взаимодействия для UserCard.xaml
    /// </summary>

    public partial class UserCard : Window
    {
        private StatusRepository status_repository = new StatusRepository();
        private RoleRepository role_repository = new RoleRepository();
        private UserRepository _repository = new UserRepository();
        private UserViewModel _selectedItem = null;

        public UserCard()
        {
            InitializeComponent();
            _repository = new UserRepository();
            Status.ItemsSource = status_repository.GetList();
            Role.ItemsSource = role_repository.GetList();
        }
        public UserCard(UserViewModel selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;
            FillFormFields();
        }
        private void FillFormFields()
        {
            if (_selectedItem != null)
            {// Заполнение полей формы значениями выбранного элемента
                Name.Text = _selectedItem.name;
                Surename.Text = _selectedItem.surname;
                Sex.Text = _selectedItem.sex;
                Login.Text = _selectedItem.login;
                Password.Text = _selectedItem.password;
                Status.ItemsSource = status_repository.GetList();
                Role.ItemsSource = role_repository.GetList();
                var result = new List<StatusViewModel>();
                var result1 = new List<RoleViewModel>();
                foreach (RoleViewModel role in Role.ItemsSource)
                {
                    if (_selectedItem.role.id == role.id)
                    {
                        Role.SelectedItem = role;// Установка выбранного элемента в списке скидок
                        break;
                    }
                    else
                    {
                        result1.Add(role);
                    }
                    Role.SelectedItem = result1[0];// Установка первого элемента списка ролей по умолчанию
                }
                foreach (StatusViewModel status in Status.ItemsSource)
                {
                    if (_selectedItem.status.id == status.id)
                    {
                        Status.SelectedItem = status;// Установка выбранного элемента в списке скидок
                        break;
                    }
                    else
                    {
                        result.Add(status);
                    }
                    Status.SelectedItem = result[0];// Установка первого элемента списка статусов по умолчанию
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                RoleViewModel selectedRole = Role.SelectedItem as RoleViewModel;
                StatusViewModel selectedStatus = Status.SelectedItem as StatusViewModel;
                UserEntity entity = new UserEntity  // Создание объекта с данными клиента
                {
                    name = Name.Text,
                    surname = Surename.Text,
                    sex = Sex.Text,
                    login = Login.Text,
                    password = Password.Text,
                    
                };
                if (selectedRole == null || selectedStatus == null)
                {
                    throw new Exception("Не все поля заполнены");// Выброс исключения, если не все поля заполнены
                }
                else
                {
                    entity.role = selectedRole.id;
                    entity.status = selectedStatus.id;
                }
                if (_selectedItem != null)
                {
                    entity.id = _selectedItem.id;
                    _repository.Update(entity);// Обновление данных клиента
                }
                else
                {
                    _repository.Add(entity);// Добавление нового клиента
                }

                MessageBox.Show("Запись успешно сохранена."); // Вывод сообщения об успешном сохранении
                this.Close(); // Закрытие окна
                _repository = new UserRepository();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Вывод сообщения об ошибке
            }
        }

    }
}
