namespace AspIdentityApp.Core
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginModel request);
        Task<Response> RegisterAsync(RegisterModel request);
    }
}
