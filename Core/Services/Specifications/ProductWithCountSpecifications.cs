using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithCountSpecifications(ProductSpecificationsParameters specParams)
            : base(
                  P =>
                  (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search.ToLower()))
                  && (!specParams.BrandID.HasValue || P.BrandId == specParams.BrandID)
                  && (!specParams.TypeID.HasValue || P.TypeId == specParams.TypeID))
        {

        }
    }
}
