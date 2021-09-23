using API.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace API.Dto
{
  public class NewPostDto
  {
    public string Title { get; set; }
    public string Description { get; set; }
  }
}