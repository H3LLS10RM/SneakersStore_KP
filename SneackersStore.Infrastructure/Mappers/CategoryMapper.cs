using SneackersStore.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryViewModel Map(CategoryEntity category)
        {
            var viewModel = new CategoryViewModel
            {
                id = category.id,
                name = category.name
            };
            return viewModel;
        }

        public static List<CategoryViewModel> Map(List<CategoryEntity> categories)
        {
            var viewModels = categories.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
