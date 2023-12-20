using SneackersStore.Infrastructure.Mappers;
using SneackersStore.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.DataBase
{
    public class CategoryRepository
    {
        public List<CategoryViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.category.ToList();
                return CategoryMapper.Map(items);
            }
        }
        public CategoryEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.category.FirstOrDefault(x => x.id == id);
                return item;
            }
        }
    }
}
