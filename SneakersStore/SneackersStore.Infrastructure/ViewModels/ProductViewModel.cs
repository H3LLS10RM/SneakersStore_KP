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
        public long id { get; set; }

        public long category_id { get; set; }

       
        public string name { get; set; }

        public long cost { get; set; }

        public long amount { get; set; }

        public long? discount_id { get; set; }
        
        [JsonIgnore]
        public virtual CategoryEntity category { get; set; }
        
        [JsonIgnore]
        public virtual DiscountEntity discount { get; set; }
    }
}
