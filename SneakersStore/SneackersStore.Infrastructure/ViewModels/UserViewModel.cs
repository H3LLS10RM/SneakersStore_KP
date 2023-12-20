using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace SneackersStore.Infrastructure.ViewModels
{
    public class UserViewModel
    {
        public long id { get; set; }

        public string name { get; set; }


        public string surname { get; set; }
        
        [JsonIgnore]
        public RoleEntity role { get; set; }


        public string sex { get; set; }
        
        [JsonIgnore]
        public StatusEntity status { get; set; }


        public string login { get; set; }

        public string password { get; set; }

        public virtual long role1Name { get; set; }

        public virtual long status1Name { get; set; }
    }
}