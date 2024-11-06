using Catalog.DataAccessLayer.Entities;
using Bogus;

namespace Catalog.DataAccessLayer.Seeding
{
    public static class CatalogContextSeeder
    {
        public static void Seed(CatalogContext context)
        {
            var categories = GenerateCategories(10);
            context.Categories.AddRange(categories);
            context.SaveChanges();
            var products = GenerateProducts(50, categories);
            context.Products.AddRange(products);
            context.SaveChanges();
            

            var productTags = GenerateProductTags(100, products);
            AddTagsToProducts(productTags, products);
            context.SaveChanges();

            var promotions = GeneratePromotions(20);
            context.Promotions.AddRange(promotions);
            context.SaveChanges();

            AddPromotionsToProducts(promotions, products);
            context.SaveChanges();

            var ingredients = GenerateIngredients(30);
            context.Ingredients.AddRange(ingredients);
            context.SaveChanges();

            AddIngredientsToProducts(ingredients, products);
            context.SaveChanges();
        }

        private static List<Category> GenerateCategories(int count)
        {
            var categoryFaker = new Faker<Category>()
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                .RuleFor(c => c.ImageUrl, f => f.Image.PicsumUrl());

            return categoryFaker.Generate(count);
        }

        private static List<Product> GenerateProducts(int count, List<Category> categories)
        {
            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => f.Random.Decimal(10, 1000))
                .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).CategoryId)
                .RuleFor(p => p.ImageUrl, f => f.Image.PicsumUrl())
                .RuleFor(p => p.Availability, f => f.Random.Bool());

            return productFaker.Generate(count);
        }

        private static List<Producttag> GenerateProductTags(int count, List<Product> products)
        {
            var productTagFaker = new Faker<Producttag>()
                .RuleFor(pt => pt.TagName, f => f.Commerce.ProductAdjective());
               

            return productTagFaker.Generate(count);
        }

        private static List<Promotion> GeneratePromotions(int count)
        {
            var promotionFaker = new Faker<Promotion>()
                .RuleFor(p => p.DiscountPercentage, f => f.Random.Decimal(5, 50))
                .RuleFor(p => p.StartDate, f => f.Date.Past(1))
                .RuleFor(p => p.EndDate, f => f.Date.Future(1));

            return promotionFaker.Generate(count);
        }

        private static List<Review> GenerateReviews(int count, List<Product> products)
        {
            var reviewFaker = new Faker<Review>()
                .RuleFor(r => r.Rating, f => f.Random.Byte(0, 5))
                .RuleFor(r => r.Comment, f => f.Lorem.Sentence())
                .RuleFor(r => r.ReviewDate, f => f.Date.Past(1))
                .RuleFor(r => r.ProductId, f => f.PickRandom(products).ProductId);

            return reviewFaker.Generate(count);
        }

       

       
        private static void AddTagsToProducts(List<Producttag> productTags, List<Product> products)
        {
            var random = new Random();
            foreach (var product in products)
            {
                int tagCount = random.Next(1, Math.Min(4, productTags.Count + 1));
                var tagsForProduct = productTags.OrderBy(t => random.Next()).Take(tagCount).ToList();

                foreach (var tag in tagsForProduct)
                {
                    product.Producttags.Add(tag);
                    tag.Product.Add(product);
                }
            }
        }

        private static void AddPromotionsToProducts(List<Promotion> promotions, List<Product> products)
        {
            var random = new Random();

            foreach (var promotion in promotions)
            {
                
                int productCount = random.Next(1, Math.Min(6, products.Count + 1));
                var selectedProducts = products.OrderBy(p => random.Next()).Take(productCount).ToList();

                foreach (var product in selectedProducts)
                {
                    
                    product.Promotions.Add(promotion);
                    promotion.Products.Add(product);
                }
            }
        }

        private static List<Ingredient> GenerateIngredients(int count)
        {
            var ingredientFaker = new Faker<Ingredient>()
                .RuleFor(i => i.Name, f => f.Commerce.ProductMaterial());

            return ingredientFaker.Generate(count);
        }

       
        private static void AddIngredientsToProducts(List<Ingredient> ingredients, List<Product> products)
        {
            var random = new Random();
            foreach (var product in products)
            {
                int ingredientCount = random.Next(1, Math.Min(5, ingredients.Count + 1)); // От 1 до 4 ингредиентов на продукт
                var selectedIngredients = ingredients.OrderBy(i => random.Next()).Take(ingredientCount).ToList();

                foreach (var ingredient in selectedIngredients)
                {
                    product.ProductIngredients.Add(ingredient);
                    ingredient.Products.Add(product);
                }
            }
        }

    }
}

