using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthIdentityWithJwtBearer.Domain.Entities;


namespace AuthIdentityWithJwtBearer.Data.Repositories
{
  public interface IUserRepository
  {
    Task<bool> Add(User user);
    User Get(string username, string password);
  }

  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;
    public UserRepository(DataContext ctx)
    {
      _context = ctx;
    }
    public User Get(string username, string password)
    {
      //var users = new List<User>();
      //users.Add(new User { Id = Guid.NewGuid(), Username = "batman", Password = "batman", Role = "manager" });
      //users.Add(new User { Id = Guid.NewGuid(), Username = "robin", Password = "robin", Role = "employee" });
      return _context.Users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
    }
    public async Task<bool> Add(User user)
    {
      await _context.Users.AddAsync(user);

      return _context.SaveChanges() > 0;
    }
  }
}