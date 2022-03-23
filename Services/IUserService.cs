using UserAuthentication.Models;
using UserAuthentication.Models.RequestModel;
using UserAuthentication.Models.ResponseModel;

namespace UserAuthentication.Services
{
    public interface IUserService
    {
        public Task<Models.ResponseModel.UserResponseModel> Register(UserRequestModel user);
        public Task<Models.ResponseModel.UserResponseModel> Login(UserLoginRequestModel user);
        string GetMyName();
    }
}
