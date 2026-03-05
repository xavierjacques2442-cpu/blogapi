using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapi.Models;
using blogapi.Models.DTO;
using blogapi.Serivces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace blogapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userSerivce;

        public UserController(UserService userSerivce)
        {
            _userSerivce = userSerivce;
        }

        //Fuction that adds user to database.
        [HttpPost("Add")]
        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            return _userSerivce.AddUser(UserToAdd);
        }


         [HttpGet("GetAllUser")]
        public IEnumerable<UserModel> GetAllUser()
        {
            return _userSerivce.GetAllUser();
        }

        [HttpGet("UserById")]

        public UserIdDTO GetUserDTOUserName(string username)
        {
            return _userSerivce.GetUserDTOUserName(username);
        }

        [HttpPost("Login")]

        public IActionResult Login([FromBody] LoginDTO User)
        {
            return _userSerivce.Login(User);
        }
    }
}