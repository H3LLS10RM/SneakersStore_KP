using SneackersStore.Infrastructure.Mappers;
using SneackersStore.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.DataBase
{
    public class DiscountRepository
    {
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
    }
}
