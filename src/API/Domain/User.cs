using System;
using System.Collections.Generic;
using API.Domain.Interfaces;
using API.Dto;

namespace API.Domain
{
  public class User : IUser, IEntity
  {
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<Guid> Favourites { get; set; }
    public List<Reaction> Reactions { get; set; }

    public User(NewUserDto entity)
    {
      Id = Guid.NewGuid();
      Login = entity.Login;
      Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
      Favourites = new List<Guid>();
      Reactions = new List<Reaction>();
    }
  }
}