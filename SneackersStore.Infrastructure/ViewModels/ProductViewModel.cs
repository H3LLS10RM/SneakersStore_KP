using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.ViewModels
{
    public class ProductViewModel
    {
        public int id { get; set; }

        public int category_id { get; set; }

       
        public string name { get; set; }

        public long cost { get; set; }

        public int amount { get; set; }

        public int discount_id { get; set; }
        
        [JsonIgnore]
        public virtual CategoryEntity category { get; set; }
        
        [JsonIgnore]
        public virtual DiscountEntity discount { get; set; }
    }
}
