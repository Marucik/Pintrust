using System.Threading.Tasks;

namespace API.Domain.Interfaces
{
  public interface IAuthenticateService
  {
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
  }
}