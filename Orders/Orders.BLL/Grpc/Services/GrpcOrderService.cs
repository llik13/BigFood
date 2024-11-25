using AutoMapper;
using Grpc.Core;
using Orders.DAL.UOF;

namespace Orders.BLL.Grpc.Services;

public class GrpcOrderService : OrderProtoService.OrderProtoServiceBase 
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GrpcOrderService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public override async Task GetAllOrders(GetAllOrdersRequest request, IServerStreamWriter<ShortOrderResponseGrpc> responseStream, ServerCallContext context)
    {
        var orders = await _unitOfWork.OrderRepository.GetAllAsync();
        foreach (var order in orders)
        {
            var result = _mapper.Map<ShortOrderResponseGrpc>(order);
            await responseStream.WriteAsync(result);
        }
    }
}