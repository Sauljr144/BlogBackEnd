using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogBackEnd.Models;
using BlogBackEnd.Models.DTO;
using BlogBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        //create variable with a type of service
        private readonly UserService _data;

        //creagte a constructor
        public UserController(UserService dataFromService)
        {
            _data = dataFromService;
        }

        //GetUserByUsername
        [HttpGet("GetUSerByUsername/{username}")]
        public UseridDTO GetUserIdDTOByUsername(string username)
        {
            return _data.GetUserIdDTOByUsername(username);
        }


        //Add a user
        [HttpPost("AddUsers")]
        //Type          //name
        public bool AddUser(CreateAccountDTO UserToAdd)
        {

            return _data.AddUser(UserToAdd);
        }

        //Get All Users
        [HttpGet("GetAllUsers")]
        public IEnumerable<UserModel> GetAllUsers()
        {
            return _data.GetAllUsers();
        }
        //if user already exists
        //if user does not exist, create an account
        //else throw an error

        //login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO User)
        {
            return _data.Login(User);
        }

        //delete user account
        [HttpPost("DeleteUser/{userToDelete}")]

        public bool DeleteUser(string userToDelete)
        {
            return _data.DeleteUser(userToDelete);
        }

        //update user account
        [HttpPost("UpdateUser")]

        public bool UpdateUser(int id, string username)
        {
            return _data.UpdateUsername( id, username);
        }

        


    }
}