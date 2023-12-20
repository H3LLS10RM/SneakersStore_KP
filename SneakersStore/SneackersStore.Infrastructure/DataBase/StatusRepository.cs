using SneackersStore.Infrastructure.Mappers;
using SneackersStore.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.DataBase
{
    public class StatusRepository
    {
        public List<StatusViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.status.ToList();
                return StatusMapper.Map(items);
            }
        }
        public StatusEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.status.FirstOrDefault(x => x.id == id);
                return item;
            }
        }
    }
}
