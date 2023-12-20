using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.ViewModels
{
    public class CategoryViewModel
    {
        
        public CategoryViewModel()
        {
            product = new HashSet<ProductEntity>();
        }

        public int id { get; set; }

        
        public string name { get; set; }

        
        public virtual ICollection<ProductEntity> product { get; set; }
    }
}
