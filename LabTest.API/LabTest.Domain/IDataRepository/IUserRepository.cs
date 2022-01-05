using System;
using System.Collections.Generic;
using System.Text;
using LabTest.Domain.Models;

namespace LabTest.Domain.Interfaces
{
   public interface IUserRepository
    {
        /// <summary>
        /// Declaration of Login method
        /// </summary>
        /// <param name="loginUser">Model to check the authentication</param>
        /// <returns>string token</returns>
        string Login(User loginUser);
    }
}
