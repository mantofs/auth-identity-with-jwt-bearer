using System.Security.Claims;
using System.Threading.Tasks;
using AuthIdentityWithJwtBearer.Data.Repositories;
using AuthIdentityWithJwtBearer.Domain.Entities;

namespace AuthIdentityWithJwtBearer.Application.Services
{
  public interface IAuthService
  {
    Task<bool> SignIn(User user);
    Task<bool> SignUp(User user);
  }

  public class AuthService : IAuthService
  {
    private readonly IAuthRepository _authRepository;
    public AuthService(IAuthRepository authRepository)
    {
      _authRepository = authRepository;
    }

    public async Task<bool> SignIn(User user)
    {
      return await _authRepository.IsLogged(user.Username, user.Password);
    }
    public async Task<bool> SignUp(User user)
    {
      if (await _authRepository.Create(user.Username, user.Password))
      {
        var claims = new Claim[]{
          new Claim(ClaimTypes.Name, user.Username),
          new Claim(ClaimTypes.Role, user.Role)
        };

        await _authRepository.AddClaims(user.Username, claims);

        return true;
      }
      return false;
    }

  }
}