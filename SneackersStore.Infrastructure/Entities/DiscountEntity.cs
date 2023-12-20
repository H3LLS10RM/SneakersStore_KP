namespace SneackersStore.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("discount")]
    public partial class DiscountEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiscountEntity()
        {
            product = new HashSet<ProductEntity>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string disc_name { get; set; }

        public long value { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductEntity> product { get; set; }
    }
}
