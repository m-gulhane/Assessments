using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using LabTest.Domain.Interfaces;
using LabTest.Domain.Models;
using LabTest.Infrastructure.DataContext;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LabTest.Infrastructure.Repository
{
    public class UserRepository :IUserRepository
    {
        #region Global Variables
        /// <summary>
        /// Private read only dbContext
        /// </summary>
        private readonly LabTestDataContext dbContext;

        /// <summary>
        /// Collection of users
        /// </summary>
        private readonly IDictionary<string, string> authoriseUsers = new Dictionary<string, string>
        {
            { "demouser", "demopassword" }
        }; 
        #endregion

        #region Contructor
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="dbContext"></param>
        public UserRepository(LabTestDataContext dbContext)
        {
            this.dbContext = dbContext;
        } 
        #endregion

        #region Public Methods
        /// <summary>
        /// Login Method to get the token
        /// </summary>
        /// <param name="loginUser">Model to check the authentication</param>
        /// <returns>string token</returns>
        public string Login(User loginUser)
        {
            if (!authoriseUsers.Any(u => u.Key == loginUser.UserName && u.Value == loginUser.Password))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = dbContext.Config.GetValue<string>("TokenKey");
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, loginUser.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // set the token lifetime
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        } 
        #endregion

    }
}
