using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapi.Models.DTO;
using blogapi.Serivces;
using Microsoft.AspNetCore.Mvc;

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
    }
}