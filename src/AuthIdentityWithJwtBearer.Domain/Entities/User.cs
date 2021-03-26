using System;
namespace AuthIdentityWithJwtBearer.Domain.Entities
{
  public class User
  {
    protected User()
    {
      Id = Guid.NewGuid();
    }
    public User(string username, string password, string role)
    {
      Username = username;
      Password = password;
      Role = role;
      Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Role { get; private set; }
  }
}