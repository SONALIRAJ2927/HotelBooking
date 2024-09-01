using HotelBooking.BusinessInterface;
using HotelBooking.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly IUserService _UserService;
        public RegisterUserController(IUserService userService)
        {
            _UserService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUser()
        {
            var user = await _UserService.GetAllUser(); //we are storing data in variable
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var user = await _UserService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> AddUser(UserModel user)
        {
            var newuser = await _UserService.AddUser(user);
            if (newuser == null)
            {
                return BadRequest("Failed to add user.");
            }

            //return CreatedAtAction(nameof(GetUserById), new { id = newuser.Id}, newuser);
            return Ok(newuser);
        }


        [HttpPut("{ID}")]
        public async Task<ActionResult> UpdateUser(int ID, UserModel user)
        {
            if (ID !=user.Id)
            {
                return BadRequest();
            }

            var UpdateUser = await _UserService.UpdateUser(user);

            if (UpdateUser == null)
            {
                return NotFound();
            }

            return Ok(UpdateUser);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> DeleteUser(int ID)
        {
            await _UserService.DeleteUser(ID);
            return NoContent();
        }

        [HttpPost("login")] //login path 
        public async Task<IActionResult> Login( ValidateUserModel validateUser) //login method
        {
            if (validateUser == null)
            {
                return BadRequest("Invalid login request.");
            }

            var isValidUser = await _UserService.Login(validateUser.Email, validateUser.Password);

            if (isValidUser) //bool type we return data in stored proceured 0 and 1
            {
                return Ok("Login successful.");
            }
            else
            {
                return Unauthorized("Invalid email or password.");
            }
        }
    }

}
