using System;

namespace API.Domain.Interfaces
{
  public interface IReaction
  {
    Guid PostId { get; set; }
    string UserReaction { get; set; }
  }
}