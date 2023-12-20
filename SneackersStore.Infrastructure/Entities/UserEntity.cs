namespace SneackersStore.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("users")]
    public partial class UserEntity
    {
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string surname { get; set; }

        
        public int role { get; set; }

        [Required]
        [StringLength(10)]
        public string sex { get; set; }


        public int status { get; set; }

        [Required]
        [StringLength(50)]
        public string login { get; set; }

        [Required]
        [StringLength(20)]
        public string password { get; set; }

        public virtual RoleEntity role1 { get; set; }

        public virtual StatusEntity status1 { get; set; }
    }
}
