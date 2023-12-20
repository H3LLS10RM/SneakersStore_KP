using SneackersStore.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.Mappers
{
    public static class DiscountMapper
    {
        public static DiscountViewModel Map(DiscountEntity discount)
        {
            var viewModel = new DiscountViewModel
            {
                id = discount.id,
                disc_name = discount.disc_name,
                value = discount.value,
            };
            return viewModel;
        }

        public static List<DiscountViewModel> Map(List<DiscountEntity> discounts)
        {
            var viewModels = discounts.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
