using AutoMapper;
using Grpc.Core;
using ProductMicroservice.Data.Entities;
using ProductMicroservice.Data.Exceptions;
using ProductMicroservice.Data.Repositories;
using ProductMicroservice.Protos;

namespace ProductMicroservice.Services;

public class ProductService : ProductServiceProto.ProductServiceProtoBase
{
    private readonly ILogger<ProductService> _logger;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService(
        ILogger<ProductService> logger,
        IMapper mapper,
        IProductRepository productRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public override async Task GetProducts(GetProductsRequest request, IServerStreamWriter<ProductDto> responseStream, ServerCallContext context)
    {
        _logger.LogInformation("Product microservice: Getting products...");

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
        _logger.LogInformation("Product microservice: Adding new product...");

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