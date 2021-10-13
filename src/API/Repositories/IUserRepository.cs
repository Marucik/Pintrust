using System;
using System.Threading.Tasks;
using API.Domain;
using API.Domain.Interfaces;

namespace API.Repositories
{
  public interface IUserRepository : IRepository<User>
  {
    Task<User> GetByLogin(string login);
    Task<User> GetById(Guid id);
    Task AddFavourite(User user, Guid postId);
  }
}