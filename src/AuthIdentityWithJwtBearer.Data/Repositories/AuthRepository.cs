using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AuthIdentityWithJwtBearer.Data.Repositories
{
  public interface IAuthRepository
  {
    Task AddClaims(string username, Claim[] claims);
    Task<bool> Create(string username, string password);
    Task<IList<Claim>> GetClaims(string username);
    Task<bool> IsLogged(string username, string password);
  }

  public class AuthRepository : IAuthRepository
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    public async Task<bool> Create(string username, string password)
    {

      var result = await _userManager.CreateAsync(new IdentityUser { UserName = username }, password);

      return result.Succeeded;
    }
    public async Task<bool> IsLogged(string username, string password)
    {
      var user = await _userManager.FindByNameAsync(username);

      if (user == null) return false;

      var result = await _signInManager.PasswordSignInAsync(user, password, false, true);

      return result.Succeeded;
    }
    public async Task AddClaims(string username, Claim[] claims)
    {
      var user = await _userManager.FindByNameAsync(username);

      if (user != null)
        foreach (var claim in claims)
          await _userManager.AddClaimAsync(user, claim);
    }
    public async Task<IList<Claim>> GetClaims(string username)
    {
      var user = await _userManager.FindByNameAsync(username);

      if (user == null) return null;

      return await _userManager.GetClaimsAsync(user);
    }
  }
}