using System;
using System.Collections.Generic;

namespace API.Domain.Interfaces
{
  public interface IUser
  {
    Guid Id { get; set; }
    string Login { get; set; }
    string Password { get; set; }
    List<Guid> Favourites { get; set; }
  }
}