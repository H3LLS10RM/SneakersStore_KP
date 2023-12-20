using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.ViewModels
{
    public class StatusViewModel
    {
        
        public StatusViewModel()
        {
            users = new HashSet<UserEntity>();
        }

        public int id { get; set; }

        public string status_name { get; set; }

        
        public virtual ICollection<UserEntity> users { get; set; }
    }
}
