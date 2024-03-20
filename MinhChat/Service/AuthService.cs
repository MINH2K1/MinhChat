using MinhChat.Interface;
using MinhChat.Model;

namespace MinhChat.Service
{
    public class AuthService: IAuthService
    {
        readonly ITokenService tokenService;

        public AuthService(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }
        public Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request)
        {
            UserLoginResponse response = new();


            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Username == "onur" && request.Password == "123456")
            {
                response.AccessTokenExpireDate = DateTime.UtcNow;
                response.AuthenticateResult = true;
                response.AuthToken = string.Empty;
            }

            return Task.FromResult(response);
        }
    }
}
