using System;
using System.IO;
using System.Threading.Tasks;
using API.Domain.Interfaces;
using API.Dto;
using Microsoft.AspNetCore.Http;

namespace API.Domain
{
  public class Post : IPost, IEntity, IAuditable
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public string ImageUrl { get; set; }
    public IUser Author { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Post(NewPostDto entity, string imageUrl)
    {
      Id = new Guid();
      Title = entity.Title;
      Description = entity.Description;
      Likes = 0;
      Dislikes = 0;
      ImageUrl = imageUrl;
      Author = new User();
      CreatedAt = UpdatedAt = DateTime.Now;
    }

  }
}