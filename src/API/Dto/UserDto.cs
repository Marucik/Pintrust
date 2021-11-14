using System;
using API.Domain;

namespace API.Dto
{
  public class UserDto
  {
    public Guid Id { get; set; }
    public string Login { get; set; }

    public UserDto(User user)
    {
      Id = user.Id;
      Login = user.Login;
    }
  }
}