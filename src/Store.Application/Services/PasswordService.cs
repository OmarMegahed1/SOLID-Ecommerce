using Microsoft.AspNetCore.Identity;
using Store.Application.Models;
using Store.Common.Helpers;

namespace Store.Application.Services;

public class PasswordService : IPasswordService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher.NotNull();
    }

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(new User(), password);
    }

    public bool VerifyHashedPassword(string hashedPassword, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(new User(), hashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }
}