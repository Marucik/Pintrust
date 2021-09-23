using System;
using API.Domain.Interfaces;

namespace API.Domain
{
  public class User : IUser, IEntity
  {
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string Name { get; set; }
    public Guid[] Favourites { get; set; }
  }
}