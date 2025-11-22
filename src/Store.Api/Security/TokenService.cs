using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Store.Api.Contracts.Responses;
using Store.Application.Services;
using Store.Common.Helpers;

namespace Store.Api.Security;

public class TokenService
{
    private readonly JwtConfig _config;
    private readonly ITokenUserService _tokenUserService;

    public TokenService(JwtConfig config, ITokenUserService tokenUserService)
    {
        _config = config.NotNull();
        _tokenUserService = tokenUserService.NotNull();
    }

    public async Task<TokenResponse> CreateTokenAsync(string email, string password, CancellationToken cancellationToken)
    {
        var validPassword = await _tokenUserService.VerifyPassword(email, password, cancellationToken);
        if (!validPassword.Success)
            return null;

        var userResult = await _tokenUserService.GetUserAsync(email, cancellationToken);
        if (!userResult.Success)
            return null;

        var issuer = _config.Issuer;
        var audience = _config.Audience;
        var signingCredentials = new SigningCredentials(
            _config.Key,
            SecurityAlgorithms.HmacSha512Signature
        );

        var identity = new ClaimsIdentity(new[]
        {
                new Claim(ClaimTypes.NameIdentifier, userResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, userResult.Data.Email),
        });

        var roleClaims = userResult.Data.Roles.Select(x => new Claim(ClaimTypes.Role, x));
        identity.AddClaims(roleClaims);

        var expires = DateTime.UtcNow.AddMinutes(10);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = signingCredentials,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return new TokenResponse
        {
            Token = jwtToken,
            Expires = expires
        };
    }
}