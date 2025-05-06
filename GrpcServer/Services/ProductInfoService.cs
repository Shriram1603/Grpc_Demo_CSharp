using Grpc.Core;
using GrpcServer.Protos;

namespace GrpcServer.Services
{
    public class ProductInfoService : ProductInfo.ProductInfoBase
    {
        private static List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = "1",
                Name = "Product 1",
                Description = "Description 1",
            },
            new Product
            {
                Id = "2",
                Name = "Product 2",
                Description = "Description 2",
            }
        };

        private readonly ILogger<ProductInfoService> _logger;

        public ProductInfoService(ILogger<ProductInfoService> logger)
        {
            _logger = logger;
        }

        public override Task<ProductID> addProduct(Product request, ServerCallContext context)
        {
            Products.Add(request);

            ProductID productId = new ProductID
            {
                Value = request.Id
            };

            return Task.FromResult(productId);
        }

        public override Task<Product> getProduct(ProductID request, ServerCallContext context)
        {
            var product = Products.FirstOrDefault(p => p.Id == request.Value);

            return Task.FromResult(product);
        }
    }
}
