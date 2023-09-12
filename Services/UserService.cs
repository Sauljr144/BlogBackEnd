using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogBackEnd.Models;
using BlogBackEnd.Models.DTO;
using BlogBackEnd.Services.Context;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace BlogBackEnd.Services
{
    public class UserService : ControllerBase
    {
        public IEnumerable<UserModel> GetAllUsers()
        {
            return _context.UserInfo;
        }

        //create a variable
        private readonly DataContext _context;

        //Create a contstructor
        public UserService(DataContext context)
        {
            _context = context;
        }

        //helper function, check to see if user exist, DoesUserExist(string username)
        public bool DoesUserExist(string? username)
        {
            //check the table to see if username exists
            //if one item matches our condition that item will be returned
            //if no item matches condition, return null
            //if multiple items match the condition, return error

            // UserModel foundUser = _context.UserInfo.SingleOrDefault(user => user.Username == username);

            // if(foundUser != null){
            //     //the user exists in the tabel
            // }
            // else{
            //     //user does not exist
            // }

            return _context.UserInfo.SingleOrDefault(user => user.Username == username) != null;
        }

        public bool AddUser(CreateAccountDTO UserToAdd)
        {

            bool result = false;

            //if user already exists
            if (!DoesUserExist(UserToAdd.Username))
            {
                //we need to create a new instance of our UserModel
                UserModel newUser = new UserModel();

                var newHashedPassword = HashPassword(UserToAdd.Password);

                // newUser.Id = UserToAdd.Id;
                newUser.Username = UserToAdd.Username;
                newUser.Salt = newHashedPassword.Salt;
                newUser.Hash = newHashedPassword.Hash;

                //add to our database
                _context.Add(newUser);
                //save changes
                result = _context.SaveChanges() != 0;

            }
            return result;

            //if user does not exist, create an account
            //else throw a false
        }

        public PasswordDTO HashPassword(string password)
        {
            //logic goes here
            //create a pasword DTO this is what we are going to return
            //we need to create a new instance of our PasswordDTO

            PasswordDTO newHashedPassword = new PasswordDTO();

            //salt bytes size of our SaltBytes which is 64
            byte[] SaltBytes = new byte[64];

            //RNGCryptoServiceProvider creates random numbers
            var provider = new RNGCryptoServiceProvider();
            //now wer are going to exclude all the zeros
            provider.GetNonZeroBytes(SaltBytes);
            //encrypt our 64 string and encrypt if for us
            var Salt = Convert.ToBase64String(SaltBytes);
            //we will use to create the hash, first argument is the password, bytes, iterations
            var rfc2898derivebytes = new Rfc2898DeriveBytes(password, SaltBytes, 10000);
            //now we create our hash
            var Hash = Convert.ToBase64String(rfc2898derivebytes.GetBytes(256));
            //return newHashedPassword.Salt
            newHashedPassword.Salt = Salt;
            newHashedPassword.Hash = Hash;

            return newHashedPassword;

        }

        public bool VerifyUserPassword(string? Password, string? StoredHashed, string? StoredSalt)
        {
            var SaltBytes = Convert.FromBase64String(StoredSalt);
            var rfc2898derivebytes = new Rfc2898DeriveBytes(Password, SaltBytes, 10000);
            var newHash = Convert.ToBase64String(rfc2898derivebytes.GetBytes(256));
            return newHash == StoredHashed;
        }

        public UserModel GetUserByUsername(string? username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username);
        }

        public UserModel GetUserByID(int ID)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Id == ID);
        }

        public IActionResult Login(LoginDTO user)
        {
            IActionResult Result = Unauthorized();
            if (DoesUserExist(user.Username))
            {

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                Result = Ok(new { Token = tokenString });
            }
                return Result;
        }

        public bool DeleteUser(string Username)
        {
            //This one is sending over just the username
            //Then you have to get the object and then update
            UserModel foundUser = GetUserByUsername(Username);
            bool result = false;
            if(foundUser != null)
            {
                //found user
                foundUser.Username = Username;
                _context.Remove<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public bool UpdateUsername(int id, string Username)
        {
            UserModel foundUser =  GetUserByID(id);
            bool result = false;
            if(foundUser != null)
            {
                foundUser.Username = Username;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;

            }
            
            return result;
        }
    }

}