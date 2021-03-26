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
    public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User model)
    {

      if (!await _authService.SignIn(model))
        return NotFound(new { message = "Usuário ou senha inválidos" });

      // Gera o Token
      var token = await _tokenService.GenerateTokenAsync(model);

      return Ok(new { token });

    }

    [HttpPost]
    [Route("signup")]
    public async Task<ActionResult> Create([FromBody] User model)
    {
      await _authService.SignUp(model);

      var token = await _tokenService.GenerateTokenAsync(model);

      return Ok(new { token });

    }
  }
}