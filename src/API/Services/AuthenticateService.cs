using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Domain;
using API.Domain.Interfaces;
using API.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthenticateService> _logger;

        public AuthenticateService(IUserRepository userRepository, ILogger<AuthenticateService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var user = await _userRepository.GetByLogin(request.Login);

            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) return null;

            var token = generateJwtToken(user);

            return new AuthenticateResponse(token);
        }

        private string generateJwtToken(User user)
        {
            var userId = user.Id;

            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("D117A25C-4895-428A-8EC0-221A93AFB653");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}