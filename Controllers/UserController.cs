using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public UserController(UserService dataFromService){
            _data = dataFromService;
        }    


        //Add a user
        [HttpPost("AddUsers")]
                            //Type          //name
        public bool AddUser(CreateAccountDTO UserToAdd){
             
            return _data.AddUser(UserToAdd);
        }

            //if user already exists
            //if user does not exist, create an account
            //else throw an error

         //login
         //update user account
         //delete user account   
    }
}