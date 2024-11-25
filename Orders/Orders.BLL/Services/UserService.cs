using AutoMapper;
using Orders.BLL.Services.Contrancts;
using Orders.DAL.Models;
using Orders.DAL.UOF;

namespace Orders.BLL.Services;

public class UserService : IUserService
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    

    public UserService(
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var result = await _unitOfWork.UserRepository.GetAllAsync();
        _unitOfWork.CompleteAsync();
        return result;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        var result = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        _unitOfWork.CompleteAsync();
        return result;
    }

    public async Task<User> AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }
}