using SneackersStore.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.Mappers
{
    public static class StatusMapper
    {
        public static StatusViewModel Map(StatusEntity status)
        {
            var viewModel = new StatusViewModel
            {
                id = status.id,
                status_name = status.status_name,
            };
            return viewModel;
        }

        public static List<StatusViewModel> Map(List<StatusEntity> statuses)
        {
            var viewModels = statuses.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
