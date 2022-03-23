using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserAuthentication.Models;
using UserAuthentication.Services;

namespace UserAuthentication.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService) {

            _questionService = questionService;
      
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _questionService.ListAsync(); 
            return Ok(res);
        }


        [HttpPost, Authorize]
        public async Task<IActionResult> Create(Question question)
        {
            var user = User;
            if (user == null)
            {
                return Unauthorized();
            }
            var role = user.FindFirstValue(ClaimTypes.Role);
            if (role == "admin")
            {
                var res = await _questionService.Create(question);
                return Ok(res);

            }

            return BadRequest("User cannot access");
        }


        [HttpGet("{Id}")] 
        public async Task<IActionResult> GetbyId(int Id)
        {
            var res = await _questionService.Get(Id);
            return Ok(res);
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> Put(Question question)
        {
            var user = User;
            if (user == null)
            {
                return Unauthorized();
            }
            var role = user.FindFirstValue(ClaimTypes.Role);
            if (role == "admin")
            {
                var res = await _questionService.Update(question);
                return Ok(res);

            }

            return BadRequest("User cannot access");
        }

        [HttpDelete, Authorize] 
        public async Task<IActionResult> DeleteById(int Id)
        {
            var user = User;
            if (user == null)
            {
                return Unauthorized();
            }
            var role = user.FindFirstValue(ClaimTypes.Role);
            if (role == "admin")
            {
                var res = await _questionService.Delete(Id);
                return Ok(res);

            }

            return BadRequest("User cannot access");
        }
        
    }

}
