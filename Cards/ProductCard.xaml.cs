using SneackersStore.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using SneakersStore.Cards;
using SneakersStore.Pages;
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
using SneakersStore.Windows;
using SneackersStore.Infrastructure;
using SneackersStore.Infrastructure.ViewModels;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using SneackersStore.Infrastructure.QR;
using System.Data;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;


namespace SneakersStore.Cards
{
    /// <summary>
    /// Логика взаимодействия для ProductCard.xaml
    /// </summary>
    public partial class ProductCard : Window
    {
        private DiscountRepository discount_repository = new DiscountRepository();
        private CategoryRepository category_repository = new CategoryRepository();
        private ProductRepository _repository = new ProductRepository();
        private ProductViewModel _selectedItem = null;
        public ProductCard()
        {
            InitializeComponent();
            Discount.ItemsSource = discount_repository.GetList();
            Category.ItemsSource = category_repository.GetList();
        }
        public ProductCard(ProductViewModel selectedItem)
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
                Amount.Text = _selectedItem.amount.ToString();
                Cost.Text = _selectedItem.cost.ToString();
                Category.ItemsSource = category_repository.GetList();
                Discount.ItemsSource = discount_repository.GetList();
                var result1 = new List<CategoryViewModel>();
                var result = new List<DiscountViewModel>();
                foreach (CategoryViewModel category in Category.ItemsSource)
                {
                    if (_selectedItem.category.id == category.id)
                    {
                        Category.SelectedItem = category;// Установка выбранного элемента в списке скидок
                        break;
                    }
                    else
                    {
                        result1.Add(category);
                    }
                    Category.SelectedItem = result1[0];// Установка первого элемента списка категорий по умолчанию
                }
                foreach (DiscountViewModel discount in Discount.ItemsSource)
                {
                    if (_selectedItem.discount.id == discount.id)
                    {
                        Discount.SelectedItem = discount;// Установка выбранного элемента в списке скидок
                        break;
                    }
                    else
                    {
                        result.Add(discount);
                    }
                    Discount.SelectedItem = result[0];// Установка первого элемента списка скидок по умолчанию
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {


                DiscountViewModel selectedDiscount = Discount.SelectedItem as DiscountViewModel;
                CategoryViewModel selectedCategory = Category.SelectedItem as CategoryViewModel;
                ProductEntity entity = new ProductEntity  // Создание объекта с данными продукта
                {
                    name = Name.Text,
                    amount = int.Parse(Amount.Text),
                    cost = int.Parse(Cost.Text),
                    

                };
                if (selectedCategory == null || selectedDiscount == null)
                {
                    throw new Exception("Не все поля заполнены");// Выброс исключения, если не все поля заполнены
                }
                else
                {
                    entity.discount_id = selectedDiscount.id;
                    entity.category_id = selectedCategory.id;
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
                this.Close(); // Закрытие 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Вывод сообщения об ошибке
            }
        }
    }
}
