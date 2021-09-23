using System;

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
    IUser Author { get; set; }
  }
}