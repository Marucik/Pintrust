using System;
using API.Dto;

namespace API.Domain.Interfaces
{
  public interface IPost
  {
    Guid Id { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    int Likes { get; set; }
    int Dislikes { get; set; }
    string ImageUrl { get; set; }
    UserDto Author { get; set; }
  }
}