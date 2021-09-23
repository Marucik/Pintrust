using System;

namespace API.Domain.Interfaces
{
  public interface IAuditable
  {
    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
  }
}