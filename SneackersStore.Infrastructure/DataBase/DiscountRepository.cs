using SneackersStore.Infrastructure.Mappers;
using SneackersStore.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.DataBase
{
    public class DiscountRepository : IDisposable
    {
        private Context _context;
        public DiscountRepository()
        {
            _context = new Context();
        }
        public List<DiscountViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.discount.ToList();
                return DiscountMapper.Map(items);
            }
        }
        public DiscountEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.discount.FirstOrDefault(x => x.id == id);
                return item;
            }
        }

        public List<DiscountViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            var result = _context.discount
            .Where(x => x.disc_name.ToLower().Contains(search))
            .ToList();

            return DiscountMapper.Map(result);
        }
        public DiscountViewModel Delete(long id) // Метод для удаления клиента из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var discountToRemove = context.discount.FirstOrDefault(c => c.id == id);
                if (discountToRemove != null)
                {
                    context.discount.Remove(discountToRemove);// Удаление клиента из базы данных.
                    context.SaveChanges();
                }
                return DiscountMapper.Map(discountToRemove);// Преобразование удаленной сущности в ViewModel.
            }
        }
        public DiscountViewModel Add(DiscountEntity entity) // Метод для добавления нового клиента в базу данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.disc_name = entity.disc_name.Trim();
            // Проверка наличия заполненных полей.
            if (string.IsNullOrEmpty(entity.disc_name))
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                context.discount.Add(entity);// Добавление нового клиента в базу данных.
                context.SaveChanges();
            }
            return DiscountMapper.Map(entity);
        }
        public DiscountViewModel Update(DiscountEntity entity) // Метод для обновления данных клиента в базе данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.disc_name = entity.disc_name.Trim();
            // Проверка наличия заполненных полей.
            if (string.IsNullOrEmpty(entity.disc_name))
            {
                throw new Exception("Не все поля заполнены");
            }

            using (var context = new Context())
            {
                var existingDiscount = context.discount.Find(entity.id);

                if (existingDiscount != null)
                {// Обновление данных существующего клиента.
                    context.Entry(existingDiscount).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return DiscountMapper.Map(entity);// Преобразование сущности в ViewModel.
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}
