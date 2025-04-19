using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;

        public DbInitializer(StoreDbContext context)
        {
            _context = context;
        }
        public async Task InitializeAsync()
        {
            try
            {
                //Create Database if it doesn't exist && Apply any Pending Migrations (Update Database)
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }


                // Data Seeding
                //1. Product Types
                if (!_context.ProductTypes.Any())
                {
                    // Read the JSON file
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                    // Deserialize the JSON data into a list of ProductType objects
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    //Add the data to the database
                    if (types is not null && types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }

                //2. Product Brands
                if (!_context.ProductBrands.Any())
                {
                    // Read the JSON file
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                    // Deserialize the JSON data into a list of ProductBrand objects
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    //Add the data to the database
                    if (brands is not null && brands.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }
                }

                //3. Products 
                if (!_context.Products.Any())
                {
                    // Read the JSON file
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                    // Deserialize the JSON data into a list of Products objects
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    //Add the data to the database
                    if (products is not null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
