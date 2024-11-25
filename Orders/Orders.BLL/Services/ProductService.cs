using AutoMapper;
using Orders.BLL.Services.Contrancts;
using Orders.DAL.Models;
using Orders.DAL.UOF;

namespace Orders.BLL.Services;

public class ProductService : IProductService
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    

    public ProductService(
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var result = await _unitOfWork.ProductRepository.GetAllAsync();
        _unitOfWork.CompleteAsync();
        return result;
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        var result = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
        _unitOfWork.CompleteAsync();
        return result;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        var result = await _unitOfWork.ProductRepository.AddAsync(product);
        _unitOfWork.CompleteAsync();
        return result;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        var result = await _unitOfWork.ProductRepository.UpdateAsync(product);
        _unitOfWork.CompleteAsync();
        return result;
    }

    public async Task DeleteProductAsync(int id)
    {
        await _unitOfWork.ProductRepository.DeleteByIdAsync(id);
        _unitOfWork.CompleteAsync();
    }
}