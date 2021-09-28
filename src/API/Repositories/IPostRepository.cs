using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain;
using Microsoft.AspNetCore.Http;

namespace API.Repositories
{
  public interface IPostRepository : IRepository<Post>
  {
    Task<IEnumerable<Post>> GetAllAsync();
  }
}