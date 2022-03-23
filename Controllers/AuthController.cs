using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAuthentication.Models.ResponseModel;
using UserAuthentication.Services;

namespace UserAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserResponseModel>> Register(Models.RequestModel.UserRequestModel request)
        {
            var user = await _userService.Register(request);

            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserResponseModel>> SignIn(Models.RequestModel.UserLoginRequestModel request)
        {

            var token = await _userService.Login(request);
            return Ok(token);
        }

        //[Authorize]
        //[HttpPut]
        //public async Task<ActionResult<string>> UpdateUser(UserRequestModel request)
        //{
        //   if (User.Claims.Role == "Admin")
        //    {
        //        // do update
        //    }
        //   else if (User.Claims.Role == "User")
        //    {
        //        if (User.Claims.UserName == request.Email)
        //        {
        //            // do update
        //        }
        //        else
        //        {
        //            // you cannot able update the record
        //        }
        //    }
        //    return null;
        //}
       

        
    }

}

