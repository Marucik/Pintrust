using System;
using System.Collections.Generic;

namespace API.Domain
{
    public class UserProfile
    {
        public UserProfile(User user)
        {
            Id = user.Id;
            Login = user.Login;
            Favourites = user.Favourites;
            Reactions = user.Reactions;
        }

        Guid Id { get; set; }
        public string Login { get; set; }
        public IEnumerable<Guid> Favourites { get; set; }
        public IEnumerable<Reaction> Reactions { get; set; }
    }
}