using SneackersStore.Infrastructure.DataBase;
using SneackersStore.Infrastructure.ViewModels;
using SneackersStore.Infrastructure;
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

namespace SneakersStore.Cards
{
    /// <summary>
    /// Логика взаимодействия для DiscountCard.xaml
    /// </summary>
    public partial class DiscountCard : Window
    {
        private DiscountRepository _repository = new DiscountRepository();
        private DiscountViewModel _selectedItem = null;
        public DiscountCard()
        {
            InitializeComponent();
        }
        public DiscountCard(DiscountViewModel selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;
            FillFormFields();
        }
        private void FillFormFields()
        {
            if (_selectedItem != null)
            {// Заполнение полей формы значениями выбранного элемента
                Name.Text = _selectedItem.disc_name;
                Value.Text = _selectedItem.value.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                DiscountEntity entity = new DiscountEntity  // Создание объекта с данными продукта
                {
                    disc_name = Name.Text,
                    value = int.Parse(Value.Text),
                };
               
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
                this.Close(); // Закрытие 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Вывод сообщения об ошибке
            }
        }
    }
}
