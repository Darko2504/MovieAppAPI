using DataAccess;
using Domain.Models;
using Dtos.UserDto;
using Mappers;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string LoginUser(LoginUserDto loginUser)
        {
            if (string.IsNullOrEmpty(loginUser.Username) || string.IsNullOrEmpty(loginUser.Password))
            {
                throw new UserDataException("Username and password are required fields!");
            }

            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] bytePassword = Encoding.ASCII.GetBytes(loginUser.Password);
            byte[] bytePasswordHashed = mD5CryptoServiceProvider.ComputeHash(bytePassword);
            string hashedPassword = Encoding.ASCII.GetString(bytePasswordHashed);

            User userDb = _userRepository.LoginUser(loginUser.Username, hashedPassword);

            if (userDb == null)
            {
                throw new UserNotFoundException("User not found!");
            }

            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("Our very very very very very very secret key");
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, userDb.Username),
                        new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}")
                    }
                    ),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = securityTokenHandler.CreateToken(securityTokenDescriptor);

            return securityTokenHandler.WriteToken(token); 
        }

        public void RegisterUser(RegisterUserDto registerUser)
        {
            ValidRegisterUser(registerUser);

            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] passwrodBytes = Encoding.ASCII.GetBytes(registerUser.Password);

            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwrodBytes);
            string hashedPassword = Encoding.ASCII.GetString(hashBytes);


            User user = new User()
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Username = registerUser.Username,
                Password = hashedPassword
            };

            _userRepository.Add(user);
        }

        private void ValidRegisterUser(RegisterUserDto registerUserDto)
        {
            if (string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password) || string.IsNullOrEmpty(registerUserDto.ConfirmPassword))
            {
                throw new UserDataException("Username and password are required fields!");
            }
            if (registerUserDto.Username.Length > 30)
            {
                throw new UserDataException("Username maximum length is 30 characters");
            }
            if (string.IsNullOrEmpty(registerUserDto.FirstName) || registerUserDto.FirstName.Length > 50)
            {
                throw new UserDataException("First Name cannot be longer than 50 characters");
            }
            if (string.IsNullOrEmpty(registerUserDto.LastName) || registerUserDto.LastName.Length > 50)
            {
                throw new UserDataException("Last Name cannot be longer than 50 characters");
            }
            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new UserDataException("Passwords must match!");
            }

            var userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if (userDb != null)
            {
                throw new UserDataException("Username is already in use, try another one");
            }
        }
        }
}
