using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(x => x.Id == id)
        {
            ApplyInclude();
        }

        public ProductWithBrandsAndTypesSpecifications(ProductSpecificationsParameters specParams) 
            : base(
                  P => 
                  (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search.ToLower()))
                  && (!specParams.BrandID.HasValue || P.BrandId == specParams.BrandID)
                  && (!specParams.TypeID.HasValue || P.TypeId == specParams.TypeID) )
        {
            ApplyInclude();
            ApplySorting(specParams.Sort);
            ApplyPagination(specParams.PageIndex, specParams.PageSize);
        }

        private void ApplyInclude()
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
        private void ApplySorting(string? sort) {

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "nameasc":
                        AddOrderBy(x => x.Name);
                        break;
                    case "namedesc":
                        AddOrderByDescending(x => x.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(x => x.Name);
            }
        }

    }
    
}
