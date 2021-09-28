namespace API.Domain
{
  public class AuthenticateRequest
  {
    public string Login { get; set; }
    public string Password { get; set; }
  }
}