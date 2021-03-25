using System;
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
    public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

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