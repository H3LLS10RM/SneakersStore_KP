using SneackersStore.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.Mappers
{
    public static class RoleMapper
    {
        public static RoleViewModel Map(RoleEntity role)
        {
            var viewModel = new RoleViewModel
            {
                id = role.id,
                role_name = role.role_name
            };
            return viewModel;
        }

        public static List<RoleViewModel> Map(List<RoleEntity> roles)
        {
            var viewModels = roles.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
