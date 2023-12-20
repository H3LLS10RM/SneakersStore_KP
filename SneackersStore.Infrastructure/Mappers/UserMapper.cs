using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SneackersStore.Infrastructure.ViewModels;

namespace SneackersStore.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static UserViewModel Map(UserEntity user)
        {
            var viewModel = new UserViewModel
            {
                id = user.id,
                name = user.name,
                surname = user.surname,
                role = user.role1,
                status = user.status1,
                login = user.login,
                password = user.password,
                sex = user.sex == "М" ? "Мужской" : "Женский",
            };
            return viewModel;
        }

        public static List<UserViewModel> Map(List<UserEntity> users)
        {
            var viewModels = users.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}


