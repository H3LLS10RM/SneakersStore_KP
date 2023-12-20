using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.ViewModels
{
    public class DiscountViewModel
    {
        public DiscountViewModel()
        {
            product = new HashSet<ProductEntity>();
        }

        public long id { get; set; }

        
        public string disc_name { get; set; }

        public long value { get; set; }

       
        public virtual ICollection<ProductEntity> product { get; set; }
    }
}
