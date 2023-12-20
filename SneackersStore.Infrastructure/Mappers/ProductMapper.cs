using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SneackersStore.Infrastructure.ViewModels;

namespace SneackersStore.Infrastructure.Mappers
{
    public static class ProductMapper
    {
        public static ProductViewModel Map(ProductEntity product)
        {
            var viewModel = new ProductViewModel
            {
                id = product.id,
                name = product.name,
                category = product.category,
                cost = product.cost,
                amount = product.amount,
                discount = product.discount,
            };
            return viewModel;
        }

        public static List<ProductViewModel> Map(List<ProductEntity> products)
        {
            var viewModels = products.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
