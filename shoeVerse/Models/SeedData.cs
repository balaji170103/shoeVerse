using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using shoeVerse.Data;
using System;
using System.Linq;

namespace shoeVerse.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new shoeVerseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<shoeVerseContext>>()))
            {
                // Check if any products exist
                if (context.Product.Any())
                {
                    return;   // DB has already been seeded
                }

                // Add sample product data
                context.Product.AddRange(
                    new Product
                    {
                        Brand = "Nike",
                        Material = "Leather",
                        SoleType = "Rubber",
                        Size = "10",
                        Style = "Sneakers"
                    },
                    new Product
                    {
                        Brand = "Adidas",
                        Material = "Mesh",
                        SoleType = "Foam",
                        Size = "9",
                        Style = "Running Shoes"
                    },
                    new Product
                    {
                        Brand = "Puma",
                        Material = "Synthetic",
                        SoleType = "Rubber",
                        Size = "8",
                        Style = "Casual Shoes"
                    },
                    new Product
                    {
                        Brand = "Reebok",
                        Material = "Canvas",
                        SoleType = "EVA",
                        Size = "11",
                        Style = "Sports Shoes"
                    }
                );

                // Save changes to database
                context.SaveChanges();
            }
        }
    }
}
