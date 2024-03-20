using MinhChat.Model;

namespace MinhChat.Interface
{
    public interface IAuthService
    {
        public Task<UserLoginResponse> LoginUserAsync(UserLoginRequest request);
    }
}
