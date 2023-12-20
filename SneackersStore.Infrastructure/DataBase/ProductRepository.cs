using SneackersStore.Infrastructure.Mappers;
using SneackersStore.Infrastructure.ViewModels;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;

namespace SneackersStore.Infrastructure.DataBase
{
    public class ProductRepository : IDisposable
    {
        private Context _context;
        public ProductRepository()
        {
            _context = new Context();
        }
        public List<ProductViewModel> GetList()
        {
                var items = _context.product
                    .Include(p => p.discount)
                    .Include(p => p.category)
                    .ToList();

                return ProductMapper.Map(items);
        }
        public ProductEntity GetById(long id)
        {
            using (var context = new Context())
            {
                return _context.product.FirstOrDefault(x => x.id == id);
                
            }
        }
        public List<ProductViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

                var result = _context.product
                .Where(x => x.name.ToLower().Contains(search))
                .ToList();

                return ProductMapper.Map(result);
        }
            public ProductViewModel Delete(long id) // Метод для удаления клиента из базы данных по идентификатору.
            {
                using (var context = new Context())
                {
                    var productToRemove = context.product.FirstOrDefault(c => c.id == id);
                    if (productToRemove != null)
                    {
                        context.product.Remove(productToRemove);// Удаление клиента из базы данных.
                        context.SaveChanges();
                    }
                    return ProductMapper.Map(productToRemove);// Преобразование удаленной сущности в ViewModel.
                }
            }
            public ProductViewModel Add(ProductEntity entity) // Метод для добавления нового клиента в базу данных.
            {// Обрезка строковых полей от лишних пробелов.
                entity.name = entity.name.Trim();
                // Проверка наличия заполненных полей.
                if (string.IsNullOrEmpty(entity.name))
                {
                    throw new Exception("Не все поля заполнены");
                }
                using (var context = new Context())
                {
                    context.product.Add(entity);// Добавление нового клиента в базу данных.
                    context.SaveChanges();
                }
                return ProductMapper.Map(entity);
            }
            public ProductViewModel Update(ProductEntity entity) // Метод для обновления данных клиента в базе данных.
            {// Обрезка строковых полей от лишних пробелов.
                entity.name = entity.name.Trim();
                // Проверка наличия заполненных полей.
                if (string.IsNullOrEmpty(entity.name))
                {
                    throw new Exception("Не все поля заполнены");
                }

                using (var context = new Context())
                {
                    var existingProduct = context.product.Find(entity.id);

                    if (existingProduct != null)
                    {// Обновление данных существующего клиента.
                        context.Entry(existingProduct).CurrentValues.SetValues(entity);
                        context.SaveChanges();
                    }
                }
                return ProductMapper.Map(entity);// Преобразование сущности в ViewModel.
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
