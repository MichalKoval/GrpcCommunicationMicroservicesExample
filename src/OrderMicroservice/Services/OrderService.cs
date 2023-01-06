using AutoMapper;
using Grpc.Core;
using OrderMicroservice.Data.Entities;
using OrderMicroservice.Data.Exceptions;
using OrderMicroservice.Data.Repositories;
using OrderMicroservice.Protos;

namespace OrderMicroservice.Services;

public class OrderService : OrderServiceProto.OrderServiceProtoBase
{
    private readonly ILogger<OrderService> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderService(
        ILogger<OrderService> logger,
        IMapper mapper,
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
    }

    public override async Task GetOrders(GetOrdersRequest request, IServerStreamWriter<OrderDto> responseStream, ServerCallContext context)
    {
        _logger.LogInformation("Order microservice: Getting orders...");

        var orders = await _orderRepository.GetAsync(request.OrderIds);

        if (orders is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Orders were not found."));
        }

        var orderResults = orders.Select(o => _mapper.Map<OrderDto>(o)).ToList();

        foreach (var orderResult in orderResults)
        {
            if (context.CancellationToken.IsCancellationRequested)
            {
                return;
            }

            await responseStream.WriteAsync(orderResult);
        }
    }

    public override async Task<OrderDto> CreateOrder(CreateOrderRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Order microservice: Creating order...");

        var newOrder = new Order
        {
            OrderItems = new List<OrderItem>()
        };

        try
        {
            var order = await _orderRepository.AddAsync(newOrder);

            return _mapper.Map<OrderDto>(order);
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

    public override async Task<OrderItemDto> AddOrderItem(AddOrderItemRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Order microservice: Adding order item to the order ...");

        var newOrderItem = request.OrderItem;

        if (newOrderItem.OrderId is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Please provide Order Id."));
        }

        if (newOrderItem.ProductId is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Please provide Product Id."));
        }

        try
        {
            var orderItem = await _orderItemRepository.AddAsync(_mapper.Map<OrderItem>(newOrderItem));

            return _mapper.Map<OrderItemDto>(orderItem);
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