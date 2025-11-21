using Store.Application.Models;
using Store.Common.Results;

namespace Store.Application.Services;
public interface IAdminUserService
{
        Task<Result<User>> CreateAdminUserAsync(User user, string password, CancellationToken cancellationToken);
        Task<Result<User>> GetUserAsync(int userId, CancellationToken cancellationToken);
}