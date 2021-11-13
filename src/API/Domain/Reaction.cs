using System;
using API.Domain.Interfaces;

namespace API.Domain
{
  public class Reaction : IReaction
  {
    public Reaction()
    {
    }

    public Guid PostId { get; set; }
    public string UserReaction { get; set; }
  }
}