using System.Threading.Tasks;
using AuthIdentityWithJwtBearer.Data.Repositories;
using AuthIdentityWithJwtBearer.Domain.Entities;

namespace AuthIdentityWithJwtBearer.Application.Services
{
  public interface IAuthService
  {
    User SignIn(User user);
    Task<User> SignUp(User user);
  }

  public class AuthService : IAuthService
  {
    private readonly IUserRepository _userRepository;
    public AuthService(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public User SignIn(User user)
    {
      return _userRepository.Get(user.Username, user.Password);
    }
    public async Task<User> SignUp(User user)
    {
      if (await _userRepository.Add(user))
      {
        return _userRepository.Get(user.Username, user.Password);
      }
      else return null;
    }

  }
}