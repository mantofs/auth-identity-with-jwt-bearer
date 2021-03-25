using System.Threading.Tasks;
using AuthIdentityWithJwtBearer.Application.Services;
using AuthIdentityWithJwtBearer.Data.Repositories;
using AuthIdentityWithJwtBearer.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuthIdentityWithJwtBearer.Controllers
{
  [ApiController]
  [Route("auth")]
  public class AuthController : Controller
  {
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    public AuthController(IAuthService authService, ITokenService tokenService)
    {
      _authService = authService;
      _tokenService = tokenService;
    }

    [HttpPost]
    [Route("login")]
    public ActionResult<dynamic> Authenticate([FromBody] User model)
    {
      // Recupera o usuário
      var user = _authService.SignIn(model);

      // Verifica se o usuário existe
      if (user == null)
        return NotFound(new { message = "Usuário ou senha inválidos" });

      // Gera o Token
      var token = _tokenService.GenerateToken(user);

      // Oculta a senha
      user.Password = "";

      // Retorna os dados
      return new
      {
        user = user,
        token = token
      };
    }
    [HttpPost]
    [Route("signup")]
    public async Task<ActionResult<dynamic>> Create([FromBody] User model)
    {
      // Recupera o usuário
      var user = await _authService.SignUp(model);

      // Verifica se o usuário existe
      if (user == null)
        return NotFound(new { message = "Usuário ou senha inválidos" });

      // Gera o Token
      var token = _tokenService.GenerateToken(user);

      // Oculta a senha
      user.Password = "";

      // Retorna os dados
      return new
      {
        user = user,
        token = token
      };
    }
  }
}