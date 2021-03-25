using System.Threading.Tasks;
using AuthIdentityWithJwtBearer.Domain.Entities;
using AuthIdentityWithJwtBearer.Repositories;
using AuthIdentityWithJwtBearer.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthIdentityWithJwtBearer.Controllers
{
  [ApiController]
  [Route("auth")]
  public class AuthController : Controller
  {

    [HttpPost]
    [Route("login")]
    public ActionResult<dynamic> Authenticate([FromBody] User model)
    {
      // Recupera o usuário
      var user = UserRepository.Get(model.Username, model.Password);

      // Verifica se o usuário existe
      if (user == null)
        return NotFound(new { message = "Usuário ou senha inválidos" });

      // Gera o Token
      var token = TokenService.GenerateToken(user);

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