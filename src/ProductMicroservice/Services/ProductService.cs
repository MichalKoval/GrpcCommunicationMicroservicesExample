using AutoMapper;
using Grpc.Core;
using ProductMicroservice.Data.Entities;
using ProductMicroservice.Data.Repositories;
using ProductMicroservice.Protos;
using System.Data;

namespace ProductMicroservice.Services
{
    public class ProductService : ProductServiceProto.ProductServiceProtoBase
    {
        private readonly TimeSpan ResponseStreamMessageDelay = TimeSpan.FromMilliseconds(500);
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductReviewRepository _productReviewRepository;

        public ProductService(
            ILogger<ProductService> logger,
            IMapper mapper,
            IProductRepository productRepository,
            IProductReviewRepository productReviewRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
            _productReviewRepository = productReviewRepository;
        }

        public override async Task GetProducts(GetProductsRequest request, IServerStreamWriter<ProductDto> responseStream, ServerCallContext context)
        {
            var products = await _productRepository.GetAsync(request.ProductIds);

            if (products is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Products were not found."));
            }

            var productResults = products.Select(p => _mapper.Map<ProductDto>(p)).ToList();

            foreach (var productResult in productResults)
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    return;
                }

                await responseStream.WriteAsync(productResult);
            }
        }

        public override async Task<ProductDto> AddProduct(AddProductRequest request, ServerCallContext context)
        {
            try
            {
                var product = await _productRepository.AddAsync(_mapper.Map<Product>(request.Product));

                return _mapper.Map<ProductDto>(product);
            }
            catch (DataException e)
            {
                throw new RpcException(new Status(StatusCode.AlreadyExists, e.Message));
            }
            catch (Exception)
            {
                throw new RpcException(new Status(StatusCode.Internal, "Unknown error."));
            }
        }
    }
}