using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace API.Repositories
{
  public interface IRepository<T> where T : IEntity
  {
    Task InsertAsync(T entity);
  }
}