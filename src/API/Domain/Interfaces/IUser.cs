using System;

namespace API.Domain.Interfaces
{
  public interface IUser
  {
    Guid Id { get; set; }
    Guid AccountId { get; set; }
    string Name { get; set; }
    Guid[] Favourites { get; set; }
  }
}