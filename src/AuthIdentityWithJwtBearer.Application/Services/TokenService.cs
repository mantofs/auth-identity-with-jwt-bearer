using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthIdentityWithJwtBearer.Config;
using AuthIdentityWithJwtBearer.Data.Repositories;
using AuthIdentityWithJwtBearer.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthIdentityWithJwtBearer.Application.Services
{
  public interface ITokenService
  {
    Task<string> GenerateTokenAsync(User user);
  }

  public class TokenService : ITokenService
  {
    private readonly Settings _settings;
    private readonly IAuthRepository _authRepository;
    public TokenService(IOptions<Settings> opt, IAuthRepository authRepository)
    {
      _settings = opt.Value;
      _authRepository = authRepository;
    }
    public async Task<string> GenerateTokenAsync(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_settings.SecretKey);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(await _authRepository.GetClaims(user.Username)),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        Issuer = _settings.Issuer,
        Audience = _settings.Audience
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}