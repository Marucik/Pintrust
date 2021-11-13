using System;
using System.Collections.Generic;
using API.Domain.Interfaces;

namespace API.Domain
{
    public class AuthenticateResponse
    {
        // Guid Id { get; set; }
        // public string Login { get; set; }
        // public IEnumerable<Guid> Favourites { get; set; }
        // public IEnumerable<Reaction> Reactions { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(string token)
        {
            //   Id = user.Id;
            //   Login = user.Login;
            //   Favourites = user.Favourites;
            //   Reactions = user.Reactions;
            Token = token;
        }
    }
}