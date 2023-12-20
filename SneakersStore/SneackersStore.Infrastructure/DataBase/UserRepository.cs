using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SneackersStore.Infrastructure.ViewModels;
using SneackersStore.Infrastructure.Mappers;

namespace SneackersStore.Infrastructure.DataBase
{
    public class UserRepository : IDisposable
    {
        private Context _context;

        public UserRepository()
        {
            _context = new Context();
        }

        public List<UserViewModel> GetList()
        {
            var items = _context.users
                .Include(u => u.status1)
                .Include(u => u.role1)
                .ToList();

            return UserMapper.Map(items);
        }

        public UserEntity GetById(long id)
        {
            return _context.users.FirstOrDefault(x => x.id == id);
        }

        public UserEntity Login(string login, string password)
        {
            if (login != null && password != null)
            {
                return _context.users
                    .Include(x => x.role1)
                    .FirstOrDefault(x => x.login == login && x.password == password);
            }

            return null;
        }

        public List<UserViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            var result = _context.users
                .Where(x => x.name.ToLower().Contains(search) || x.surname.ToLower().Contains(search))
                .ToList();

            return UserMapper.Map(result);
        }
        public UserViewModel Delete(long id) // Метод для удаления клиента из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var clientToRemove = context.users.FirstOrDefault(c => c.id == id);
                if (clientToRemove != null)
                {
                    context.users.Remove(clientToRemove);// Удаление клиента из базы данных.
                    context.SaveChanges();
                }
                return UserMapper.Map(clientToRemove);// Преобразование удаленной сущности в ViewModel.
            }
        }
        public UserViewModel Add(UserEntity entity) // Метод для добавления нового клиента в базу данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.name = entity.name.Trim();
            entity.surname = entity.surname.Trim();
            entity.login = entity.login.Trim();
            entity.password = entity.password.Trim();
            entity.sex = entity.sex.Trim();
            // Проверка наличия заполненных полей.
            if (string.IsNullOrEmpty(entity.name) || string.IsNullOrEmpty(entity.surname) || string.IsNullOrEmpty(entity.login) || string.IsNullOrEmpty(entity.password) || string.IsNullOrEmpty(entity.sex))
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                context.users.Add(entity);// Добавление нового клиента в базу данных.
                context.SaveChanges();
            }
            return UserMapper.Map(entity);
        }
        public UserViewModel Update(UserEntity entity) // Метод для обновления данных клиента в базе данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.name = entity.name.Trim();
            entity.surname = entity.surname.Trim();
            entity.sex = entity.sex.Trim();
            entity.login = entity.login.Trim();
            entity.password = entity.password.Trim();
            // Проверка наличия заполненных полей.
            if (string.IsNullOrEmpty(entity.name) || string.IsNullOrEmpty(entity.surname) || string.IsNullOrEmpty(entity.sex) || string.IsNullOrEmpty(entity.login) || string.IsNullOrEmpty(entity.password))
            {
                throw new Exception("Не все поля заполнены");
            }

            using (var context = new Context())
            {
                var existingClient = context.users.Find(entity.id);

                if (existingClient != null)
                {// Обновление данных существующего клиента.
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return UserMapper.Map(entity);// Преобразование сущности в ViewModel.
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
