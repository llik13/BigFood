using AgregatorGrpc.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;


namespace AgregatorGrpc.Services
{
    public class AggregatorService : Agregator.AgregatorBase
    {
        private readonly Products.ProductsClient _productsClient;
        private readonly Reviews.ReviewsClient _reviewsClient;

        public AggregatorService(Products.ProductsClient productsClient, Reviews.ReviewsClient reviewsClient)
        {
            _productsClient = productsClient;
            _reviewsClient = reviewsClient;
        }


        public async override Task<ListFullProducts> GetAll(Empty empty, ServerCallContext context)
        {
            var productList = await _productsClient.GetProductsAsync(empty);
            var reviewList = await _reviewsClient.GetReviewsAsync(empty);

            var listFullProducts = new ListFullProducts();

            var reviewDictionary = reviewList.Reviews.ToDictionary(r => r.Id);

            foreach (var product in productList.Products)
            {
                var fullProductModel = new FullProductModel
                {
                    Product = product,
                    Review = reviewDictionary.ContainsKey(product.ProductId) ? reviewDictionary[product.ProductId] : null
                };
                listFullProducts.Product.Add(fullProductModel);
            }

            return listFullProducts;


        }

    }
}
