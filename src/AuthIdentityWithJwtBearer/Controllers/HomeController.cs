using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthIdentityWithJwtBearer.Controllers
{
  [ApiController]
  [Route("home")]
  public class HomeController : Controller
  {
    [HttpGet]
    [Route("anonymous")]
    [AllowAnonymous]
    public string Anonymous() => "Anônimo";

    [HttpGet]
    [Route("authenticated")]
    [Authorize]
    public ActionResult Authenticated()
    {
      return Ok(String.Format("Autenticado - {0} - {1}", User.Identity.Name,
      User.FindFirst(c => c.Type == ClaimTypes.Role).Value));
    }

    [HttpGet]
    [Route("employee")]
    [Authorize(Roles = "employee,manager")]
    public string Employee() => "Funcionário";

    [HttpGet]
    [Route("manager")]
    [Authorize(Roles = "manager")]
    public string Manager() => "Gerente";
  }
}