namespace SneackersStore.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class ProductEntity
    {
        public int id { get; set; }

        public int category_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int cost { get; set; }

        public int amount { get; set; }

        public int? discount_id { get; set; }

        public virtual CategoryEntity category { get; set; }

        public virtual DiscountEntity discount { get; set; }
    }
}
