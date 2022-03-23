using UserAuthentication.Models.Common;

namespace UserAuthentication.Models.ResponseModel
{
    public class UserResponseModel : UserModel
    {
        public int Id { get; set; }
        public string Token { get; set; }
    }
}
