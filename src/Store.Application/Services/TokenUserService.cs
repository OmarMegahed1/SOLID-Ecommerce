using Store.Application.Mappers;
using Store.Application.Models;
using Store.Common.Results;
using Store.Common.Helpers;
using Store.Infrastructure.Data;

namespace Store.Application.Services;

public class TokenUserService : ITokenUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordRepository _passwordRepository;
    private readonly IPasswordService _passwordService;

    public TokenUserService(IUserRepository userRepository, IPasswordRepository passwordRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository.NotNull();
        _passwordRepository = passwordRepository.NotNull();
        _passwordService = passwordService.NotNull();
    }

    public async Task<Result<User>> GetUserAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(email, cancellationToken);
        if (user == null)
            return new NotFoundResult<User>();

        return new SuccessResult<User>(user.Map());
    }

    public async Task<Result> VerifyPassword(string email, string password, CancellationToken cancellationToken)
    {
        var hashedPassword = await _passwordRepository.GetHashedPasswordAsync(email, cancellationToken);

        if (hashedPassword != null && _passwordService.VerifyHashedPassword(hashedPassword, password))
            return new SuccessResult();

        return new InvalidResult("Invalid email or password");
    }
}
