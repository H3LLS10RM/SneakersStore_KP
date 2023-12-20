using SneackersStore.Infrastructure.Mappers;
using SneackersStore.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.DataBase
{
    public class RoleRepository
    {
        public List<RoleViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.role.ToList();
                return RoleMapper.Map(items);
            }
        }
        public RoleEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.role.FirstOrDefault(x => x.id == id);
                return item;
            }
        }
    }
}
