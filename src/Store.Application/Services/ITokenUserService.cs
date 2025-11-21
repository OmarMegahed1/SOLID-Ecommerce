using Store.Application.Models;
using Store.Common.Results;

namespace Store.Application.Services;

public interface ITokenUserService
{
        Task<Result> VerifyPassword(string email, string password, CancellationToken cancellationToken);
        Task<Result<User>> GetUserAsync(string email, CancellationToken cancellationToken);
}